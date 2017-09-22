using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
    public static SoundManager S;

    public AudioSource slash;
    public AudioSource slashFinal;

    public AudioSource spit;
    public AudioSource spitCharge;

    public AudioSource dash;
    public AudioSource spin;

    public AudioSource hit;

    public AudioSource playerDeath;
    public AudioSource enemyDeath;

    public AudioSource waveWon;

    public AudioSource menuPositive;
    public AudioSource menuNegative;

    void Awake() {
        S = this;
    }

    public void Play(AudioSource source) {
        AudioSource clone = Instantiate(source.gameObject).GetComponent<AudioSource>();
        clone.Play();
        Destroy(clone.gameObject, 1);
    }
}
