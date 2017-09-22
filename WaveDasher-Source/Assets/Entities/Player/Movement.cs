using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum ActionState {
	active,
	canInterrupt,
	none
}

public class Movement : MonoBehaviour {
	public float maxSpeed = 3;

	ActionState state = ActionState.none;
	Action currentAction = null;
	
	[HideInInspector] public Vector3 velocity;
	Rigidbody2D rb;
	public List<Vector2> knockbackVelocities;

	protected virtual void Awake() {
		rb = this.GetComponent<Rigidbody2D>();
	}

	protected virtual void Update() {
		if (state == ActionState.none) {
			UpdateNoState();
		}
		rb.velocity = velocity;
		foreach(Vector2 v in knockbackVelocities) {
			rb.velocity = rb.velocity + v;
		}
	}

	protected virtual void UpdateNoState() {
		velocity = Vector3.zero;
		FaceDirection(velocity.normalized);
	}

	public void FaceDirection(Vector3 dir) {
		this.transform.rotation = Quaternion.Euler(0, 0, 270 + Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg);
	}

	public bool CanOverride() {
		return (state != ActionState.active);
	}

	public virtual void GrabOverride() {
		if (state == ActionState.active)
			Debug.LogError("Trying to grab override when something already has it!");

		if (state == ActionState.canInterrupt)
			currentAction.Interrupt();
		state = ActionState.active;
	}

	public virtual void SetCancellable(Action a) {
		currentAction = a;
		state = ActionState.canInterrupt;
	}

	public virtual void ReleaseOverride() {
		state = ActionState.none;
	}

	public virtual Vector3 GetTargetDirection() {
		return velocity.normalized;
	}

	public ActionState GetState() {
		return state;
	}
	public Action GetCurrentAction () {
		if (state == ActionState.none)
			return null;
		return currentAction;
	}
}
