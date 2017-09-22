using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spit : Action {
	public float chargeTime = 1f;

	public float recoilSpeed = 6f;
	public float recoilTime = 1f;

	public float timeBetweenAttacks = 1f;
	bool canAttack = true;
	public float lagTime;

	public bool canAim = true;

	public GameObject bulletPrefab;
	GameObject instancedAttack;

	public override bool IsReady() {
		return canAttack && base.IsReady();
	}

	public override void Activate() {
		co = StartCoroutine(ExecuteSpit());
	}

	public override void Interrupt() {
		base.Interrupt();
		if (co != null) {
			StopCoroutine(co);
		}
		if (animator && animator.GetCurrentAnimatorStateInfo(0).IsName("Charge"))
			animator.SetTrigger("CancelCharge");
	}

	IEnumerator ExecuteSpit() {
		movement.GrabOverride();

		movement.velocity = Vector3.zero;
		Vector3 targetDirection = movement.GetTargetDirection();
		movement.FaceDirection(targetDirection);

		if (animator)
			animator.SetTrigger("StartCharge");
        // Sound
        SoundManager.S.Play(SoundManager.S.spitCharge);
        for (float t = 0; t < chargeTime; t+= Time.deltaTime) {
            movement.velocity = Vector3.zero;
            if (canAim)
                targetDirection = movement.GetTargetDirection();
            movement.FaceDirection(targetDirection);
			yield return null;
		}

        if (canAim)
		    targetDirection = movement.GetTargetDirection();
		instancedAttack = GameObject.Instantiate(bulletPrefab, this.transform.position, this.transform.rotation);
		ColorManager.S.Colorize(instancedAttack, this.GetComponent<Damagable>().affiliation);
		Hurtbox hurtbox = instancedAttack.GetComponent<Hurtbox>();
		hurtbox.Setup(this.GetComponent<Damagable>().affiliation, targetDirection);
		StartCoroutine(CooldownAttack());

		movement.velocity = -targetDirection * recoilSpeed;
		movement.FaceDirection(targetDirection);
		if (animator)
			animator.SetTrigger("Attack");
        // Sound
        SoundManager.S.Play(SoundManager.S.spit);
        yield return new WaitForSeconds(recoilTime);

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
