using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Source {
	enemy,
	player
}

public class Hurtbox : MonoBehaviour {
	public Source source;
	public int damage = 1;
	Vector2 knockbackDirection;
	public float knockbackAmount;

	public void Setup(Source source, Vector2 knockbackAmount) {
		this.source = source;
		this.knockbackDirection = knockbackAmount;
	}

	public Vector3 GetKnockback() {
		return knockbackDirection * knockbackAmount;
	}

	public Vector3 GetDirection() {
		return knockbackDirection.normalized;
	}
}
