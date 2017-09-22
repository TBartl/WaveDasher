using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleAttack : Action {
	public float lungeSpeed = 6f;
	public float lungeTime = 1f;

	public float timeBetweenAttacks = 1f;
	bool canAttack = true;
	public float lagTime;

	int index;
	public List<GameObject> attackPrefabs;
	GameObject instancedAttack;

	Coroutine reset;

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
	}

	IEnumerator ExecuteSlash() {
		movement.GrabOverride();
		if (index == attackPrefabs.Count - 1)
			StartCoroutine(CooldownAttack(3));
		else
			StartCoroutine(CooldownAttack(1));

		Vector3 direction = movement.GetTargetDirection();
		movement.velocity = direction * lungeSpeed;
		movement.FaceDirection(direction);

		instancedAttack = GameObject.Instantiate(attackPrefabs[index], this.transform.position, this.transform.rotation, this.transform);
		ColorManager.S.Colorize(instancedAttack, this.GetComponent<Damagable>().affiliation);
		index = (index + 1) % attackPrefabs.Count;
		Hurtbox hurtbox = instancedAttack.GetComponent<Hurtbox>();
		hurtbox.Setup(this.GetComponent<Damagable>().affiliation, direction);

		// Animate
		if (animator)
			animator.SetTrigger("Attack");
        //Sound
        if (index != 0)
            SoundManager.S.Play(SoundManager.S.slash);
        else
            SoundManager.S.Play(SoundManager.S.slashFinal);
        yield return new WaitForSeconds(lungeTime);

		movement.SetCancellable(this);
		movement.velocity = Vector3.zero;
		if (reset != null)
			StopCoroutine(reset);
		reset = StartCoroutine(ResetTripleAttack());
		yield return new WaitForSeconds(lagTime);

		movement.ReleaseOverride();
	}

	IEnumerator CooldownAttack(float mod) {
		canAttack = false;
		yield return new WaitForSeconds(timeBetweenAttacks * mod);
		canAttack = true;
	}

	IEnumerator ResetTripleAttack() {
		yield return new WaitForSeconds(1f);
		index = 0;
	}


}
