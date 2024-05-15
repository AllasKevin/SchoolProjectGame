using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : Enemy
{
	[SerializeField] private float leftCap;
	[SerializeField] private float rightCap;
	
	[SerializeField] private float jumpLength;
	[SerializeField] private float jumpHeight;
	
	private bool facingLeft = true;
	
	private Collider2D coll;
	[SerializeField] private LayerMask ground;
	
	protected override void Start(){
		base.Start();
		rb = GetComponent<Rigidbody2D>();
		coll = GetComponent<Collider2D>();
	}
	
	private void Update(){
		if(anim.GetBool("jumping")){
			if(rb.velocity.y < 0){
				anim.SetBool("jumping", false);
				anim.SetBool("falling", true);
			}
		}
		
		if(anim.GetBool("falling")){
			if(coll.IsTouchingLayers(ground)){
				anim.SetBool("falling", false);
			}
		}
		
	}
	
	private void Move(){
		
		if(facingLeft){
			//test to see if we have surpassed the leftCap
			if(transform.position.x > leftCap){
				//makes sure the frog is facing the correct way
				if(transform.localScale.x != 1){
					transform.localScale = new Vector3(1, 1, 1);
				}
				
				if(coll.IsTouchingLayers(ground)){
					rb.velocity = new Vector2(-jumpLength, jumpHeight);
					anim.SetBool("jumping", true);
				}
			}else{
				facingLeft = false;
			}
		}else{
			//test to see if we have surpassed the rightCap
			if(transform.position.x < rightCap){
				//makes sure the frog is facing the correct way
				if(transform.localScale.x != -1){
					transform.localScale = new Vector3(-1, 1, 1);
				}
				
				if(coll.IsTouchingLayers(ground)){
					rb.velocity = new Vector2(jumpLength, jumpHeight);
					anim.SetBool("jumping", true);
				}
			}else{
				facingLeft = true;
			}
		}
	}


}
