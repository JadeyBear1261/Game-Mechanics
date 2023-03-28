using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour {
public float speed = 10;		//how fast bullets fly
public int damage;				//how much health bullet takes off enemy
public GameObject target;		//target, startPosition, and targetPosition determine bullet direction
public Vector3 startPosition;
public Vector3 targetPosition;
private Vector3 normalizeDirection;    //standardize vectors, without this closer enemy bullets move faster than further away bullets
private GameManagerBehaviour gameManager;


	//Checks for collision wth enemy, but can be hit by any bullet not just from monster initially aiming.
	void OnTriggerEnter2D(Collider2D other)
	{
    target = other.gameObject;
		if(target.tag.Equals("Enemy"))
		{
			Transform healthBarTransform = target.transform.Find("HealthBar");		//Reduce health bar in hit enemy by damage taken
			HealthBar healthBar = healthBarTransform.gameObject.GetComponent<HealthBar>();
			healthBar.currentHealth -= damage;

			if (healthBar.currentHealth <= 0)		//If health is equal to or less than zero, destroy enemy
			{
				Destroy(target);
				AudioSource audioSource = target.GetComponent<AudioSource>();		//Play sound effect upon enemy destruction
				AudioSource.PlayClipAtPoint(audioSource.clip, transform.position);
				gameManager.Gold += 50;		//Increase player gold by 50
			}  
			Destroy(gameObject);		//Destroy bullet
		}
	}

	// Use this for initialization
	void Start () {
		//when new bullet instantiated normalize difference between targetPosition and startPosition to get standard 'direction' vector. Also reference GameManagerBehaviour
		normalizeDirection = (targetPosition - startPosition).normalized;
		GameObject gm = GameObject.Find("GameManager");
		gameManager = gm.GetComponent<GameManagerBehaviour>();
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += normalizeDirection * speed * Time.deltaTime;   //Updates bullet position along normalized vector according to speed variable
	}
}
