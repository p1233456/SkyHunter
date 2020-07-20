using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    protected void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Skill1();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            Skill2();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Skill3();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Skill4();
        }
    }

    protected virtual void Skill1()
    {

    }

    protected virtual void Skill2()
    {

    }

    protected virtual void Skill3()
    {

    }

    protected virtual void Skill4()
    {

    }
}
