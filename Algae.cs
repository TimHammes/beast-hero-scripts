using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Algae : MonoBehaviour {


	GameObject gManager;
	AudioSource soundPlayer;
	SoundManager soundManager;

	private void Start()
	{
		gManager = GameObject.Find("GameManager");
		soundPlayer = GameObject.Find("SoundManager").GetComponent<AudioSource>();
		soundManager = soundPlayer.GetComponent<SoundManager>();
	}
	void OnTriggerEnter(Collider other)
	{

		if (other.gameObject.tag == "Player")
		{

			soundPlayer.PlayOneShot(soundManager.transport);
			SceneManager.LoadScene(4);
			
		}
	}


}
