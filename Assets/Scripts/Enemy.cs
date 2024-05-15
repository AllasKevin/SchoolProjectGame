using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	protected Animator anim;
	protected Rigidbody2D rb;
	
	protected virtual void Start(){
		anim = GetComponent<Animator>();
		rb = GetComponent<Rigidbody2D>();
	}
	
	public void JumpedOn(){
		anim.SetTrigger("death");
		rb.velocity = Vector2.zero;
		rb.bodyType = RigidbodyType2D.Static;
	}
	
	private void Death(){

		UnityEngine.Debug.Log("before");
		Destroy(this.gameObject);
		UnityEngine.Debug.Log("after");
	}
}
