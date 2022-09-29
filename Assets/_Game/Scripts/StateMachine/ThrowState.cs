using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowState : IState
{

    float timer;
    public void OnEnter(EnemyController enemy)
    {
        timer = 0;
        if (enemy.Target != null)
        {
            enemy.StopMoving();
        }

        enemy.Attack();
    }
    public void OnExecute(EnemyController enemy)
    {
       
    }
    public void OnExit(EnemyController enemy)
    {
       
    }

 
}
