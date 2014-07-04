using UnityEngine;

[RequireComponent(typeof(PlatformerCharacter2D))]
public class Platformer2DUserControl : MonoBehaviour 
{
	private PlatformerCharacter2D character;
    private bool jump;
	InputVCR vcr;
	bool useVCR = false;
	bool isPlayer = false;

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

		if (vcr.GetButtonDown("Jump")) 
			jump = true;

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

		// Pass all parameters to the character control script.
		character.Move( h, crouch , jump );

        // Reset the jump input once it has been used.
	    jump = false;
	}
}
