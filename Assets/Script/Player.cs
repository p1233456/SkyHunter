using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {
    [SerializeField]
    private GameObject explosionPrefab;	//파괴시 애니메이션
    [SerializeField]
    private GameObject laserPrefab;		//레이저
    [SerializeField]
    private GameObject barier;			//베리어
    [SerializeField]
    private GameObject overBolete;				//관통 레이저
    [SerializeField]
    private GameObject supportShot;     //지원사격 이미지

    public float animTime = 2f;         // Fade 애니메이션 재생 시간 (단위:초).  
    [SerializeField]
    private SpriteRenderer fadeImage;            // 페이드 아웃할 렌더러  

    private float start = 1f;           // Mathf.Lerp 메소드의 첫번째 값.  
    private float end = 0f;             // Mathf.Lerp 메소드의 두번째 값.  
    private float time = 0f;            // Mathf.Lerp 메소드의 시간 값.  

    private bool isPlaying = false;     // Fade 애니메이션의 중복 재생을 방지하기위해서 사용.  

    private bool canShoot = true; 	//레이저를 쏠 수 있는 상태인지 검사합니다.
    [SerializeField]
	public float shootDelay = 0.4f; 	//레이저를 쏘는 주기를 정해줍니다.

	public int damage = 1;			//공격력
	private float moveSpeed = 5.0f;	//이동속도
	private bool isbarier=false;		//배리어 사용중인지 검사
	private int HP=1;					//채력
    private bool overShot;
    private Laser laser;
    private Enemy enemy;

    [SerializeField]
    private Transform shootPosition;
	
	void Awake(){
		canShoot = true;    //레이저를 쏠 수 있는 상태인지 검사합니다.

        shootDelay = 0.7f; 	//레이저를 쏘는 주기를 정해줍니다.

		damage = 1;			//공격력
		isbarier=false;     //배리어 사용중인지 검사
        HP = 1;			    //채력
        fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, 0);
	}

    private void Start()
    {
        laser = laserPrefab.GetComponent<Laser>();
        barier.SetActive(false);
    }

    void Update () {
		MoveControl();
		ShootControl ();
		SkillControl ();
        if (gameManager.Instance.gameOver)
        {
            Destroy(this.gameObject);
        }
	}

    private void OnDestroy()
    {
        Instantiate(explosionPrefab, this.transform.position, Quaternion.identity);
    }

    void MoveControl()//이동 관할
	{		
		float distanceX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
		this.gameObject.transform.Translate(distanceX, 0, 0);
		//화면밖으로 안나가게 검사
		if (this.transform.position.x < -3)
			this.transform.position = new Vector3 (-3f, -4f, -1f);
		else if (this.transform.position.x > 3)
			this.transform.position = new Vector3 (3f, -4f, -1f);

	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag.Equals("Enemy") || other.gameObject.tag.Equals("Shot"))	//부딪힌 객체의 태그를 비교해서 적인지 판단합니다.
		{
			if (isbarier) {						//베리어가 펼쳐저 있다면
				Destroy (other.gameObject);		//에너미 파괴
				isbarier = false;				//베리어 끔		
                barier.SetActive(false);
			}
            else {
                HP -= 1;
			}
		}
	}

	void ShootControl() // 발사를 관리하는 함수
	{
		if (canShoot && Input.GetKey(KeyCode.Space)) // 쏠 수 있는 상태인지 검사
		{
            StartCoroutine(Shot());
		}
	}

	void SkillControl(){//스킬 관할
        if (Input.GetKeyDown(KeyCode.Q) && gameManager.Instance.Gage >= 100 && !isbarier)//게이지 100소비로 베리어(3초중 1회)
            StartCoroutine(Barier());
        if (Input.GetKeyDown(KeyCode.W) && gameManager.Instance.Gage >= 225)//게이지 200소비로 지원사격(전체 화면의 적 처치)
            StartCoroutine(SupportShooting());
        if (Input.GetKeyDown(KeyCode.E) && gameManager.Instance.Gage >= 50)//게이지 80소비로 관통초(관통총알 발살가능(5))
            StartCoroutine(OverShot());
    }			
	

	IEnumerator Barier()    //베리어
    {
        gameManager.Instance.MGage(100);
        barier.SetActive(true);
        isbarier = true;
        yield return new WaitForSeconds(5f);
        barier.SetActive(false);
        isbarier = false;
	}		

    IEnumerator SupportShooting()//지원사격
    {
        gameManager.Instance.MGage(225);
        GameObject[] findObject = GameObject.FindGameObjectsWithTag("Enemy");
        for(int i=0; i<findObject.Length; i++)
        {
            enemy = findObject[i].GetComponent<Enemy>();
            enemy.MHP((int)enemy.HP);
        }
        StartCoroutine(PlayFadeOut());
        yield return 0;
    }

    IEnumerator OverShot()  //관통총
    {
        gameManager.Instance.MGage(50);
        overShot = true;
        yield return new WaitForSeconds(3f);
        overShot = false;
    }
    	
    IEnumerator Shot()  //사격 코루틴
    {
        canShoot = false;
        if (overShot)
        {
            goTh overshot = Instantiate(overBolete, shootPosition.position, Quaternion.identity).GetComponent<goTh>();
            overshot.damage = damage + (int)gameManager.Instance.attackLevel;
        }
        else
        {
            Laser laser = Instantiate(laserPrefab,shootPosition.position, Quaternion.identity).GetComponent<Laser>();
            laser.damage = damage + (int)gameManager.Instance.attackLevel;
        }
        yield return new WaitForSeconds(shootDelay - (gameManager.Instance.attackSpeedSatat/8));
        canShoot = true;
    }

    IEnumerator PlayFadeOut()   //페이드 아웃
    {
        // 애니메이션 재생중.  
        isPlaying = true;
        fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b,255);
        // Image 컴포넌트의 색상 값 읽어오기.  
        Color color = fadeImage.color;
        time = 0f;
        color.a = Mathf.Lerp(start, end, time);

        while (color.a > 0f)
        {
            // 경과 시간 계산.  
            // 2초(animTime)동안 재생될 수 있도록 animTime으로 나누기.  
            time += Time.deltaTime / animTime;

            // 알파 값 계산.  
            color.a = Mathf.Lerp(start, end, time);
            // 계산한 알파 값 다시 설정.  
            fadeImage.color = color;

            yield return null;
        }
        fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, 0);
        // 애니메이션 재생 완료.  
        isPlaying = false;
    }

    public void MHP(int num) //체력 감소함수
    {
        HP -= num; //체력 감소
    }

    public int ReHp()   //HP반환(생존여부)
    {
        return HP;
    }
}
