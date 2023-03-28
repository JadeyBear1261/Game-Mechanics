using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour {
public float maxHealth = 100; 	//maximal health points
public float currentHealth = 100;		//how much current health
private float originalScale;		//remembers health bar original size


	// Use this for initialization
	void Start () {

		originalScale = gameObject.transform.localScale.x;
	}
	
	// Update is called once per frame
	void Update () {
		//this calculates the scale of health bar based on currentHealth/maxHealth
		Vector3 tmpScale = gameObject.transform.localScale;
		tmpScale.x = currentHealth / maxHealth * originalScale;
		gameObject.transform.localScale = tmpScale;
	}
}
