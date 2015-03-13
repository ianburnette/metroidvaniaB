using UnityEngine;
using System.Collections;

public class roverControls : MonoBehaviour {

	public float hoverDistance, hoverForce, hSpeed, vSpeed;
	public LayerMask groundMask;
	public Animator anim;
	private float  h, v;
	private Rigidbody2D rb;

	// Use this for initialization
	void Start () {
		rb = transform.GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		GetInput ();
		Hover ();
		MoveHorizontal ();
		Animate ();
	}

	void GetInput(){
		h = Input.GetAxisRaw ("Horizontal");
		v = Input.GetAxisRaw ("Vertical");
	}

	void Hover(){
		RaycastHit2D groundHit = Physics2D.Raycast (transform.position, -Vector2.up, hoverDistance, groundMask);
		if (groundHit.transform != null) {
			rb.AddForce(Vector2.up * hoverForce);
		}
	}

	void MoveHorizontal(){
		float myHVel = h * hSpeed;
		float myVvel = v * vSpeed;

		rb.velocity = new Vector2 (myHVel, rb.velocity.y + v);
	}

	void Animate(){
		int normalizedSpeed = Mathf.RoundToInt (h);
		anim.SetInteger ("direction", normalizedSpeed);
//print ("setting to " + h);
	}
}
