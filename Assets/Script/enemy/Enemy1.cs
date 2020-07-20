using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : Enemy {
    
    private void Start()
    {
        transform.rotation = Quaternion.Euler(0, 0, 180);
        direction = new Vector2(0, 1);
        score = 50;
        moveSpeed = (gameManager.Instance.Score / 1000 + 1) * 1.3f;
        hP = gameManager.Instance.Score / 2000 + 1;
        EXP = 1;
    }
}
