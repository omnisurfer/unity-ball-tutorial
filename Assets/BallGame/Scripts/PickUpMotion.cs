using UnityEngine;
using System.Collections;

public class PickUpMotion : MonoBehaviour {

    public GameObject ground;
    public GameObject pickUp;

    private Vector3 groundCenterReference;

    private float xFrequency = 0.05f;
    private float xCenter = 0.0f, yCenter = 0.0f;
    private float xAmp = 6.0f;
    private float yAmp = 6.0f;

    private float xInit = 0.0f;
    private float yInit = 0.0f;

    private float startAngle = 0.0f;
    
    private void Start()
    {           
        Debug.Log(" lossScale.x " + ground.transform.lossyScale.x + " transform.position.x: " + transform.position.x);

        xInit = transform.position.x;
        yInit = transform.position.z;

        startAngle = Mathf.Atan2(yInit, xInit);
    }

    // Before rendering each frame..
    void Update () 
	{
        // Rotate the game object that this script is attached to by 15 in the X axis,
        // 30 in the Y axis and 45 in the Z axis, multiplied by deltaTime in order to make it per second
        // rather than per frame.
        float time = Time.time;
        float xPos = Mathf.Sin((2.0f * Mathf.PI * xFrequency * time) + startAngle) * xAmp + xCenter;
        float yPos = Mathf.Cos((2.0f * Mathf.PI * xFrequency * time) + startAngle) * yAmp + yCenter;
        float zPos = Mathf.Cos((2.0f * Mathf.PI * xFrequency * time) * yAmp) + 1.0f;

        transform.Rotate (new Vector3 (15, 30, 45) * Time.deltaTime, Space.Self);

        /*
        transform.Translate(xPos,
            0.0f,
            yPos,
            //Mathf.Cos((1.0f * Mathf.PI / 180.0f) * Time.deltaTime)
            Space.World);
        */
        //transform.Translate(new Vector3 (0.1f, 0.0f, 0.0f) * Time.deltaTime, Space.World);

        transform.position = new Vector3(xPos, zPos, yPos);
        
        //Debug.Log("xPos: " + xPos + ", count: " + time);
    }
}	