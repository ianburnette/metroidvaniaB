using UnityEngine;
using System.Collections;

public class hookshotBehavior : MonoBehaviour {

	public Vector2 origin, latestPosUpdate;
	public Transform ropePiece;
	public float drawDist = .3f;

	//public float gravAmt = 2f;
	//public float timeToGrav = 3;
	public int timeToDie = 5;
	
	// Use this for initialization
	void Start () {
		Invoke("DestroySelf", timeToDie);
		origin = transform.parent.position;
		latestPosUpdate = origin;
	}

	void SetOrigin(Vector2 ori){
		origin = ori;
		latestPosUpdate = origin;
	}

	void Update(){
		origin = transform.parent.position;
		Animate ();
	}

	void Animate(){
		if (Vector2.Distance (latestPosUpdate, (Vector2)transform.position) > drawDist) {
			Transform newRopePiece = (Transform)Instantiate(ropePiece, latestPosUpdate, transform.rotation);
			newRopePiece.parent = transform.parent;
			latestPosUpdate = transform.position;
		}
	}

	// Update is called once per frame
	void OnTriggerEnter2D(Collider2D col) {
		if (col.transform.tag == "destroyable"){
			col.SendMessage("BulletHit");
		}
		DestroySelf();
	}
	
	void DestroySelf(){
		Destroy(transform.parent.gameObject);
	}
}
