using UnityEngine;
using System.Collections;

public class customParticles : MonoBehaviour {

	public Transform particleToEmit;
	public float emmisionRate, expelSpeed;
	public float xMin, xMax;
	private float margin = 0.1f;

	// Use this for initialization
	void Start () {
		InvokeRepeating ("Emit", 0, 1f / emmisionRate);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void Emit(){
		Transform newParticle = Instantiate(particleToEmit, transform.position, Quaternion.identity) as Transform;
		float xPos = Random.Range (xMin, xMax);
		xPos *= margin;
		newParticle.position = new Vector2 (transform.position.x + xPos, newParticle.position.y);
		newParticle.GetComponent<Rigidbody2D> ().AddForce (-Vector2.up * expelSpeed);
	}
}
