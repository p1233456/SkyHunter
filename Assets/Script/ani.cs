using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ani : MonoBehaviour {

	void Start(){
		StartCoroutine ("Dest");
	}

	IEnumerator Dest(){//코루틴
		yield return new WaitForSeconds (1f);
		Destroy (this.gameObject);
	}
}
