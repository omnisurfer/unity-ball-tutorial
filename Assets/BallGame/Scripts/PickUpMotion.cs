using UnityEngine;
using System.Collections;

public class PickUpMotion : MonoBehaviour {

    public GameObject ground;
    
    private Vector3 initPosition = new Vector3(0.0f, 0.0f, 0.0f);
    private Vector3 movementAmplitude = new Vector3(6.0f, 0.0f, 6.0f);
    private Vector3 rotationCenter = new Vector3(0.0f, 0.0f, 0.0f);

    private float xFrequency = 0.05f;

    private float startAngle = 0.0f;
    
    private void Start()
    {                      
        initPosition = transform.position;

        startAngle = Mathf.Atan2(initPosition.x, initPosition.z);
    }

    // Before rendering each frame..
    void Update () 
	{
        // Rotate the game object that this script is attached to by 15 in the X axis,
        // 30 in the Y axis and 45 in the Z axis, multiplied by deltaTime in order to make it per second
        // rather than per frame.
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime * 10.0f, Space.Self);

        // create a rotation effect centered around world 0,0.5,0
        float time = Time.time;

        Vector3 newPosition = new Vector3 (
            Mathf.Sin((2.0f * Mathf.PI * xFrequency * time) + startAngle) * movementAmplitude.x + rotationCenter.x,            
            Mathf.Cos((2.0f * Mathf.PI * xFrequency * 3.0f * time) + initPosition.y) * movementAmplitude.y + 0.5f + rotationCenter.y,
            Mathf.Cos((2.0f * Mathf.PI * xFrequency * time) + startAngle) * movementAmplitude.z + rotationCenter.z
            );

        transform.position = newPosition;
        
        //Debug.Log("xPos: " + xPos + ", count: " + time);
    }
}	