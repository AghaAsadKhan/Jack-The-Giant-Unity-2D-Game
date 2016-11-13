using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public float speed = 8f;
	public float maxVelocity = 4f;
	[SerializeField]
	private Rigidbody2D myBody;
	private Animator anim;

	void Awake(){

		myBody = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		PlayerMoveKeyBoard ();
	
	}

	void PlayerMoveKeyBoard(){
	
		float forceX = 0f;
		float vel = Mathf.Abs (myBody.velocity.x);// mathf for abslout vaue which is alwayse positive 

		float horizontalAccess =Input.GetAxisRaw("Horizontal");

		if (horizontalAccess > 0) {
			if (vel < maxVelocity) {
				forceX = speed;

				Vector3 temp = transform.localScale;
				temp.x = 1.3f;
				transform.localScale = temp;

				anim.SetBool ("Walk", true);// animated waliking
			}else {anim.SetBool("Walk", false);}// stop animated walik to idle
		}
			else if (horizontalAccess < 0)
			{
				if(vel < maxVelocity)
				{
					forceX = -speed;
				Vector3 temp = transform.localScale;
				temp.x = -1.3f;
				transform.localScale = temp;
				anim.SetBool ("Walk", true);// animated walking
			} else {anim.SetBool("Walk", false);}// stop animated walik to idle

			}
			myBody.AddForce (new Vector2(forceX, 0));
		
	
	}
}
