using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EpochController : MonoBehaviour {

    public static EpochController instance;
    public int currentTime;

    private void Awake()
    {
        instance = this;
    }
    void Start () {
		
	}
	
	void FixedUpdate () {

        System.DateTime epochStart = new System.DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
        currentTime = (int)(System.DateTime.UtcNow - epochStart).TotalSeconds;
        
    }
}
