using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hero : MonoBehaviour, IKillable
{


    public float speed;
    public float jumpForce = 240;
    Rigidbody2D theHero;
    public bool grounded;
    public Transform groundcheck;
    private float groundRadius = 0.2f;
    public LayerMask whatIsGround;
    public LayerMask whatIsEnemy;
    private bool running;
    private Animator heroAnimator;
    public bool facingRight = true;
    private float keyDirection;
    private float joyDirection;
    private Vector2 movement;
    private Vector2 hAxis;
    //private Transform currentPos;
    private float YJumpForce = 1f;
    public GameObject spellSprite;
    //public Transform spellCast;
    public Transform newSpellCast;
    
    private Vector2 impJump;
    private int heroHealth;
    private bool vulnerable;
    public string testString;
    



    void Start() {

        grounded = false;
        theHero = this.GetComponent<Rigidbody2D>();
        heroAnimator = GetComponent<Animator>();
        impJump = new Vector2(0, YJumpForce);
        heroHealth = 10;
        vulnerable = true;    
        LevelManager.instance.heroHealth = heroHealth;

    }


    private void Update()
    {
        // Detecting inputs from both the joystick and keyboard
        //Movement using the keypad is included for testing purposes

        movement = new Vector2(CrossPlatformInputManager.GetAxis("Horizontal"), 0);
        hAxis = new Vector2(Input.GetAxis("Horizontal"), 0);
        keyDirection = Input.GetAxis("Horizontal");
        joyDirection = CrossPlatformInputManager.GetAxis("Horizontal");

        //Jumping

        if (CrossPlatformInputManager.GetButtonDown("Jump") || Input.GetKeyDown("space"))
        {
            int addNum = 1;

            //For some reason the jump is smaller when using the spacebar, so I need to add to the jump value

            if (Input.GetKeyDown("space"))
            {

                addNum = 3;
            }


            jump(addNum);

        }

        if (CrossPlatformInputManager.GetButtonDown("Attack"))
        {

            Spell();

        }

        //Setting the running animation when there's movement

        if (CrossPlatformInputManager.GetAxis("Horizontal") != 0.0 || Input.GetAxis("Horizontal") != 0.0)
        {

            heroAnimator.SetBool("running", true);
        }
        else
        {
            heroAnimator.SetBool("running", false);

        }

        if (!grounded)
        {
            heroAnimator.SetBool("jumping", true);

        }
        else
        {
            heroAnimator.SetBool("jumping", false);
        }

        //Facing the hero sprite in the right direction 

        if ((keyDirection > 0 || joyDirection > 0) && (facingRight == false))
        {
            Flip();
        }
        else if ((keyDirection < 0 || joyDirection < 0) && (facingRight == true))
        {
            Flip();
        }

        if(heroHealth <= 0)
        {
            die();
        }

    }


    void FixedUpdate()
    {

        // Move the hero with keypad or joystick

        theHero.AddForce(movement);
        theHero.AddForce(hAxis * speed);

        //Checking if the player is grounded 

        Collider2D colliderWeCollidedWith = Physics2D.OverlapCircle(groundcheck.position, groundRadius, whatIsGround);
        Collider2D enemyWeCollidedWith = Physics2D.OverlapCircle(groundcheck.position, groundRadius, whatIsEnemy);
        grounded = (bool)colliderWeCollidedWith || (bool)enemyWeCollidedWith;

    }


    void jump(int addNum)
    {
        if (grounded == true) {

            //theHero.velocity += addNum * jumpForce * Vector2.up * Time.deltaTime;
            theHero.AddForce(impJump, ForceMode2D.Impulse);

        }
    }

    private void Flip()
    {

        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
        newSpellCast.localScale = theScale;
        
    }

    void Spell() {

        //Instantiating a spell prefeab and applying force to it

        
        GameObject spell = Instantiate(spellSprite, newSpellCast.position, newSpellCast.rotation);


        Rigidbody2D spellRB;
        spellRB = spell.GetComponent<Rigidbody2D>();


        if (facingRight)
        {
            spellRB.velocity = new Vector2(20, 0);
        }
        else
        {
            spellRB.velocity = new Vector2(-20, 0);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        //All of the below are attempts at detecting the collision object's type - couldn't get it to work

        //testString = collision.gameObject.GetType().ToString();
        //Debug.Log("Object type is " + testString);
        //collision.gameObject == typeof(Enemy);

        //if(collision.gameObject.GetType() is Enemy)
        //{
        //Debug.Log("Enemy hit detected");
        //}
        //for(int i = 0; i < LevelManager.instance.enemies.Length; i++)
        //{
        //if(LevelManager.instance.enemies[i] == collision.gameObject)
        //{
        //Debug.Log("Hit enemy from the enemy array");
        //}
        //}


        // The aim of the above attempts was to detect collision and damage the player though Polymorphism - if he hits an Enemy type,
        // but had to resort to using tags

        if (collision.gameObject.gameObject.tag == "enemy" && vulnerable == true) 
        {
            vulnerable = false;  //Make sure the hero can't be hurt immediatley after
            heroHealth -= 1;
            StartCoroutine(preventDamage());  //Makes hero invincible for a second before setting vulnerable to true
            LevelManager.instance.deductHealth();
        }

        
    }

    public void die()
    {
        //Reload the scene when you die
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);

    }

    //To ensure the Hero doesnt die by colliding with the enemies once every frame, I'm making
    //him invincible for a second so you can get away
    IEnumerator preventDamage()
    {
        yield return new WaitForSeconds(2f);
        vulnerable = true;
    }
}

