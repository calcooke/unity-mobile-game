using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{

    public GameObject hero;


    void LateUpdate()
    {
        float newXPosition = hero.transform.position.x;
        float newYPosition = hero.transform.position.y;

        transform.position = new Vector3(newXPosition, newYPosition + 0.2f, transform.position.z);  
    }																							
}
