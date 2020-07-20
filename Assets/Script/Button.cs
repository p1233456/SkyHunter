using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour {
    /*bool tfhundred=false;			//1500점 지났는지 판단
	bool ttousand=false;				//3000점 지났는지 판단

	void Awake(){
		tfhundred=false;			//1500점 지났는지 판단
		ttousand=false;
	}

	void Update(){
		if (gameManager.ReScore () >= 1500 && !tfhundred) {//1500에서 렙업
			tfhundred = true;
		}
		if (gameManager.ReScore () >= 3000 && !ttousand) {//3000점에서 렙업
			ttousand = true;
		}
	}*/

    public void AttackSpeed(){
		gameManager.Instance.player.shootDelay -= 0.1f;
		Time.timeScale = 1f;
	}

	public void Damage(){
		gameManager.Instance.player.damage += 1;
		Time.timeScale = 1f;
	}

	public void ChargeSpeed(){
		gameManager.Instance.gageSpeed -= 0.4f;
		Time.timeScale = 1f;
	}
}
