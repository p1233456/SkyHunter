using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : Enemy {
	public GameObject bigShot;

    private void Start()
    {
        moveSpeed = 2.5f;
        hP = gameManager.Instance.Score / 2000 + 1;
        direction = new Vector2(1, 0);
        StartCoroutine("ReSpawn");
        EXP = 1;
    }

    protected override void Update() {
        base.Update();

		MoveControlE2();
	}

	void MoveControlE2()//이동관리
	{
		if (transform.position.x < -3.5)
			direction = new Vector2(1,0);
		else if (transform.position.x > 3.5)
			direction = new Vector2(-1,0);
	}

	IEnumerator ReSpawn(){//총알 발사 함수
        yield return new WaitForSeconds(1f);
		while (HP>0){
			Instantiate(bigShot, new Vector3(this.transform.position.x + 0f ,this.transform.position.y + 0f, 0f), Quaternion.identity);
			yield return new WaitForSeconds (3f);
		}
		Destroy (this.gameObject);
	}
}
