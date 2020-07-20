using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour {
	private static gameManager instance; //싱글톤선언
    public static gameManager Instance
    {
        get
        {
            if (!instance)
            {
                instance = GameObject.FindObjectOfType<gameManager>();
            }

            return instance;
        }
    }
    
    [SerializeField]
    public Player player;       //플레이어

   [SerializeField]
	private int score;         //점수를 관리합니다.
    public int Score
    {
        get
        {
            return score;
        }
    }

    public bool gameOver;        //게임오버 여부
    public float gageSpeed = 1; //게이지가 차는 속도
    
    //게이지 관련 변수
    [SerializeField]
    private float gage;     //게이지
    public float Gage  //게이지 생성, 소멸자
    {
        get
        {
            return gage;
        }

        set
        {
            if (value > 200 + manaStat * 25) gage = 200 + manaStat * 25;
            else if (value < 0) gage = 0;
            else gage = value;
        }
    }

    //경험치 관련 변수
    [SerializeField]
    private float experition;    //경험치
    public float Experition
    {
        get
        {
            return experition;
        }
    }
    [SerializeField]
    private float maxExperition=10;    //최대경험치량(레벨업까지 필요한 경헙치량)
    public float MaxExperition
    {
        get
        {
            return maxExperition;
        }
    }
    //레벨 관련 변수
    private float level;    //현재 래밸
    public float Level
    {
        get
        {
            return level;
        }
    }
    private float maxLevel = 10; //최대 래밸

    public float statPoint;    //잔여 스탯 포인트

    public float attackLevel;//공격 스탯
    public float attackSpeedSatat;//공속 스탯
    public float manaStat;//마나 회복량 스탯

    bool save = false;

    void Awake()
	{
        gage = 0;
        score = 0;
		gageSpeed = 1;
        attackLevel = 0;
        attackSpeedSatat = 0;
        manaStat = 0;
        gameOver = false;
		if (!instance) //정적으로 자신을 체크합니다.
			instance = this; //정적으로 자신을 저장합니다.
        Time.timeScale = 1;
        save = false;
	}

	void Start(){
        player.GetComponent<Player>();
		StartCoroutine (GageUp());
    }

	void Update(){
        if (player.ReHp() <= 0)
        {
            if (DataManager.Instance.highScore < score)
            {
                if (!save)
                    SaveData();
            }
            Time.timeScale = 0;
            gameOver = true;
        }
        StatInput();
	}
	
	//점수관련 함수;

	public void AddScore(int num) //점수를 추가해주는 함수를 만들어 줍니다.
	{
		score += num; //점수를 더해줍니다.
	}    

    //게이지 관련 함수

    IEnumerator GageUp()
    {
        while (true)
        {
            Gage += 5;
            yield return new WaitForSeconds((gageSpeed - (manaStat / 6))/2);
        }
    }

    public void MGage(int num)
    {
        gage -= num;
    }    

    //경험치 관련 함수

    public void EXPUP(float EXP)    //경험치 증가 함수
    {
        experition += EXP;
        while (experition >= MaxExperition)
        {
            Debug.Log("levelUp");
            LevelUp();
        }
    }

    private void LevelUp()
    {
        if(level < maxLevel)
        {
            StartCoroutine(UIManager.Instance.LevelUPUI());
            level++;
            statPoint++;
            experition -= MaxExperition;
            maxExperition = level * 10 + 10;
        }
    }

    //스탯분배 관련 함수
    private void AttackUp() //공 업
    {
        if (attackLevel < 5 && statPoint > 0)
        {
            statPoint--;
            attackLevel++;
            UIManager.Instance.StatUIUpdate();
        }
    }

    private void AttackSpeedUp()        //공속 업
    {
        if (attackSpeedSatat < 5 && statPoint > 0)
        {
            statPoint--;
            attackSpeedSatat++;
            UIManager.Instance.StatUIUpdate();
        }
    }

    private void ManaUp()   //마나회복 속도 업
    {
        if (manaStat < 5 && statPoint > 0)
        {
            statPoint--;
            manaStat++;
            UIManager.Instance.StatUIUpdate();
        }
    }

    private void StatInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            AttackUp();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            AttackSpeedUp();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ManaUp();
        }
    }
    
    void SaveData()
    {
       /* new LogEventRequest().SetEventKey("SAVE_HIGHSCORE")
                    .SetEventAttribute("HIGHSCORE", score)
                    // 저장하라고 요청한다.
                    .Send((response) =>
                    {
                        if (!response.HasErrors)
                        {
                            Debug.Log("유저 데이터 저장 완료");
                        }
                        else
                        {
                            Debug.Log("저장 실패 : " + response.Errors.JSON);
                        }
                    });
        new LogEventRequest().SetEventKey("SUBMIT_SCORE")
           .SetEventAttribute("SCORE", score)
           .Send((response) => {
               if (!response.HasErrors)
               {
                   Debug.Log("점수 전달 완료");
               }
               else
               {
                   Debug.Log("점수 전달 실패..." + response.Errors.JSON);
               }
           });
        save = true;*/
    }
}

