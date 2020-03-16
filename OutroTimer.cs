using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OutroTimer : MonoBehaviour
{
  float levelTimer;
  float levelTime = 34f;
  AudioSource soundPlayer;
  GameObject soundObject;
  SoundManager soundManager;

 

 

  void Start()
  {
    soundObject = GameObject.Find("SoundManager");
    soundManager = soundObject.GetComponent<SoundManager>();
    soundPlayer = soundObject.GetComponent<AudioSource>();

  
   

  }

  // Update is called once per frame
  void Update()
  {
    levelTimer += Time.deltaTime;

    if (levelTimer >= levelTime)
    {

      soundPlayer.PlayOneShot(soundManager.transport);
      SceneManager.LoadScene(0);


    }

 
   

  }
}
