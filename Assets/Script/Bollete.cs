using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bollete : MonoBehaviour
{
    private const float moveSpeed = 10f;
    public int damage;
    private Enemy enemy;
    protected virtual void Update()
    {
        transform.Translate(0, moveSpeed * Time.deltaTime, 0);        //이동
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("Enemy"))
        {
            enemy = other.GetComponent<Enemy>();
            enemy.MHP(damage);
        }
    }

    protected void OnBecameInvisible()
    {
        Destroy(this.gameObject);// 자기 자신을 지웁니다.
    }
}
