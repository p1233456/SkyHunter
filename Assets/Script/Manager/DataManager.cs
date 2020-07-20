using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

public class DataManager : MonoBehaviour
{
    private static DataManager instance; //싱글톤선언
    public static DataManager Instance
    {
        get
        {
            if (!instance)
            {
                instance = GameObject.FindObjectOfType<DataManager>();
            }

            return instance;
        }
    }
    [SerializeField]
    public string userName;
    [SerializeField]
    public string iD;
    [SerializeField]
    public int highScore;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void ResetUser()
    {
        userName = null;
        iD = null;
        highScore = 0;
    }

    
}
