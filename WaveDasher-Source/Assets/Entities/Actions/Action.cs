using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action : MonoBehaviour {
	protected Movement movement;
	protected Coroutine co;
	protected Animator animator;

	protected virtual void Awake() {
		movement = this.GetComponent<Movement>();
		animator = this.GetComponent<Animator>();
	}

	public virtual bool IsReady() {
		return movement.CanOverride();
	}

	public virtual void Activate() {

	}

	public virtual void Interrupt() {
		movement.ReleaseOverride();
		if (co != null)
			StopCoroutine(co);
	}

}
