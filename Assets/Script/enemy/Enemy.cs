using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    protected float moveSpeed ;    //이동속도
    [SerializeField]
    protected float hP;              //체력
    public float HP
    {
        get
        {
            return hP;
        }
    }
    protected int score;            //처치시 점수
    protected Vector2 direction;    //이동방향
    protected int EXP;
    [SerializeField]
    protected GameObject explsionPrefab;

    public void MHP(int num)   //체력 감소 함수
    {
        hP -= num;      //체력을 감소
    }

    protected virtual void Update()
    {
        if (HP < 1)
        {//체력이 0이되면 파괴
            gameManager.Instance.AddScore(score);
            gameManager.Instance.EXPUP(EXP);
            Destroy(this.gameObject);
        }
        MoveControl();
    }

    protected void MoveControl()//이동 관리 함수
    {
        transform.Translate(direction * moveSpeed * Time.deltaTime);
    }

    void OnBecameInvisible()//화면에서 나갔을 때
    {
        Destroy(this.gameObject);// 자기 자신을 지웁니다.
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Laser"))
        {
            GameObject boom = Instantiate(explsionPrefab, other.transform.position, Quaternion.identity);
        }
    }
}
