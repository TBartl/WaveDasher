using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScreen : MonoBehaviour {
    bool transitioning = false;
    public Image black;

    // Update is called once per frame
    void Update () {
        if (Input.GetButton("Dash") && transitioning == false) {
            SoundManager.S.Play(SoundManager.S.menuPositive);
            StartCoroutine(ChangeToGame());
        }
		
	}

    IEnumerator ChangeToGame() {
        transitioning = true;
        float transitionTime = 1f;
        Color from = new Color(0, 0, 0, 0);
        Color to = new Color(0, 0, 0, 1);

        for (float t = 0; t < transitionTime; t += Time.deltaTime) {
            black.color = Color.Lerp(from,to, t/transitionTime);
            yield return null;
        }

        SceneManager.LoadScene(1);
    }
}
