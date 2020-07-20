using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialEnemy : Enemy
{
    private void Start()
    {
        transform.rotation = Quaternion.Euler(0, 0, 180);
        hP = 1;
        EXP = 1;
    }
}
