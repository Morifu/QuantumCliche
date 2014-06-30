using UnityEngine;
using System.Collections;

public class Respawner : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		other.transform.position = Vector3.zero;
	}
}
