using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ButtonActionPair {
	public string button;
	public Action action;
	public float fallOffTime = .15f;

	int pressCount;
	float timeUntilFalloff;

	public void UpdateInput() {
		if (Input.GetButtonDown(button)) {
			pressCount += 1;
			timeUntilFalloff = fallOffTime;
		}
		if (timeUntilFalloff < 0) {
			pressCount = Mathf.Max(0, pressCount - 1);
			timeUntilFalloff = fallOffTime;
		}
		timeUntilFalloff -= Time.deltaTime;
	}

	public bool IsPressed() {
		if (pressCount > 0) {
			pressCount -= 1;
			timeUntilFalloff = fallOffTime;
			return true;
		}
		return false;
	}
}

public class PlayerActionController : MonoBehaviour {
	public List<ButtonActionPair> buttonActions;

	void Update() {
		foreach (ButtonActionPair ba in buttonActions) {
			ba.UpdateInput();
			if (ba.action.IsReady() && ba.IsPressed()) {
				ba.action.Activate();
			}
		}
	}

}
