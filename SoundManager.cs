using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

	public static SoundManager Instance = null;
	private AudioSource soundEffectAudio;


	public AudioClip footstepsWalk;
	public AudioClip footstepsRun;
	public AudioClip spinnerDeath;
	public AudioClip spinnerHurt;
	public AudioClip pedeHurt;
	public AudioClip pedeDeath;
	public AudioClip deathGameSound;
	public AudioClip queenBattle;
	public AudioClip queenEncounter;
	public AudioClip rockHit;
	public AudioClip playerDeath;
	public AudioClip gameOver;
	public AudioClip playerHurt;
	public AudioClip transport;
	public AudioClip desertDanger;
	public AudioClip cavesDanger;
	public AudioClip pedeSpawn;



	private void Awake()
	{
		DontDestroyOnLoad(gameObject);
	}


	void Start () {
													
		if (Instance == null) {
			Instance = this;

		} else if (Instance != this) {
			Destroy (gameObject);

		}
		AudioSource[] sources = GetComponents<AudioSource> ();
		foreach (AudioSource source in sources) {
			if (source.clip == null) {
				soundEffectAudio = source;

			}
		}

	}

	public void PlayOneShot(AudioClip clip){
		soundEffectAudio.PlayOneShot (clip);

	}
	

	void Update () {
		
	}
}
