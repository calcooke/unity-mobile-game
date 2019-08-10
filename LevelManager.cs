using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {

    
    public int score;
    public Enemy[] enemies;
    public Text scoreCount;
    public Text enemyCount;
    public string enemyArrayLength;
    private float countdownTime;
    private Text remainingTime;
    public Image healthBar;
    public int heroHealth;
    float spawnRateA;
    float spawnRateB;
    float spawnRateC;
    public static LevelManager instance;

    private void Awake()
    {
        instance = this;
    }

  
    void Start () {
        setupScoreText();
        setupSpawnProperties();
        StartCoroutine(spawnEnemyA());
    }
	
	
	void FixedUpdate () {

        // Creating an array of all the Enemies.
        enemies = FindObjectsOfType(typeof(Enemy)) as Enemy[];
        enemyArrayLength = enemies.Length.ToString();
        addEnemyCount(enemies.Length.ToString());
        //enemies = FindObjectsOfType<Enemy>();
        //enemies = FindObjectsOfType(typeof(Enemy));
        
    }

   
    private void setupScoreText()
    {
        score = 0;
        scoreCount.text = "Score " + score;
        
    }

    private void setupSpawnProperties()
    {
        spawnRateA = 5;
        spawnRateB = 5;
        spawnRateC = 5;
        StartCoroutine(enemySpawnCountdown());
        
    }

    public void addScore(int addScore)
    {
        score += addScore;
        scoreCount.text = "Score: " + score;
    }

    public void addEnemyCount(string enemiesLength)
    {
       
        // Seting the enemy count text to be the length of the enemies aray
        enemyCount.text = "Enemies:  " + enemiesLength;
        
    }

    public void deductHealth()
    {
        // Hero has 10 health, so 0.1 is deducted from the health bar every hit
        healthBar.fillAmount -= 0.1f;  
    }

    //This coroutine just staggers the spawn rate of the more pawerful enemies. 
    
    private IEnumerator enemySpawnCountdown()
    {
        yield return new WaitForSeconds(30f);
        StartCoroutine(spawnEnemyB());
        yield return new WaitForSeconds(30f);
        StartCoroutine(spawnEnemyC());

    }

    private IEnumerator spawnEnemyA()
    {
        while (true)  //I want this coroutine to loop forever
        {
            
            EnemySpawner.instance.spawnA(spawnRateA);  //Spawn enemy of typeA 
            yield return new WaitForSeconds(spawnRateA); //Wait for the duration of the spawn rate to spawn again
            if (spawnRateA >= 1f)
            {
                spawnRateA -= 0.1f;  //Reduce the spawnrate slightly.
            }
            else
            {
                spawnRateA = 1f; //  I don't want spawns to get faster than a second.
            }
            
        }
        
    }

    private IEnumerator spawnEnemyB()
    {
        while (true)  //I want this coroutine to loop forever
        {

            EnemySpawner.instance.spawnB(spawnRateB);  //Spawn enemy of typeA 
            yield return new WaitForSeconds(spawnRateB); //Wait for the duration of the spawn rate to spawn again

            if (spawnRateB >= 1f) //If the spawn rate is greater than a second
            {
                spawnRateB -= 0.1f;  //Reduce the spawnrate slightly.
            }
            else
            {
                spawnRateB = 1f; //  I don't want spawns to get faster than a second.
            }

        }
    }

        private IEnumerator spawnEnemyC()
        {
            while (true)  //I want this coroutine to loop forever
            {

                EnemySpawner.instance.spawnC(spawnRateC);  //Spawn enemy of typeA 
                yield return new WaitForSeconds(spawnRateC); //Wait for the duration of the spawn rate to spawn again

                if (spawnRateC >= 1f) //If the spawn rate is greater than a second
            {
                    spawnRateC -= 0.1f;  //Reduce the spawnrate slightly.
                }
                else
                {
                    spawnRateC = 1f; //  I don't want spawns to get faster than a second.
            }

            }

        }


}
