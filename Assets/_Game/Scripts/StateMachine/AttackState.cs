using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState
{
    float timer;

    // khoi tao thong so enemy
    public void OnEnter(EnemyController enemy)
    {

        timer = 0;
        if(enemy.Target != null)
        {
            enemy.StopMoving();
        }

        enemy.Attack();
    }

    // xu ly thong so
    public void OnExecute(EnemyController enemy)
    {
        timer += Time.deltaTime;
        if (timer >= 1.5f)
        {
            enemy.ChangeState(new PatrolState());
        }
    }

    public void OnExit(EnemyController enemy)
    {

    }
}
