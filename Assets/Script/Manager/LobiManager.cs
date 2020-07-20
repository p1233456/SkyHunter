using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LobiManager : MonoBehaviour
{
    private static LobiManager instance; //싱글톤선언
    public static LobiManager Instance
    {
        get
        {
            if (!instance)
            {
                instance = GameObject.FindObjectOfType<LobiManager>();
            }

            return instance;
        }
    }

    [SerializeField]
    Text guideText;

    [SerializeField]
    GameObject gameName;

    public bool logined;

    private void Start()
    {
        DataManager.Instance.ResetUser();
        Screen.SetResolution(1600, 900, true);
        if (DataManager.Instance.userName != null)
            logined = true;
        else
            logined = false;
        gameName.SetActive(true);
    }

    public void StarGame()
    {
            SceneManager.LoadScene("GameScene");
            /*new LogEventRequest().SetEventKey("LOAD_HIGHSCORE").Send((response) => {
                if (!response.HasErrors)
                {
                    Debug.Log("게임스파크로 부터 데이터 로드");

                    // 전달 받은 데이터를 저장한다.
                    GSData data = response.ScriptData.GetGSData("gameData");

                    DataManager.Instance.highScore = data.GetInt("highScore").Value;
                    Debug.Log(data.GetInt("highScore").Value);
                    Debug.Log(DataManager.Instance.highScore.ToString());
                }
                else
                {
                    Debug.Log("Error Loading Player Data..." + response.Errors.JSON);
                }
            });*/
    }

   
    
    public void TutorialScene()
    {
        SceneManager.LoadScene("TutorialScene");
    }
}
