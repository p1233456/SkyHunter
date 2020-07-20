using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideSho : MonoBehaviour
{
    private Rigidbody2D myRigidbody;

    [SerializeField]
    private float speed;

    private Transform target;


    // Use this for initialization
    void Start()
    {

        myRigidbody = GetComponent<Rigidbody2D>();
        target = GameObject.Find("Player").transform;
        Vector2 direction = target.position - transform.position;
        myRigidbody.velocity = direction.normalized * speed;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    protected void OnBecameInvisible()
    {
        Destroy(this.gameObject);// 자기 자신을 지웁니다.
    }
}
