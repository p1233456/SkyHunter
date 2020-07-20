using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boolete : MonoBehaviour {//보스,적 총알
	public GameObject ParticleFXExplosion;
	private const float moveSpeed = 4.8f;	//총알이 움직일 속도를 상수로 지정해줍시다.
	void Awake () {
		this.transform.rotation = Quaternion.Euler(0,0,180);
	}
	void Update () {
		float moveY = moveSpeed * Time.deltaTime;
		//이동할 거리를 지정해 줍시다.
		transform.Translate(0, moveY, 0);
		//이동을 반영해줍시다.
	}


	void OnBecameInvisible()
	{
		Destroy(this.gameObject);// 자기 자신을 지웁니다.
	}
}
