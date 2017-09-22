using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeAway : MonoBehaviour {
    Image i;

	// Use this for initialization
	void Start () {
        i = this.GetComponent<Image>();
        StartCoroutine(FadeOut());
	}

    IEnumerator FadeOut() {
        Color from = new Color(1, 1, 1, 1);
        Color to = new Color(1, 1, 1, 0);
        float time = 5;

        for (float t = 0; t < time; t += Time.deltaTime) {
            i.color = Color.Lerp(from, to, t / time);
            yield return null;
        }
        Destroy(this.gameObject);
    }
}
