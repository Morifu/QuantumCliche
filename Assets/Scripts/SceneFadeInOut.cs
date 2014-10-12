using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SceneFadeInOut : MonoBehaviour
{
	public float fadeSpeed = 1.5f;          // Speed that the screen fades to and from black.
	public bool followCamera = false;
	
	private bool sceneStarting = true;      // Whether or not the scene is still fading in.
	
	
	void Awake ()
	{
		// Set the texture so that it is the the size of the screen and covers it.
		guiTexture.pixelInset = new Rect(0f, 0f, Screen.width, Screen.height);
	}
	
	
	void Update ()
	{

		if (followCamera)
		{
			Vector3 temp = Camera.main.ScreenToWorldPoint (transform.position);
			guiTexture.pixelInset = new Rect(temp.x,temp.y,Screen.width, Screen.height);

		}
		// If the scene is starting...
		if(sceneStarting)
			// ... call the StartScene function.
			StartScene();
	}
	
	
	void FadeToClear ()
	{
		// Lerp the colour of the texture between itself and transparent.
		Color tempColor = guiTexture.color;
		tempColor.a = Mathf.Lerp(guiTexture.color.a, 0, fadeSpeed * Time.deltaTime);
		guiTexture.color = tempColor;
	}
	
	
	IEnumerator FadeToBlack (int scene)
	{
		// Make sure the texture is enabled.
		guiTexture.enabled = true;
		Color tempColor = guiTexture.color;
		// Lerp the colour of the texture between itself and black.
		while (guiTexture.color.a < 0.45f)
		{
			tempColor.a = Mathf.Lerp(guiTexture.color.a, 0.5f, fadeSpeed * Time.deltaTime);
			guiTexture.color = tempColor;
			yield return null;
		}
		// If the screen is almost black...
		if(guiTexture.color.a >= 0.45f)
		{
			sceneStarting = true;
			Application.LoadLevel(scene);
		}
	}
	
	
	void StartScene ()
	{
		// Fade the texture to clear.
		FadeToClear();
		
		// If the texture is almost clear...
		if(guiTexture.color.a <= 0.05f)
		{
			// ... set the colour to clear and disable the GUITexture.
			guiTexture.color = Color.clear;
			guiTexture.enabled = false;
			
			// The scene is no longer starting.
			sceneStarting = false;
		}
	}
	
	
	public void EndScene (int scene)
	{
		
		// Start fading towards black.
		StartCoroutine (FadeToBlack(scene));
		

	}
}