using UnityEngine;
using System.Collections;

public class gunControls : MonoBehaviour {

	public int weaponEquipped;
	public bool haveGun = false;
	public bool haveScanner = false;
	public bool haveHookshot = false;
	public Transform gunArm, cursorTransform, gunBarrel, bulletPrefab, hookshotPrefab;
	SpriteRenderer cursorSprite;
	public Vector2 rightPos, leftPos;
	public float bulletSpeed, hookshotSpeed;
	SpriteRenderer armSprite;
	bool aiming, aimingScanner;
	playerControls controlScript;
	//PlayerControlsNoRigidbody controlScript;
	public float cursorDefault;

	// Use this for initialization
	void Start () {
		cursorSprite = cursorTransform.GetComponent<SpriteRenderer>();
		controlScript = GetComponent<playerControls>();
		//controlScript = GetComponent<PlayerControlsNoRigidbody>();
		armSprite = gunArm.GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		GetInput();
		MaintainArmPosition();
		GetRotation();
	}
	
	void GetInput(){
		if (weaponEquipped == 0) { //scanner
			if (Input.GetAxis ("Scanner") > 0 && haveScanner) {
				//ResetCursor();
				armSprite.enabled = true;
				cursorSprite.enabled = true;
				aimingScanner = true;
				aiming = true;
			} else if (!Input.GetButton ("Aim")) {
				ResetCursor ();
				armSprite.enabled = false;
				cursorSprite.enabled = false;
				aimingScanner = false;
				aiming = false;
			}
			if (aimingScanner && Input.GetButtonDown ("Fire")) {
				Scan ();
			}
		} else if (weaponEquipped == 1) {//pistol
			if (Input.GetButton ("Aim") && haveGun) {
				armSprite.enabled = true;
				cursorSprite.enabled = true;
				aiming = true;
			} else {
				armSprite.enabled = false;
				cursorSprite.enabled = false;
				aiming = false;
			}
			if (aiming && Input.GetButtonDown ("Fire")) {
				Fire ();
			}
		} else if (weaponEquipped == 2) {//hookshot
			if (Input.GetButton ("Aim") && haveHookshot) {
				armSprite.enabled = true;
				cursorSprite.enabled = true;
				aiming = true;
			} else {
				armSprite.enabled = false;
				cursorSprite.enabled = false;
				aiming = false;
			}
			if (aiming && Input.GetButtonDown ("Fire")) {
				FireHookshot ();
			}
		}
	}
	
	void Scan(){
		print ("scan");
	}
	
	void ResetCursor(){
		if (controlScript.facingRight){
			cursorTransform.position = (Vector2)transform.position + (Vector2.right * cursorDefault);//new Vector2 (cursorDefault, 0);
		}else{
			cursorTransform.position = (Vector2)transform.position + (Vector2.right * -cursorDefault);
		}
		
	}	
	
	void Fire(){
		Transform newBullet = (Transform)Instantiate(bulletPrefab, gunBarrel.position, Quaternion.identity);
		newBullet.GetComponent<Rigidbody2D>().velocity = gunArm.up * bulletSpeed;
	}

	void FireHookshot(){
		Transform hookshotInstance = (Transform)Instantiate (hookshotPrefab, gunBarrel.position, gunBarrel.rotation);
		hookshotInstance.GetComponent<Rigidbody2D> ().velocity = gunBarrel.up * hookshotSpeed;
	}
	
	void GetRotation(){
		/* float x = -Input.GetAxisRaw("HorizontalRS");
		float y = Input.GetAxisRaw("VerticalRS"); 
	
		print ("x is " + x + " and y is " + y); */
	
		/* var angle = Mathf.Atan2(y,x) * Mathf.Rad2Deg;
		gunArm.transform.rotation = Quaternion.Euler(0, 0, angle); */
	
	
		Vector3 mousePos = cursorTransform.position;//Camera.main.ScreenToWorldPoint(Input.mousePosition);
		gunArm.transform.rotation = Quaternion.LookRotation(Vector3.forward, mousePos - gunArm.transform.position);	
		float x = -Input.GetAxis("HorizontalRS");
		float y = Input.GetAxis("VerticalRS");
	}
	
	void MaintainArmPosition(){
		if (controlScript.facingRight){
			gunArm.transform.position = (Vector2)transform.position + rightPos;
			gunArm.localScale = new Vector3(1,1,1);
		}else{
			gunArm.transform.position = (Vector2)transform.position + leftPos;
			gunArm.localScale = new Vector3(-1,1,1);
		}
	}
}
