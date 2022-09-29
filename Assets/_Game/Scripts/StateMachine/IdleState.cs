using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{

    float ramdomTime;
    float timer;
    //khoi tao state

    public void OnEnter(EnemyController enemy)
    {
        enemy.StopMoving();
        timer = 0;
        ramdomTime = Random.Range(1f, 3f);
    }

    // xu ly state
    public void OnExecute(EnemyController enemy)
    {
       
        timer += Time.deltaTime;
        if (timer > ramdomTime)
        {     
            enemy.ChangeState(new PatrolState());
        }

    }
    public void OnExit(EnemyController enemy)
    {

    }


}
