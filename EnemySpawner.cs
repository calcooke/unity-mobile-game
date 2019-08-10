using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public static EnemySpawner instance;
    public GameObject[] enemy;
    private float randX;
    Vector2 spawnLocation;
    public float spawnRate;
    float nextSpawn = 0;

    public float spawnRateA;
    public float spawnRateB;
    public float spawnRateC;


    private void Awake()
    {
        instance = this;
    }

    
    void Start () {

		
	}
	
	
	void Update () {

		
	}

    public void spawnA(float spawnRateA)
    {
        
        randX = Random.Range(-24, 24);     //Create a new random spawn point
        spawnLocation = new Vector2(randX, transform.position.y);
        // This spawns an random enemy from the array.
        Instantiate(enemy[0], spawnLocation, Quaternion.identity);
    }

    public void spawnB(float spawnRateB)
    {
        
        randX = Random.Range(-24, 24);     //Create a new random spawn point
        spawnLocation = new Vector2(randX, transform.position.y);
        // This spawns an random enemy from the array.
        Instantiate(enemy[1], spawnLocation, Quaternion.identity);
    }

    public void spawnC(float spawnRateB)
    {
        
        randX = Random.Range(-24, 24);     //Create a new random spawn point
        spawnLocation = new Vector2(randX, transform.position.y);
        // This spawns an random enemy from the array.
        Instantiate(enemy[2], spawnLocation, Quaternion.identity);
    }
}

