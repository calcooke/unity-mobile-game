using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyC : Enemy
{

    public EnemyC() : base(0.07f, 1f, 10, 3) //Speed, Distance, Health, Score
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
