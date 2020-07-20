using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    [SerializeField]
    private GameObject bigShot;
    [SerializeField]
    private GameObject smallShot;
    [SerializeField]
    private GameObject destroy;
    [SerializeField]
    private GameObject sideShot;
    [SerializeField]
    int randomT;
    [SerializeField]
    SpriteRenderer fadeRenderer;

    public float fadeTime = 3.6f; // Fade효과 재생시간

    float start = 1f;

    float end = 0f;

    float time = 0f;

    bool isPlaying = false;

    void Start()
    {
        StartCoroutine(ReSpawn());//공격 시작
        //StartCoroutine(AttackType2());
        hP = 50;
        score = 1000;
        EXP = 60;
        direction = new Vector2(1, 0);
    }

    new void OnTriggerEnter2D(Collider2D other)     //충돌시 실행하는 함수
    {
        base.OnTriggerEnter2D(other);
        if (other.tag.Equals("Laser"))
        {
            Instantiate(explsionPrefab, other.transform.position, Quaternion.identity);
        }
    }

    protected override void Update()
    {
        base.MoveControl();

        MoveControlE2();
    }

    void MoveControlE2()//이동관리
    {
        if (transform.position.x < -3.5)
            direction = new Vector2(1, 0);
        else if (transform.position.x > 3.5)
            direction = new Vector2(-1, 0);
    }

    IEnumerator ReSpawn()
    {   //공격패턴코루틴
        while (HP > 0)
        {
            yield return new WaitForSeconds(0.5f);//1초 기다림
            randomT = Random.Range(1, 10);//난수생성
            if (randomT <= 5)
            {
                StartCoroutine(AttackType1());
            }
            if (randomT >= 6)
            {
                AttackType3();
            }
            if (HP < 20 && randomT == 10)
            {
                AttackType2();
            }
            yield return new WaitForSeconds(0.52f);
        }
        StartCoroutine(DestroyBoss());
    }

    IEnumerator AttackType1()
    {//작은거 4개
        for (int i = 0; i < 5; i++)
        {
            Instantiate(smallShot, new Vector3(this.transform.position.x + (-2.1f), this.transform.position.y - 3f, 0f), Quaternion.identity);
            Instantiate(smallShot, new Vector3(this.transform.position.x + (-1.3f), this.transform.position.y - 2f, 0f), Quaternion.identity);
            Instantiate(smallShot, new Vector3(this.transform.position.x + 1.49f, this.transform.position.y - 2f, 0f), Quaternion.identity);
            Instantiate(smallShot, new Vector3(this.transform.position.x + 2.3f, this.transform.position.y - 3f, 0f), Quaternion.identity);
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator AttackType4()
    {//큰거 1개
        while (HP > 0)
        {
            Instantiate(bigShot, new Vector3(this.transform.position.x + 0f, this.transform.position.y + 4.5f, 0f), Quaternion.identity);
            yield return new WaitForSeconds(3f);
        }
    }

    void AttackType2()
    {//큰거 1개
        
        Instantiate(bigShot, new Vector3(this.transform.position.x + 0f, this.transform.position.y + 4.5f, 0f), Quaternion.identity);
        
    }

    void AttackType3()
    {//유도탄 4개
        Instantiate(sideShot, new Vector3(this.transform.position.x + (-2.1f), this.transform.position.y - 3f, 0f), Quaternion.identity);
        Instantiate(sideShot, new Vector3(this.transform.position.x + (-1.3f), this.transform.position.y - 2f, 0f), Quaternion.identity);
        Instantiate(sideShot, new Vector3(this.transform.position.x + 1.49f, this.transform.position.y - 2f, 0f), Quaternion.identity);
        Instantiate(sideShot, new Vector3(this.transform.position.x + 2.3f, this.transform.position.y - 3f, 0f), Quaternion.identity);
    }

    IEnumerator DestroyBoss()   //파괴 이팩트
    {
        StartCoroutine(PlayFadeOut());
        Instantiate(explsionPrefab, new Vector3(-2.5f, 3.8f), Quaternion.identity);
        Instantiate(explsionPrefab, new Vector3(-0.8f, 3.5f), Quaternion.identity);
        Instantiate(explsionPrefab, new Vector3(-1.4f, 4.4f), Quaternion.identity);
        Instantiate(explsionPrefab, new Vector3(1.5f, 3.6f), Quaternion.identity);
        yield return new WaitForSeconds(1.2f);
        Instantiate(explsionPrefab, new Vector3(-2.3f, 4f), Quaternion.identity);
        Instantiate(explsionPrefab, new Vector3(-0.6f, 3.2f), Quaternion.identity);
        Instantiate(explsionPrefab, new Vector3(-1f, 4.4f), Quaternion.identity);
        Instantiate(explsionPrefab, new Vector3(1f, 3.6f), Quaternion.identity);
        yield return new WaitForSeconds(1.2f);
        Instantiate(explsionPrefab, new Vector3(-2.4f, 3.7f), Quaternion.identity);
        Instantiate(explsionPrefab, new Vector3(-0.3f, 3.1f), Quaternion.identity);
        Instantiate(explsionPrefab, new Vector3(2f, 3.6f), Quaternion.identity);
        Instantiate(explsionPrefab, new Vector3(0.8f, 4.6f), Quaternion.identity);
        yield return new WaitForSeconds(1.2f);
        Destroy(this.gameObject);
    }

    IEnumerator PlayFadeOut()   //페이드 아웃
    {
        // 애니메이션 재생중.  
        isPlaying = true;
        Color color = fadeRenderer.color;
        color.a = Mathf.Lerp(start, end, time);

        while (color.a > 0f)
        {
            Debug.Log(color.a);
            // 경과 시간 계산.  
            time += Time.deltaTime / fadeTime;

            // 알파 값 계산.  
            color.a = Mathf.Lerp(start, end, time);
            // 계산한 알파 값 다시 설정.  
            fadeRenderer.color = color;

            yield return null;
        }
        // 애니메이션 재생 완료.  
        isPlaying = false;
    }

    private void OnDestroy()
    {
        gameManager.Instance.AddScore(score);
        gameManager.Instance.EXPUP(EXP);
        spawnManager.Instance.enableSpawn = true;
    }
}
