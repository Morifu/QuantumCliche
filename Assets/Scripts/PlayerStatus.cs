using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerStatus : MonoBehaviour {

	[Header("GUI bars")]
	public GameObject[] healthBar;
	public GameObject[] ammoBar;
	public GameObject[] armorBar;

	[Header("Time Bar")]
	public Text time;

	byte playerHealth = 4;
	byte playerAmmo = 3;
	byte playerArmor = 2;

	public byte health {
		set {playerHealth = value;}
		get {return playerHealth;}
	}

	public byte ammo {
		set {playerAmmo = value;}
		get {return playerAmmo;}
	}

	public byte armor {
		set {playerArmor = value;}
		get {return playerArmor;}
	}

	static PlayerStatus myself = null;
	public PlayerStatus instance {
		get{ return myself;}
		set{ 
			if(myself == null)
				myself = value;
			else
				GameObject.Destroy(value.gameObject);
		}
	}

	// Use this for initialization
	void Start () {
		instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		updateHealth (health);
		updateAmmo (ammo);
		updateArmor (armor);
		time.text = string.Format ("{0:F2}", Time.time);
	}

	void updateHealth(byte health)
	{
		healthBar [0].SetActive (health > 0);
		healthBar [1].SetActive (health > 1);
		healthBar [2].SetActive (health > 2);
		healthBar [3].SetActive (health > 3);
	}

	void updateAmmo(byte ammo)
	{
		ammoBar [0].SetActive (ammo > 0);
		ammoBar [1].SetActive (ammo > 1);
		ammoBar [2].SetActive (ammo > 2);
	}

	void updateArmor(byte armor)
	{
		armorBar [0].SetActive (armor > 0);
		armorBar [1].SetActive (armor > 1);
	}
}
