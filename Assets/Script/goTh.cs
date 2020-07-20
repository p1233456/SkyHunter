using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goTh : MonoBehaviour {
    public GameObject ParticleFXExplosion;
    private const float moveSpeed = 10f;
    public int damage;
    private Enemy enemy;
    //총알이 움직일 속도를 상수로 지정해줍시다.

    void Update()
    {
        float moveY = moveSpeed * Time.deltaTime;
        //이동할 거리를 지정해 줍시다.
        transform.Translate(0, moveY, 0);
        //이동을 반영해줍시다.
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("Enemy") || other.tag.Equals("Boss"))
        {
            enemy = other.GetComponent<Enemy>();
            enemy.MHP(damage);
        }
    }

    void OnBecameInvisible()
    {
        Destroy(this.gameObject);// 자기 자신을 지웁니다.
    }
}

