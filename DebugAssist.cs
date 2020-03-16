using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugAssist : MonoBehaviour
{
  Hero hero;
  AudioSource soundPlayer;
  GameObject soundObject;
  SoundManager soundManager;
  void Start()
  {
    soundObject = GameObject.Find("SoundManager");
    soundManager = soundObject.GetComponent<SoundManager>();
    soundPlayer = soundObject.GetComponent<AudioSource>();
  }

  private void EnableDebugKeys()
  {
    if (Input.GetKeyDown(KeyCode.L))
    {
      LoadNewScene();
    }

    if (OVRInput.Get(OVRInput.RawButton.Y))
    {
      
      LoadNewScene();

    }
  }

  private void LoadNewScene()
  {
    int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    int nextSceneIndex = currentSceneIndex + 1;

    if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
    {
      nextSceneIndex = 0;
    }
    soundPlayer.volume = 1;
    soundPlayer.PlayOneShot(soundManager.transport);
    SceneManager.LoadScene(nextSceneIndex);
  }

  void Update()
  {
    hero = FindObjectOfType<Hero>();
    if (Debug.isDebugBuild)
    {
      EnableDebugKeys();
    }

    if(OVRInput.GetDown(OVRInput.RawButton.X))
    {
      hero.isRightHanded = false;
      
    }

    if (OVRInput.GetDown(OVRInput.RawButton.A))
    {
      hero.isRightHanded = true;
    }

  }
}
