using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class Wave : ScriptableObject {
	public List<GameObject> enemies;
    public bool rewardsHealth = true;

}
