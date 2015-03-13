using UnityEngine;
using System.Collections;

public class exhaustScript : MonoBehaviour {

	public float destroyTime;
	bool hitOnce;

	// Use this for initialization
	void Start () {
		Invoke ("DestroySelf", destroyTime);
	}
	
	// Update is called once per frame
	void OnCollisionEnter2D (Collision2D col) {
		if (!hitOnce)
			hitOnce = true;
		else
			DestroySelf ();
	}

	void DestroySelf(){
		Destroy (gameObject);
	}
}
