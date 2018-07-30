using UnityEngine;

// Include the namespace required to use Unity UI
using UnityEngine.UI;

using System.Collections;

public class PlayerController : MonoBehaviour {
	
	// Create public variables for player speed, and for the Text UI game objects
	public float xySpeed;
    public float zSpeed;
    public float jumpAcceleration;
    public float jumpImpulseTime_s;    

	public Text countText;
	public Text winText;

    private GameObject[] pickUps;

    // Create private references to the rigidbody component on the player, and the count of pick up objects picked up so far
    private Rigidbody rb;
	private int count;

    private bool jump = false;
    private float moveZ = 0.0f;

	// At the start of the game..
	void Start ()
	{
		// Assign the Rigidbody component to our private rb variable
		rb = GetComponent<Rigidbody>();

        // get a reference to the pickUps
        pickUps = GameObject.FindGameObjectsWithTag("Pick Up");

        // Set the count to zero 
        count = 0;

		// Run the SetCountText function to update the UI (see below)
		SetCountText ();

		// Set the text property of our Win Text UI to an empty string, making the 'You Win' (game over message) blank
		winText.text = "";                        
	}

	// Each physics step..
	void FixedUpdate ()
	{
		// Set some local float variables equal to the value of our Horizontal and Vertical Inputs
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
        
        float distanceToGround = GetComponent<Collider>().bounds.extents.y;

        bool playerIsGrounded = Physics.Raycast(transform.position, Vector3.down, distanceToGround + 0.1f);
        
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Jump")) && playerIsGrounded)
        {
            jump = true;
        }

        if(jump)
        {
            moveZ = rb.mass * jumpAcceleration * jumpImpulseTime_s;

            Debug.Log("moveZ: " + moveZ);
            Debug.Log("moveHorizontal: " + moveHorizontal);
            Debug.Log("moveVertical: " + moveVertical);

            jump = false;
        }
        else
        {
            moveZ = 0.0f;
        }

        // Create a Vector3 variable, and assign X and Z to feature our horizontal and vertical float variables above
        Vector3 movement = new Vector3 (moveHorizontal, moveZ, moveVertical);

        // Add a physical force to our Player rigidbody using our 'movement' Vector3 above, 
        // multiplying it by 'speed' - our public player speed that appears in the inspector
        rb.AddForce(movement.x * xySpeed, moveZ * zSpeed, movement.z * xySpeed);

        // check if player has fallen off into the void
        if(rb.position.y < -10.0f)
        {
            // reset position
            rb.position = new Vector3(0.0f, 1.0f, 0.0f);

            // negate forces
            rb.velocity = Vector3.zero;
            // rb.angularVelocity = Vector3.zero;
        }

	}

	// When this game object intersects a collider with 'is trigger' checked, 
	// store a reference to that collider in a variable named 'other'..
	void OnTriggerEnter(Collider other) 
	{
		// ..and if the game object we intersect has the tag 'Pick Up' assigned to it..
		if (other.gameObject.CompareTag ("Pick Up"))
		{
			// Make the other game object (the pick up) inactive, to make it disappear
			other.gameObject.SetActive (false);

			// Add one to the score variable 'count'
			count = count + 1;

			// Run the 'SetCountText()' function (see below)
			SetCountText ();
		}
	}

	// Create a standalone function that can update the 'countText' UI and check if the required amount to win has been achieved
	void SetCountText()
	{
		// Update the text field of our 'countText' variable
		countText.text = "Count: " + count.ToString ();

        // Check if our 'count' is equal to or exceeded 12
        //int quantity = pickUps.Length;

        if (count == pickUps.Length)
        {
            // Set the text value of our 'winText'
            winText.text = "You Win!";
            count = 0;
            countText.text = "Count: " + count.ToString();
            ResetPickUps();
        }
        else
        {
            winText.text = "";
        }
    }

    void ResetPickUps()
    {
        foreach (var pickUp in pickUps)
        {
            Debug.Log("pickUps name: " + pickUp.name);
            pickUp.SetActive(true);

            rb.position = new Vector3(0.0f, 0.5f, 0.0f);
        }        
    }
}