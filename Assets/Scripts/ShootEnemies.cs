using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootEnemies : MonoBehaviour {
public List<GameObject> enemiesInRange;		//list to conain all enemies within range to monster


	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.tag.Equals("Enemy"))
			enemiesInRange.Add(other.gameObject);
	}


	void OnTriggerExit2D (Collider2D other)
	{
		if (other.gameObject.tag.Equals("Enemy"))
			enemiesInRange.Remove(other.gameObject);
	}


	// Use this for initialization
	void Start () {
		enemiesInRange = new List<GameObject>();  //Initializing enemy monster range list
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
