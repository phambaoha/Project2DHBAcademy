using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class MushroomEnemy : EnemyController
{
   [SerializeField] Vector3[] paths;
    private void Start()
    {
        transform.DOPath(paths, 10f, PathType.Linear).SetEase(Ease.Linear).SetLoops(-1);
    }
    public override void OnInit()
    {
        Hp = 50;
    }
    private void Update()
    {
      
    }

}
