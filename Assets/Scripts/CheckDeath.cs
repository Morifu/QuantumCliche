using UnityEngine;
using System.Collections;

public class CheckDeath : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	void OnTriggerEnter2D(Collider2D other)
	{
		Debug.Log (other.gameObject.tag);
	}
}
