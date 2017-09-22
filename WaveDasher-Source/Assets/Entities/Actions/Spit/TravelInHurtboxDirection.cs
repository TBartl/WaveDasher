using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TravelInHurtboxDirection : MonoBehaviour {
	public float speed;
	Hurtbox h;
	Rigidbody2D rigid;

	// Use this for initialization
	void Start () {
		h = this.GetComponent<Hurtbox>();
		rigid = this.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		rigid.velocity = h.GetDirection() * speed;
	}
}
