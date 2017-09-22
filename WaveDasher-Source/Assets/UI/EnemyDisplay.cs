using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyDisplay : MonoBehaviour {
	Damagable player;
	public GameObject barPrefab;
	public float padding = 10f;
	List<Image> bars = new List<Image>();

	// Update is called once per frame
	void Update() {
		while (bars.Count != WaveManager.S.GetEnemyCount()) {
			if (bars.Count < WaveManager.S.GetEnemyCount()) {
				GameObject go = GameObject.Instantiate(barPrefab, this.transform);
                go.GetComponent<PulseSize>().index = bars.Count;
                Image im = go.GetComponent<Image>();
				im.rectTransform.localPosition = new Vector2((im.rectTransform.sizeDelta.x + padding) * bars.Count, 0);
				bars.Add(im);
			} else {
				Destroy(bars[bars.Count - 1].gameObject);
				bars.RemoveAt(bars.Count - 1);
			}
		}
	}
}
