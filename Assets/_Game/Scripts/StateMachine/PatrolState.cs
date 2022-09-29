using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IState
{

    float randomTime;
    float timer;

    // khoi tao thoi gian di chuyen
    public void OnEnter(EnemyController enemy)
    {

        timer = 0;
        randomTime = Random.Range(2f, 5f);
    }

    // xu ly di chuyen
    public void OnExecute(EnemyController enemy)
    {

        timer += Time.deltaTime;

        if (enemy.Target != null)
        {
            // kiem tra huong enemy va tartget(player)
            enemy.ChangeDir(enemy.Target.transform.position.x < enemy.transform.position.x);
            // kiem tra 
            if(enemy.IsTargetInRangeDamge())
            {
                enemy.ChangeState(new AttackState());
            }
            else
            {
            enemy.Moving();

            }
        }
        else
        {
            if (timer < randomTime)
            {
                enemy.Moving();
            }
            else
                enemy.ChangeState(new IdleState());
        }
    }

    public void OnExit(EnemyController enemy)
    {

    }


}
