using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpellManager : MonoBehaviour
{

    new private Renderer renderer;

    private bool seen;

    void Start()
    {

        renderer = GetComponent<Renderer>();   //Get the rendereder of the spell

        seen = false;  
    }

    void Update()
    {
        //Probably won't ever need this code as they get destroyed on impact
        if (renderer.isVisible)
        {  //If a spell has rendered
            seen = true;         
        }

        if ((renderer.isVisible == false) && (seen == true))
        {     //If you can't see the  spell anymore, but it has been seen, destroy it
            Destroy(gameObject);                                 
        }

    }


    void OnCollisionEnter2D(Collision2D collisionObject)
    {
        //Destroy the spell if it hits something.
        Destroy(gameObject);

    }

}
