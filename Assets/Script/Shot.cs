using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    private const float moveSpeed = 10f;

    private void Start()
    {
        transform.Rotate(0, 0, 180);
    }

    protected virtual void Update()
    {
        transform.Translate(0, moveSpeed * Time.deltaTime, 0);        //이동
    }

    protected void OnBecameInvisible()
    {
        Destroy(this.gameObject);// 자기 자신을 지웁니다.
    }

}
