using UnityEngine;

public class Platformer2DUserControl : MonoBehaviour 
{
	private PlatformerCharacter2D character;
    private bool jump;
	private bool canJump;
	InputVCR vcr;
	bool useVCR = false;
	bool isPlayer = false;
	bool jumped = true;
	bool justPressedW = false;
	[SerializeField] float shootingSpeed = 1.0f;
	float lastBulletShotAt = 0.0f;

	void Awake()
	{
		character = GetComponent<PlatformerCharacter2D>();
		vcr = GetComponent<InputVCR>();
		useVCR = vcr != null;
	
		if(useVCR)
		{
			if(gameObject.CompareTag("Player"))
				isPlayer = true;

			if(isPlayer && vcr.mode == InputVCRMode.Record)
				vcr.Record();
		}
	}

    void Update ()
    {
		if (vcr.GetKeyDown ("w"))
			justPressedW = true;
		if(vcr.GetKeyUp("w"))
			justPressedW = false;

		if(!isPlayer) return;

		if (Input.GetKey ("r"))
			vcr.Record ();

		if(Input.GetKey("t"))
			vcr.Stop();
		if(Input.GetKey("y"))
			vcr.Play();

    }

	void FixedUpdate()
	{
		// Read the inputs.
		bool crouch = Input.GetKey(KeyCode.LeftControl);
		float h = vcr.GetAxis("Horizontal");
		float v = vcr.GetAxis ("Vertical");
		if (justPressedW && !jumped)
		{
 			Debug.Log("Jumped!");
			jump = true;
			jumped = true;
		}

		if(jumped && character.canJump() && !justPressedW)
		{
			jumped = false;
		}

		if(vcr.GetMouseButton(0) && (Time.time - lastBulletShotAt > shootingSpeed) )
		{
			lastBulletShotAt = Time.time;
			character.Shoot();
		}
		// Pass all parameters to the character control script.
		character.Move( h, crouch , jump );

        // Reset the jump input once it has been used.
	    jump = false;
	}
}
