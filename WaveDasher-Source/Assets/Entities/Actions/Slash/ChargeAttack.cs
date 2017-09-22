using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeAttack : Action {
	public float chargeTime = .5f;

	public float lungeSpeed = 6f;
	public float lungeTime = 1f;

	public float timeBetweenAttacks = 1f;
	bool canAttack = true;
	public float lagTime;

	public GameObject attackPrefab;
	GameObject instancedAttack;

	public override bool IsReady() {
		return canAttack && base.IsReady();
	}

	public override void Activate() {
		co = StartCoroutine(ExecuteSlash());
	}

	public override void Interrupt() {
		base.Interrupt();
		if (co != null) {
			StopCoroutine(co);
		}
		if (instancedAttack != null) {
			Destroy(instancedAttack);
		}
		if (animator && animator.GetCurrentAnimatorStateInfo(0).IsName("Charge"))
			animator.SetTrigger("CancelCharge");
	}

	IEnumerator ExecuteSlash() {
		movement.GrabOverride();
		StartCoroutine(CooldownAttack());

		Vector3 direction = movement.GetTargetDirection();
		movement.velocity = direction * lungeSpeed;
		movement.FaceDirection(direction);
		movement.velocity = Vector3.zero;
		// Animate
		if (animator)
			animator.SetTrigger("StartCharge");
		yield return new WaitForSeconds(chargeTime);

		instancedAttack = GameObject.Instantiate(attackPrefab, this.transform.position, this.transform.rotation, this.transform);
		ColorManager.S.Colorize(instancedAttack, this.GetComponent<Damagable>().affiliation);
		Hurtbox hurtbox = instancedAttack.GetComponent<Hurtbox>();
		hurtbox.Setup(this.GetComponent<Damagable>().affiliation, direction);
		// Animate
		if (animator)
			animator.SetTrigger("Attack");
        //Sound
        SoundManager.S.Play(SoundManager.S.slash);
		yield return new WaitForSeconds(lungeTime);

		movement.SetCancellable(this);
		movement.velocity = Vector3.zero;
		yield return new WaitForSeconds(lagTime);

		movement.ReleaseOverride();
	}

	IEnumerator CooldownAttack() {
		canAttack = false;
		yield return new WaitForSeconds(timeBetweenAttacks);
		canAttack = true;
	}


}
