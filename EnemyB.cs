using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyB : Enemy {

    public EnemyB() : base(0.04f, 1f, 7, 2) //Speed, Distance, Health, Score
    {

    }
   
    new void Start()
    {
        base.Start();
        
    }


    new void Update()
    {
        base.Update();
    }
}
