using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    [SerializeField]
    GameObject tutorialEnemy;
    [SerializeField]
    private Text tutorialText;
    [SerializeField]
    private GameObject explainImage;
    [SerializeField]
    private GameObject chinoImage;
   [SerializeField]
    private GameObject kokoroImage;
    GameObject enemy;


    int index;
    public void Start()
    {
        index = 0;
        explainImage.SetActive(false);
        chinoImage.SetActive(false);
        kokoroImage.SetActive(false);
        TutorialText();
    }

    void TutorialText()
    {
        if (index == 0)
            tutorialText.text = "환영합니다";
        else if (index == 1)
        {
            tutorialText.text = "손위치를 다음과 같이 왼손엄지가 스페이스바를\n누르도록 하면 편하게 플래이할 수 있습니다.";
            explainImage.SetActive(true);
        }
        else if (index == 2)
        {
            explainImage.SetActive(false);
            tutorialText.text = "좌우 방향키로 이동할 수 있습니다.";
        }
        else if (index == 3)
        {
            tutorialText.text = "스페이스바로 공격하여 적을 처치하세요";
            enemy = Instantiate(tutorialEnemy);
            enemy.transform.position = new Vector3(0, 4);
        }
        else if (index == 4 && enemy != null)
            index = 3;
        else if (index == 4 && enemy)
            tutorialText.text = "q,w,e로 게이지를 소비하여 스킬을 사용합니다";
        else if (index == 5)
            tutorialText.text = "q스킬은 100게이지를 소비하여 베리어로 3초동안 1회 공격을\n막아주는 베리어를 생성합니다";
        else if (index == 6)
            tutorialText.text = "w스킬은 225게이지를 소비하여 화면에 있는 보스를 제외한 모든 적을 처치합니다\n마나1레벨이상이 필요합니다";
        else if (index == 7)
            tutorialText.text = "e스킬은 50게이지를 소비하여 적을 관통하는 총알로 3초동안 총알을 교체합니다.";
        else if (index == 8)
            tutorialText.text = "1,2,3 버튼으로 좌측에 스텟을 분배 할 수 있습니다";
        else if (index == 9)
            tutorialText.text = "1버튼은 공격력증가로 공격력을 1 증가시킵니다\n 2000점마다 체력이 오르는 적들을 처치하기 쉽게 만듭니다";
        else if (index == 10)
            tutorialText.text = "2버튼은 공격속도증가로 공격속도를 증가시킵니다 ";
        else if (index == 11)
            tutorialText.text = "3버튼은 마나로 최대 게이지를 25증가시키고 게이지 충전속도를 증가시킵니다";
        else if (index == 12)
        {
            gameManager.Instance.statPoint = 15;
            tutorialText.text = "스텟을 분배하고 직접 테스트 하여 보세요";
        }
        else if (index == 13 && gameManager.Instance.statPoint != 0)
            index = 12;
        else if (index == 13 && gameManager.Instance.statPoint == 0)
            tutorialText.text = "적군에는 보스, 근거리 공격몹, 원거리 공격몹이있습니다\n 점수가 오를때마다 점점 강해집니다";
        else if (index == 14)
            tutorialText.text = "계정이없다면 학번 이름으로 회원가입후\n 로그인하여 게임을 즐기세요";
        else if (index == 15)
            tutorialText.text = "x버튼을 누르면 시작화면으로 돌아갑니다";
        else if (index == 59)
            tutorialText.text = "동아리 만들때는 믿을 만한 사람을 동아리 회원으로!\n by 김진열";
        else if (index == 60)
            tutorialText.text = "야동사이트는 pornhub by 김종혁";
        else if (index == 100)
        {
            tutorialText.text = "코코로짱 최고다!!\n! by 남승우";
            kokoroImage.SetActive(true);
        }
        else if (index == 101)
        {
            kokoroImage.SetActive(false);
            tutorialText.text = "치노짱 최고다!!\n by 김진열";
            chinoImage.SetActive(true);
        }
    }

    public void NextText()
    {
        index++;
        StartCoroutine(NexxtText());
    }

    IEnumerator NexxtText()
    {
        TutorialText();
        yield return new WaitForSeconds(1f);
        StopCoroutine(NexxtText());
    }
    
    public void MoveLobi()
    {
        SceneManager.LoadScene("StartScene");
    }

}
