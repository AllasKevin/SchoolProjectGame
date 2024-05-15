using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
	//Start() Variables
	private Rigidbody2D player;
	private Animator anim;
	private Collider2D coll;
	public bool turnedRight;
	public string test;
	private bool touchingSide;
	private bool stoppedTouching;
	private int highscore;

	//Finite State Machine
	private enum State {idle, running, jumping, falling, hurt};
	private State state = State.idle;
	
	
	//"Changeable" Variables
	[SerializeField] private LayerMask ground; 
	[SerializeField] private float speed; 
	[SerializeField] private float jumpForce;
	[SerializeField] private Text currentHighscore;
	[SerializeField] private float hurtForce = 10f;
	[SerializeField] private GameObject buildBolt;
	[SerializeField] private GameObject portalBolt;

	private void Start(){
		player = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
		coll = GetComponent<Collider2D>();
		turnedRight = true;
		touchingSide = false;
		stoppedTouching = true;
		highscore = PlayerPrefs.GetInt("currentScore");
		currentHighscore.text = highscore.ToString();
	}
	
	
	void Update(){

		
		if (state != State.hurt && !touchingSide)
		{
			Movement();
			Shoot();
		}

		checkIfDead();
		checkIfClear();
		AnimationState();
		anim.SetInteger("state", (int)state);
		
	}
	public bool GetDirection()
    {
		return turnedRight;
    }

	private void Shoot()
    {
		if (Input.GetKeyDown("x") && turnedRight)
        {
			Vector3 shotPos = new Vector3(this.gameObject.transform.localPosition.x + 1f, this.gameObject.transform.localPosition.y, this.gameObject.transform.localPosition.z);
			Instantiate(buildBolt, shotPos, buildBolt.transform.localRotation);
        } else if (Input.GetKeyDown("x") && !turnedRight)
        {
			Vector3 shotPos = new Vector3(this.gameObject.transform.localPosition.x - 1f, this.gameObject.transform.localPosition.y, this.gameObject.transform.localPosition.z);
			Instantiate(buildBolt, shotPos, buildBolt.transform.localRotation);
		}

	}
	
	private void OnTriggerEnter2D(Collider2D collision){
		if(collision.tag == "Collectable"){
			Destroy(collision.gameObject);
			highscore += 5;
			currentHighscore.text = highscore.ToString();
		}


	}
	private void OnCollisionEnter2D(Collision2D other){
		if(other.gameObject.tag == "Enemy"){
			Enemy enemy = other.gameObject.GetComponent<Enemy>();
			if(state == State.falling){
				enemy.JumpedOn();
				Jump();
				highscore += 10;
				currentHighscore.text = highscore.ToString();
			}
			else{
				state = State.hurt;
				if(other.gameObject.transform.position.x > transform.position.x){
					//Enemy is to my right and I should be damaged and moved left
					player.velocity = new Vector2(-hurtForce, player.velocity.y);
				}else{
					player.velocity = new Vector2(hurtForce, player.velocity.y);
				}
			}
		}

		if (other.gameObject.tag == "Side" && stoppedTouching)
		{
			touchingSide = true;
			UnityEngine.Debug.Log("siiiiide enter");
		}

		if (other.gameObject.tag == "Square")
		{

			UnityEngine.Debug.Log("squaaare enter");
			stoppedTouching = false;
			touchingSide = false;
		}
	}

	private void OnCollisionExit2D(Collision2D other)
    {
		if (other.gameObject.tag == "Side" || other.gameObject.tag == "Fore")
		{

			UnityEngine.Debug.Log("side || fore eeeexit");
			
			touchingSide = false;
		}
		if (other.gameObject.tag == "Square")
		{
			stoppedTouching = true;
		}
	}

	void FixedUpdate(){
		//player.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, player.velocity.y);
		//Enligt en snubbe på ett forum var ty detta bättre för att inte behöva använda Time.deltaTime

    }
	
	private void Movement(){
		float hDirection = Input.GetAxis("Horizontal");
		
		if(hDirection < 0){
			player.velocity = new Vector2(-speed, player.velocity.y);
			transform.localScale = new Vector2(-1, 1);
			turnedRight = false;
		}else if(hDirection > 0){
			player.velocity = new Vector2(speed, player.velocity.y);
			transform.localScale = new Vector2(1, 1);
			turnedRight = true;
		}
		else
		{
			
		}
		
		if(Input.GetButtonDown("Jump") && coll.IsTouchingLayers(ground)){ 		//&& Mathf.Abs(player.velocity.y) < 1
			Jump();
		}
	}
	
	private void Jump(){
			player.velocity = new Vector2(player.velocity.x, jumpForce);
			state = State.jumping;
	}
	
	private void AnimationState(){
		if(state == State.jumping){
			//player is jumping
			if(player.velocity.y < 0.1f){
				state = State.falling;
			}
		}else if(state == State.falling){
			if(coll.IsTouchingLayers(ground)){
				state = State.idle;
			}
		}else if(state == State.hurt){
			if(Mathf.Abs(player.velocity.x) < 0.1f){
				state = State.idle;
			}
		}else if(Mathf.Abs(player.velocity.x) > 2f){
			//player is moving
			
			if(player.velocity.y < -0.1f){
				state = State.falling;
			}else{
				state = State.running;
			}
		}else if( state == State.idle){
			if(player.velocity.y < 0f ){
				state = State.falling;
			}
		}else{
			state = State.idle;
		}
	}

	void checkIfDead()
    {
		if(this.gameObject.transform.localPosition.y < -10)
        {
			SceneManager.LoadScene(0);
		}
    }

	void checkIfClear()
    {
		if (this.gameObject.transform.localPosition.x > 194)
		{
			SceneManager.LoadScene(2);
		}

	}

	void OnDisable()
	{
		PlayerPrefs.SetInt("currentScore", highscore);
	}

	void OnEnable()
	{
		highscore = PlayerPrefs.GetInt("currentScore");
	}
}
