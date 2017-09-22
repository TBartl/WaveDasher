using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WaveManager : MonoBehaviour {
	public List<Wave> waves;
	int waveIndex = 0;
	List<GameObject> currentEnemies;
	List<Vector3> spawnPositions;
	public static WaveManager S;

    Damagable player;
    public GameObject playerPrefab;

    public Text title;
    public Text subtitle;

    public Image black;

    public int maxHealth = 1;


    void Awake() {
		S = this;
        GameObject playerGO = GameObject.FindGameObjectWithTag("Player");
        if (playerGO) {
            player = playerGO.GetComponent<Damagable>();
            player.health = maxHealth;
        }
	}

	// Use this for initialization
	void Start () {
		spawnPositions = new List<Vector3>();
		foreach(GameObject g in GameObject.FindGameObjectsWithTag("Spawn")) {
			spawnPositions.Add(g.transform.position);
		}

		StartCoroutine(RunGame());
	}

	IEnumerator RunGame() {
		while (waveIndex < waves.Count) {
            title.text = "Wave " + (waveIndex + 1).ToString();
            subtitle.text = GetEnemyInformation();
            StartCoroutine(RemoveTextAfterTime(3f));


			currentEnemies = new List<GameObject>();
			foreach (GameObject g in waves[waveIndex].enemies) {
				currentEnemies.Add(GameObject.Instantiate(g, GetRandomPosition(), Quaternion.identity));
                if (!Input.GetKey(KeyCode.O))
                    yield return new WaitForSeconds(.3f);
			}

			while (true) {
				foreach (GameObject g in currentEnemies) {
					if (g == null) {
						currentEnemies.Remove(null);
						break;
					}
				}
                
                if (player == null) {
                    yield return new WaitForSeconds(2f);
                    title.text = "Wave Failed!";
                    subtitle.text = "Press A to Restart. Press BACK to return to the main menu.";
                    

                    while (true) {
                        if (Input.GetButtonDown("Dash")) {
                            SoundManager.S.Play(SoundManager.S.menuPositive);
                            break;
                        }
                        if (Input.GetButtonDown("Start")) {
                            SoundManager.S.Play(SoundManager.S.menuNegative);
                            StartCoroutine(ChangeToTitle());
                            while (true)
                                yield return null;
                        }

                        yield return null;
                    }

                    foreach (GameObject g in currentEnemies) {
                        Destroy(g);
                    }
                    currentEnemies.Clear();
                    title.text = "";
                    subtitle.text = "";
                    player = GameObject.Instantiate(playerPrefab).GetComponent<Damagable>();
                    player.health = maxHealth;
                    yield return new WaitForSeconds(.5f);
                    break;
                }

                if (currentEnemies.Count == 0) {                    
                    SoundManager.S.Play(SoundManager.S.waveWon);
                    title.text = "Wave Complete!";
                    if (waves[waveIndex].rewardsHealth) {
                        maxHealth += 1;
                        subtitle.text = "+1 Max Health";
                    }
                    waveIndex += 1;
                    for (int i = player.health; i <= maxHealth; i++) {
                        player.health = i;
                        yield return new WaitForSeconds(.2f);
                    }
                    yield return new WaitForSeconds(1.5f);

                    title.text = "";
                    subtitle.text = "";
                    yield return new WaitForSeconds(.5f);
                    break;
                }

                if (Input.GetKeyDown(KeyCode.P)) {
                    foreach (GameObject g in currentEnemies) {
                        Destroy(g);
                    }
                    currentEnemies.Clear();
                }

				yield return null;
			}
		}

        title.text = "All Waves Complete!";
        subtitle.text = "Returning to menu";
        SoundManager.S.Play(SoundManager.S.waveWon);
        yield return new WaitForSeconds(2f);
        StartCoroutine(ChangeToTitle());
        while (true) 
            yield return null;

    }

	Vector3 GetRandomPosition() {
		return spawnPositions[Random.Range(0, spawnPositions.Count)];
	}

	public int GetEnemyCount() {
		if (currentEnemies == null)
			return 0;
		return currentEnemies.Count;
	}

	public Damagable GetPlayer() {
		return player;
	}

    IEnumerator RemoveTextAfterTime(float t) {
        yield return new WaitForSeconds(t);
        title.text = "";
        subtitle.text = "";
    }

    string GetEnemyInformation() {
        string s = "";

        string lastEnemyName = "";
        int enemyCount = 0;
        foreach (GameObject g in waves[waveIndex].enemies) {
            if (g.name != lastEnemyName) {
                if (lastEnemyName != "") {
                    s += " x" + enemyCount.ToString() + "  ";
                }
                s += g.name;
                lastEnemyName = g.name;
                enemyCount = 1;
            }
            else {
                enemyCount += 1;
            }
        }
        if (lastEnemyName != "") {
            s += " x" + enemyCount.ToString();
        }

        return s;
    }

    IEnumerator ChangeToTitle() {
        float transitionTime = 1f;
        Color from = new Color(0, 0, 0, 0);
        Color to = new Color(0, 0, 0, 1);

        for (float t = 0; t < transitionTime; t += Time.deltaTime) {
            black.color = Color.Lerp(from, to, t / transitionTime);
            yield return null;
        }

        SceneManager.LoadScene(0);
    }

}
