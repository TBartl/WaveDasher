using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour {
	Damagable player;
	public GameObject barPrefab;
	public float padding = 10f;
	List<Image> bars = new List<Image>();

	// Update is called once per frame
	void Update () {
		if (player == null) {
            player = WaveManager.S.GetPlayer();
		}

		while (bars.Count != GetHealth()) {
			if (bars.Count < GetHealth()) {
				GameObject go = GameObject.Instantiate(barPrefab, this.transform);
                go.GetComponent<PulseSize>().index = bars.Count;
				Image im = go.GetComponent<Image>();
				im.rectTransform.localPosition = new Vector2((im.rectTransform.sizeDelta.x + padding) * bars.Count, 0);
				bars.Add(im);
			} 
			else {
				Destroy(bars[bars.Count - 1].gameObject);
				bars.RemoveAt(bars.Count - 1);
			}
		}
	}

	int GetHealth() {
		if (player == null)
			return 0;
		return player.health;
	}
}
