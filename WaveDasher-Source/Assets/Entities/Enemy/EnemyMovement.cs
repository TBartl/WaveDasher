using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EnemyMovement : Movement {

	public float acceleration = 3f;
	Transform player;

	protected override void Awake() {
		base.Awake();
		GameObject playerGO = GameObject.FindGameObjectWithTag("Player");
		if (playerGO)
			player = playerGO.transform;
	}

	protected override void Update() {
		base.Update();
	}

	protected override void UpdateNoState() {
		Vector3 direction = GetTargetDirection();

		velocity += direction * acceleration * Time.deltaTime;
		velocity = Mathf.Min(velocity.magnitude, maxSpeed) * velocity.normalized;
		if (velocity.magnitude > .05f) {
			FaceDirection(velocity.normalized);
		}
	}
	public override Vector3 GetTargetDirection() {
		if (!player)
			return Vector3.zero;
		return (player.position - this.transform.position).normalized;
	}

	public float GetDistanceToTarget() {
		if (!player)
			return 1000;
		return Vector3.Distance(player.position, this.transform.position);
	}
}
