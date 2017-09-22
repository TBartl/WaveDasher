using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionCooldown : Action {
	public float cooldownTime;
	bool ready = true;	

	public override bool IsReady() {
		return ready && base.IsReady();
	}

	public override void Interrupt() {
		base.Interrupt();
		StartCoroutine(Cooldown());
	}

	public IEnumerator Cooldown() {
		ready = false;
		yield return new WaitForSeconds(cooldownTime);
		ready = true;
	}

}
