using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityAnimator : MonoBehaviour {
	Animator anim;
	Movement movement;
	public Vector2 speedAmount;

	// Use this for initialization
	void Start () {
		anim = this.GetComponent<Animator>();
		movement = this.GetComponent<Movement>();
	}
	
	// Update is called once per frame
	void Update () {
		float speedPercent = movement.velocity.magnitude / movement.maxSpeed;
		float animSpeed = Mathf.Lerp(speedAmount.x, speedAmount.y, speedPercent);
		anim.SetFloat("speed", animSpeed);

	}
}
