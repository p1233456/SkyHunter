using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSupport : MonoBehaviour
{
    private void Start()
    {
        Screen.SetResolution(1600, 900, true);
    }

    public void ChangeGameScene()
    {
        SceneManager.LoadScene("GameScene");
    }
}
