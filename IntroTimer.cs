using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroTimer : MonoBehaviour
{
  public ParticleSystem parts;
  public ParticleSystem.EmissionModule em;
  float levelTimer;
  float levelTime = 69f;
  AudioSource soundPlayer;
  GameObject soundObject;
  SoundManager soundManager;
  GameObject image;
  GameObject desertText;
  GameObject caveText;
  GameObject caves;
  GameObject[] particles;
  GameObject sword;


  void Start()
  {
    soundObject = GameObject.Find("SoundManager");
    soundManager = soundObject.GetComponent<SoundManager>();
    soundPlayer = soundObject.GetComponent<AudioSource>();

    image = GameObject.Find("RawImage");
    image.SetActive(false);

    desertText = GameObject.Find("desert_text");
    desertText.SetActive(false);

    caveText = GameObject.Find("cave_text");
    caveText.SetActive(false);

    caves = GameObject.Find("caves");
    caves.SetActive(false);

    particles = GameObject.FindGameObjectsWithTag("particles");
    sword = GameObject.Find("sword");
    sword.SetActive(false);

    foreach(var part in particles)
    {

      parts = part.GetComponent<ParticleSystem>();
      em = parts.emission;
      em.enabled = false;
      

    
    }
   

  }


  void Update()
  {
    levelTimer += Time.deltaTime;

    if (levelTimer >= levelTime)
    {

      soundPlayer.PlayOneShot(soundManager.transport);
      SceneManager.LoadScene(2);


    }

    if(levelTimer >= 18)
    {
      foreach (var part in particles)
      {

        parts = part.GetComponent<ParticleSystem>();
        em = parts.emission;
        em.enabled = true;
        parts.Play();
        


      }
    }

    if(levelTimer >= 34)
    {
      caves.SetActive(true);
      caveText.SetActive(true);
    }

    if(levelTimer >= 39)
    {
      image.SetActive(true);
    
    }

    if (levelTimer >= 41)
    {
     
      desertText.SetActive(true);
    }


    if(levelTimer >= 48)
    {
      sword.SetActive(true);
    }

  }
}
