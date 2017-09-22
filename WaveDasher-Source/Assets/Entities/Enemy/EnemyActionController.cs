using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DistanceActionPair {
	public float minDistance;
	public Action action;
}

public class EnemyActionController : MonoBehaviour {
	public List<DistanceActionPair> distanceActions;
	EnemyMovement enemyMovement;

    public float timeUntilActionsStart = .1f;
    public float maxTimeBetweenAttacks = 0f;
    float timeBetweenAttacks = 0;

	void Awake() {
		enemyMovement = this.GetComponent<EnemyMovement>();
	}

	void Update() {

		if (timeUntilActionsStart <= 0 && timeBetweenAttacks <= 0 && distanceActions.Count > 0) {
			int indexToTry = Random.Range(0, distanceActions.Count);
			DistanceActionPair pairToTry = distanceActions[indexToTry];

			if (enemyMovement.GetDistanceToTarget() < pairToTry.minDistance && pairToTry.action.IsReady()) {
				pairToTry.action.Activate();
                timeBetweenAttacks = maxTimeBetweenAttacks;
			}
        }
        timeUntilActionsStart -= Time.deltaTime;
        timeBetweenAttacks -= Time.deltaTime;
    }

}
