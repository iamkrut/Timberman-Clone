using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerScript : MonoBehaviour {

	public static AudioManagerScript instance;

	public AudioSource cutAudioSource;
	public AudioSource deathAudioSource;
	public AudioSource themeAudioSource;

	void Awake(){
		instance = this;
	}

	void Start(){
		PlayThemeAudio();
	}

	public void PlayCutAudio(){
		cutAudioSource.Play();
	}

	public void PlayDeathAudio(){
		deathAudioSource.Play();
	}

	public void PlayThemeAudio(){
		themeAudioSource.Play();
	}
}
