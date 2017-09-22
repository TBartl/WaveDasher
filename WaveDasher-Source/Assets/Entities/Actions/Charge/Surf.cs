using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Surf : ActionCooldown {
	public bool guidable;
	public float speed = 6f;

	public float duration = .3f;
	public float recoveryTime = .05f;

	public GameObject surfPrefab;
	GameObject surfInstance;

	public override void Activate() {
		co = StartCoroutine(ExecuteDash());
	}

	public override void Interrupt() {
		base.Interrupt();
		if (co != null) {
			StopCoroutine(co);
		}
		if (surfInstance != null)
			Destroy(surfInstance);
		if (animator)
			animator.SetTrigger("DashEnd");
		SetSolid();
	}

	IEnumerator ExecuteDash() {
		movement.GrabOverride();
		
		// Move in direction
		Vector3 direction = movement.GetTargetDirection();
		movement.velocity = direction * speed;
		movement.FaceDirection(direction);
		// Make Hurtbox
		if (surfPrefab) {
			surfInstance = GameObject.Instantiate(surfPrefab, this.transform.position, this.transform.rotation, this.transform);
			ColorManager.S.Colorize(surfInstance, this.GetComponent<Damagable>().affiliation);
			Hurtbox hurtbox = surfInstance.GetComponent<Hurtbox>();
			hurtbox.Setup(this.GetComponent<Damagable>().affiliation, direction);
		}
		// Set Passthrough
		SetPassThrough();
		//Animate
		if (animator)
			animator.SetTrigger("DashStart");
        //Sound
        if (!surfPrefab)
            SoundManager.S.Play(SoundManager.S.dash);
        else
            SoundManager.S.Play(SoundManager.S.spin);

        //Wait
        if (guidable) {
			for (float t = 0; t < duration; t += Time.deltaTime) {
				Vector3 newDirection = movement.GetTargetDirection();
				movement.velocity = newDirection * speed;
				movement.FaceDirection(newDirection);
				yield return null;
			}
		}
		else
			yield return new WaitForSeconds(duration);
		
		movement.SetCancellable(this);
		movement.velocity = Vector3.zero;
		if (surfInstance != null)
			Destroy(surfInstance);
		SetSolid();
		StartCoroutine(Cooldown());
		yield return new WaitForSeconds(recoveryTime);

		if (animator)
			animator.SetTrigger("DashEnd");
		movement.ReleaseOverride();
	}

	void SetPassThrough() {
		this.gameObject.layer = LayerMask.NameToLayer("IgnoreCollision");
	}

	void SetSolid() {
		this.gameObject.layer = LayerMask.NameToLayer("Default");
	}


}
