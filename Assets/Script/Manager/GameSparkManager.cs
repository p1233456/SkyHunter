using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSparkManager : MonoBehaviour
{
    // 싱글톤
    private static GameSparkManager instance = null;
    void Awake()
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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
