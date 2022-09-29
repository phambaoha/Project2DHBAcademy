using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{

    // bat dau su kien
    void OnEnter(EnemyController enemy);

    // xu ly di chuyen, ...
    void OnExecute(EnemyController enemy);

    // thoat su kien
    void OnExit(EnemyController enemy);



}
