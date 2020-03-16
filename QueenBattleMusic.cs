using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueenBattleMusic : MonoBehaviour
{
  GameObject queen;
  AudioSource soundPlayer;
  AudioSource soundPlayer2;
  AudioSource soundPlayer3;

  public bool isQueen;
  void Start()
    {
    queen = GameObject.FindGameObjectWithTag("Queen");
    queen.SetActive(false);
    soundPlayer = gameObject.GetComponent<AudioSource>();
    soundPlayer2 = GameObject.FindGameObjectWithTag("Level2Music").GetComponent<AudioSource>();
    soundPlayer3 = GameObject.FindGameObjectWithTag("CavesDanger").GetComponent<AudioSource>();
  }

  private void OnTriggerEnter(Collider other)
  {
   
    if (other.gameObject.tag == "Player")
    {
      isQueen = true;
      queen.SetActive(true);
      soundPlayer.volume = .1f;
      soundPlayer.mute = false;
    }
  }
  
  void Update()
    {
    if (isQueen)
    {
      soundPlayer2.mute = true;
      soundPlayer3.mute = true;
    }

    if(queen == null)
    {
      soundPlayer.Stop();
    }

    }
}
