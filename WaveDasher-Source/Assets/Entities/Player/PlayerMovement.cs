using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Movement {

	public float acceleration = 3f;
	Vector3 lastInputDirection;

	protected override void Update() {
		base.Update();
		lastInputDirection = GetTargetDirection();
	}

	protected override void UpdateNoState() {
		Vector3 inputDirection = GetInputDirection();
		Vector3 currentDirection = velocity.normalized * velocity.magnitude / maxSpeed;
		Vector3 realDirection = inputDirection - currentDirection;

		velocity += realDirection * acceleration * Time.deltaTime;
		velocity = Mathf.Min(velocity.magnitude, maxSpeed) * velocity.normalized;
		if (velocity.magnitude > .05f) {
			FaceDirection(velocity.normalized);
		}
	}

	Vector3 GetInputDirection () {
		Vector3 direction = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);
		direction = direction.normalized * Mathf.Min(direction.magnitude, 1);
		return direction;
	}

	public override Vector3 GetTargetDirection() {
		Vector3 input = GetInputDirection();
		if (input.magnitude < .05f) {
			return lastInputDirection;
		}
		return input;
	}
}
