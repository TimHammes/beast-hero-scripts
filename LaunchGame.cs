using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
public class LaunchGame : MonoBehaviour
{
  public GameObject hardButton;
  public GameObject easyButton;
  public AudioSource soundSource;
  public AudioClip soundClip;
  public GameManager gameManager;

  void Start()
  {
    soundSource = GameObject.Find("MenuMusic").GetComponent<AudioSource>();
    gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    
  }

  

  public void OnPressPlayEasy()
  {
    gameManager.difficulty = 0;

    StartGame();
  }
  public void OnPressPlayHard()
  {
    gameManager.difficulty = 1;
    StartGame();
  }

  private void StartGame()
  {
    
    soundSource.clip = soundClip;
    soundSource.volume = .1f;
    soundSource.Play();
    soundSource.loop = false;
    SceneManager.LoadScene(1);
  }



}
