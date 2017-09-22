using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damagable : MonoBehaviour {
	public int health = 5;
	public Source affiliation;
	Movement movement;

	List<Renderer> renderers;
	public GameObject deathParticles;

    List<Collider2D> recentlyHitBy = new List<Collider2D>();

	void Awake() {
		renderers = new List<Renderer>(this.GetComponentsInChildren<Renderer>());
		movement = this.GetComponent<Movement>();
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Hurtbox") {
			Hurtbox hurtbox = other.GetComponent<Hurtbox>();
			if (!hurtbox)
				return;

			if (affiliation != hurtbox.source && !recentlyHitBy.Contains(other)) {
				health -= hurtbox.damage;
                recentlyHitBy.Add(other);

				StartCoroutine(Knockback(hurtbox.GetKnockback()));
				StartCoroutine(FlashRed());
				TimeManager.S.StunPause();
				CameraShaker.S.Shake();
				if (movement.GetCurrentAction()) {
					movement.GetCurrentAction().Interrupt();
				}
				if (health <= 0) {
					Destroy(this.gameObject);
					if (deathParticles)
						Instantiate(deathParticles, this.transform.position, Quaternion.identity);
                    if (affiliation == Source.player)
                        SoundManager.S.Play(SoundManager.S.playerDeath);
                    else
                        SoundManager.S.Play(SoundManager.S.enemyDeath);

                }
                else {
                    SoundManager.S.Play(SoundManager.S.hit);
                }
			}
		}
	}

	IEnumerator FlashRed() {
		foreach (Renderer r in renderers) {
			r.material.SetColor("_ReplacementColor", Color.red);
		}
		yield return new WaitForSeconds(.15f);
		foreach (Renderer r in renderers) {
			r.material.SetColor("_ReplacementColor", new Color(0,0,0,0));
		}
	}

	IEnumerator Knockback(Vector3 amount) {
		if (movement == null) 
			yield break;
		float duration = .1f;
		amount = amount / duration;
		movement.knockbackVelocities.Add(amount);
		yield return new WaitForSeconds(duration);
		movement.knockbackVelocities.Remove(amount);
	}

    IEnumerator RecentlyHitBy(Collider2D other) {
        recentlyHitBy.Add(other);
        yield return new WaitForSeconds(.3f);
        if (recentlyHitBy.Contains(other))
            recentlyHitBy.Remove(other);
        else if (recentlyHitBy.Contains(null))
            recentlyHitBy.Remove(null);
    }
    
}
