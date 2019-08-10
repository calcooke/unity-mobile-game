using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IKillable
{

    protected float speed;
    protected float distance;
    public bool facingRight;
    private Transform heroTransform;
    protected Vector3 scale;
    protected int health;
    protected int score;


    public Enemy()
    {
        

    }

    public Enemy(float newSpeed, float newDistance, int newHealth, int newScore)
    {
        
        speed = newSpeed;
        distance = newDistance;
        health = newHealth;
        score = newScore;
        

    }
    

    protected void Start () {

        heroTransform = GetComponent<Transform>();
        StartCoroutine(move());  //This coroutine runs constantly setting the enemy facing left and right
        
    }
	
	
	protected void Update () {

        
        //This function is just moving the enemies left and right and flipping their sprites.

        if (facingRight == true)
        {   

            
            Vector3 movePos = heroTransform.position;
            movePos.x += speed;
            heroTransform.position = movePos;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
        else
        {                                   
            Vector3 movePos = heroTransform.position;
            movePos.x -= speed;
            heroTransform.position = movePos;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }

        if (health <= 0)
        {
            die();
        }


    }

    IEnumerator move()
    {
        while (true)
        { 

            facingRight = !facingRight;                                 
            yield return new WaitForSeconds(distance * Random.Range(3f, 4f));     
        }

    }

    //Reduce one health if hit by a spell

    void OnCollisionEnter2D(Collision2D collisionObject)
    {  
        if (collisionObject.gameObject.tag == "spell")
        {
            health -= 1;
            
        }
    }

    //Destroy the enemy and increase the score when they die

    public void die()
    {
        Destroy(gameObject);
        LevelManager.instance.addScore(score);
    }
}
