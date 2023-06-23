using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour1 : MonoBehaviour 
{
	public float speed = 10;
	public int damage;
	public GameObject target;
	public Vector3 startPosition;
	public Vector3 targetPosition;
	private Vector3 normalizeDirection;		// Standardize the vectors
	private GameManagerBehaviour gameManager;	// Increases players gold when enemy destroyed
	// Use this for initialization
	void Start () 
	{
		normalizeDirection = (targetPosition - startPosition).normalized;
		GameObject gm = GameObject.Find("GameManager");
		gameManager = gm.GetComponent<GameManagerBehaviour>();
			}
	
	// Update is called once per frame
	void Update () 
	{
		transform.position += normalizeDirection * speed * Time.deltaTime; 	
	}

	void OnTriggerEnter2D(Collider2D other)	// Checks for enemy bullet collision
	{
		target = other.gameObject;
		if(target.tag.Equals("Enemy"))
		{
			Transform healthBarTransform = target.transform.Find("HealthBar");	// Find healthbar of enemy and reduce according to damage
			HealthBar healthBar = healthBarTransform.gameObject.GetComponent<HealthBar>();
			healthBar.currentHealth -= damage;

			if (healthBar.currentHealth <= 0)	//Destroy target if enemy has no health after bullet collision
			{
				Destroy(target);
				AudioSource audioSource = target.GetComponent<AudioSource>();
				AudioSource.PlayClipAtPoint(audioSource.clip, transform.position);	// Play audio
				gameManager.Gold += 50;		// Add gold to player
			}  
			Destroy(gameObject);	// Destroy bullet
		}
	}
}
