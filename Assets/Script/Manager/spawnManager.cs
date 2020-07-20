using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnManager : MonoBehaviour {
	public bool enableSpawn = true;
	public int randomT;
	private bool isBoss=true;	//보스 생성 유무

    [SerializeField]
    private GameObject Enemy; //Prefab을 받을 public 변수 입니다.
    [SerializeField]
    private GameObject Enemy2;
    [SerializeField]
	private GameObject Boss;
    private static spawnManager instance; //싱글톤선언
    public static spawnManager Instance
    {
        get
        {
            if (!instance)
            {
                instance = GameObject.FindObjectOfType<spawnManager>();
            }

            return instance;
        }
    }
    void Awake(){
		enableSpawn = true;
		isBoss=true;	//보스 생성 유무        
	}

	void Start () {
        StartCoroutine(SpawnEnemy()); //3초후 부터, SpawnEnemy함수를 1초마다 반복해서 실행 시킵니다.
	}

	IEnumerator SpawnEnemy()
	{
        yield return new WaitForSeconds(3f);
        while (!gameManager.Instance.gameOver)
        {
            if (gameManager.Instance.Score >= 4000 && isBoss)
            {//4000점에서 보스 스폰
                GameObject[] findObject = GameObject.FindGameObjectsWithTag("Enemy");
                for (int i = 0; i < findObject.Length; i++)
                {
                    Debug.Log(findObject[i].name);
                    Destroy(findObject[i]);
                }
                Instantiate(Boss, new Vector3(0, 4.8f, 0f), Quaternion.identity);
                isBoss = false;
                enableSpawn = false;
                StartCoroutine(UIManager.Instance.BossUI());
            }
            if (enableSpawn)
            {
                randomT = Random.Range(1, 8);

                if (randomT < 5 && (gameManager.Instance.Score > 400))
                {//3마리
                    float randomX = Random.Range(-1.5f, 1.5f); //적이 나타날 X좌표를 랜덤으로 생성해 줍니다.
                    Type2(randomX);
                }
                else if ((randomT == 5 || randomT == 6) && (gameManager.Instance.Score > 800))
                {//5마리
                    float randomX = Random.Range(-0.5f, 0.5f); //적이 나타날 X좌표를 랜덤으로 생성해 줍니다.
                    Type3(randomX);
                }
                else if (randomT == 7 && (gameManager.Instance.Score > 2000))
                {
                    float randomX = Random.Range(-0.5f, 0.5f); //적이 나타날 X좌표를 랜덤으로 생성해 줍니다.
                    Type4(randomX);
                }
                else
                {//1마리
                    float randomX = Random.Range(-2.5f, 2.5f); //적이 나타날 X좌표를 랜덤으로 생성해 줍니다.
                    Type1(randomX);
                }
            }
            yield return new WaitForSeconds(1f - (0.1f * (gameManager.Instance.Score / 4000)));
        }
	}

	void Type1 (float randomX){//1마리 소환
		Instantiate(Enemy, new Vector3(randomX , 4.5f, 0f), Quaternion.identity);
	}
	void Type2 (float randomX){//3마리 소환
		Instantiate(Enemy, new Vector3(randomX+1 , 4.5f, 0f), Quaternion.identity);
		Instantiate(Enemy, new Vector3(randomX , 4.5f, 0f), Quaternion.identity);
		Instantiate(Enemy, new Vector3(randomX-1 , 4.5f, 0f), Quaternion.identity);
	}
	void Type3 (float randomX){//5마리 소환
		Instantiate(Enemy, new Vector3(randomX-2 , 4.5f, 0f), Quaternion.identity);
		Instantiate(Enemy, new Vector3(randomX-1 , 4.5f, 0f), Quaternion.identity);
		Instantiate(Enemy, new Vector3(randomX , 4.5f, 0f), Quaternion.identity);
		Instantiate(Enemy, new Vector3(randomX+1 , 4.5f, 0f), Quaternion.identity);
		Instantiate(Enemy, new Vector3(randomX+2 , 4.5f, 0f), Quaternion.identity);
	}
	void Type4 (float randomX){//1마리 소환
		Instantiate(Enemy2, new Vector3(randomX , 4.5f, 0f), Quaternion.identity);
	}
}
