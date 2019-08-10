using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyA : Enemy {

   
    public EnemyA (): base(0.01f, 2f, 3, 1) //Speed, Distance, Health, Score
    {

    }
    
    new void Start()
    {   
        base.Start();
    }   

	new void Update ()
    {
        base.Update();
	}
}
