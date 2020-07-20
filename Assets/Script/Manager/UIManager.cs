using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    private static UIManager instance; //싱글톤선언
    public static UIManager Instance
    {
        get
        {
            if (!instance)
            {
                instance = GameObject.FindObjectOfType<UIManager>();
            }

            return instance;
        }
    }


    [SerializeField]
    private CanvasGroup gameOverUI;          //게임오버 창
                                             //점수 관련 변수
    [SerializeField]
    private Text nowScore;      //현제 점수
    [SerializeField]
    private Text userName;  //이름
    [SerializeField]
    private Text iD;    //학번
    [SerializeField]
    private Text highScore;//최고 점수

    public Text scoreText;              //점수를 표시하는 Text객체를 에디터에서 받아옵니다.
    [SerializeField]
    private Image gageFill;      //게이지 양
    [SerializeField]
    private Text gageText;      //게이지텍스트
    [SerializeField]
    private float gageCurrentFill;  //현제 채워진 게이지
    [SerializeField]
    private Image experitionFill;  //경험치 양
    [SerializeField]
    private Text experitionText;    //경험치 텍스트
    [SerializeField]
    private float experitionCurrentFill;    //현재 채워진 경험치
    [SerializeField]
    private Text level;         //레벨 택스트
    [SerializeField]
    private Text statPoint;     //스텟 포인트 텍스트
    [SerializeField]
    private Image attackFill;   //공격 스탯 양
    [SerializeField]
    private Text attackText;    //공격 스탯 택스트
    [SerializeField]
    private Image attackSpeedFill;   //공격속도 스탯 양
    [SerializeField]
    private Text attackSpeedText;    //공격속도 택스트
    [SerializeField]
    private Image manaFill;   //마나 스탯 양
    [SerializeField]
    private Text manaText;    //마나 택스트
    [SerializeField]
    public CanvasGroup levelUPCanvasGroup; //레벨업 택스트
    [SerializeField]
    private Text bossHPText;
    [SerializeField]
    private Image bossHPFill;   //HP양
    [SerializeField]
    private CanvasGroup bossHP; //HP캔버스 그룹

    private Boss boss;

    private void Start()
    {
        bossHP.alpha = 0;
        boss = null;
        StatUIUpdate();
    }
    // Update is called once per frame
    void Update()
    {       
        GageUI();
        EXPUI();
        ScoreUI();
        if (gameManager.Instance.gameOver)//게임오버시 게임오버UI 작동
        {
            GameOver();
        }
        level.text = gameManager.Instance.Level.ToString() + " LV";
        statPoint.text = "잔여 스탯포인트 : " + gameManager.Instance.statPoint.ToString() + "점";
    }

    public void GameOver()
    {
        gameOverUI.alpha = 1;
        nowScore.text = "점수  :  " + gameManager.Instance.Score.ToString();
        highScore.text = "최고점수  :  " + DataManager.Instance.highScore.ToString();
        iD.text = DataManager.Instance.iD;
        userName.text = DataManager.Instance.userName;
        if (Input.GetMouseButtonDown(1))
        {
            gameOverUI.alpha = 0;
            SceneManager.LoadScene("StartScene");
        }
    }

    public void GageUI()
    {
        gageCurrentFill = gameManager.Instance.Gage/ (200 + gameManager.Instance.manaStat*25);
        gageText.text = gameManager.Instance.Gage + " / " + (200 + gameManager.Instance.manaStat * 25);
        if (gageCurrentFill != gageFill.fillAmount)
        {
            gageFill.fillAmount = Mathf.Lerp(gageFill.fillAmount, gageCurrentFill, 10);
        }
    }

    private void EXPUI()
    {
        experitionCurrentFill = gameManager.Instance.Experition / (gameManager.Instance.MaxExperition);
        experitionText.text = gameManager.Instance.Experition + " / " + (gameManager.Instance.MaxExperition);
        if (experitionCurrentFill != experitionFill.fillAmount)
        {
            experitionFill.fillAmount = Mathf.Lerp(experitionFill.fillAmount, experitionCurrentFill, 10);
        }
    }

    private void ScoreUI()
    {
        scoreText.text = gameManager.Instance.Score + "점";
    }
    
    public void StatUIUpdate()
    {
            attackFill.fillAmount = Mathf.Lerp(gageFill.fillAmount, gameManager.Instance.attackLevel / 5, 5);
            attackText.text = gameManager.Instance.attackLevel.ToString() + "Lv";
            attackSpeedFill.fillAmount = Mathf.Lerp(gageFill.fillAmount, gameManager.Instance.attackSpeedSatat / 5, 5);
            attackSpeedText.text = gameManager.Instance.attackSpeedSatat.ToString() + "Lv";
            manaFill.fillAmount = Mathf.Lerp(gageFill.fillAmount, gameManager.Instance.manaStat / 5, 5);
            manaText.text = gameManager.Instance.manaStat.ToString() + "Lv";
    }

    public IEnumerator LevelUPUI()
    {
        levelUPCanvasGroup.alpha = 1;
        yield return new WaitForSeconds(1);
        levelUPCanvasGroup.alpha = 0;
    }

    public IEnumerator BossUI()
    {
        boss = GameObject.FindObjectOfType<Boss>().GetComponent<Boss>();
        bossHP.alpha = 1;
        while (boss.HP > 0)
        {
            Debug.Log(boss.HP);   
            bossHPText.text = boss.HP + " / " + 50;
            if (boss.HP / 50 != bossHPFill.fillAmount)
            {
                bossHPFill.fillAmount = Mathf.Lerp(bossHPFill.fillAmount, boss.HP / 50, 10);
            }
            yield return new WaitForSeconds(0.1f);
        }
        bossHP.alpha = 0;
        yield return 0;
        StopCoroutine(BossUI());
    }
}
