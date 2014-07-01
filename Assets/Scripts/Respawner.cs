using UnityEngine;
using System.Collections;

public class Respawner : MonoBehaviour {

	public Transform spawnPoint;

	// Use this for initialization
	void Start () {
		if(spawnPoint == null)
			spawnPoint.position = Vector3.zero;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		other.transform.position = spawnPoint.position;
	}
}
