using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedeSpawnTrigger : MonoBehaviour
{
  GameObject newPede;
  Pede pedeScript;
  AudioSource soundPlayer;
  SoundManager soundManager;
  GameObject player;

  public GameObject pede;
  public GameObject spawnLocation;
  public GameObject[] spawnPoints;
  public bool isTriggered;
  float triggerTimer;
  float triggerTime = 10;

  void Start()
    {

    player = GameObject.FindGameObjectWithTag("Player");
    soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
    soundPlayer = player.GetComponent<AudioSource>();
  }

  private void OnTriggerEnter(Collider other)
  {
    if(other.gameObject.tag == "Player")
    {
      switch (gameObject.name)
      {
        case "Spawn Trigger A":
          SpawnPede();
          break;
        case "Spawn Trigger B":
          SpawnPede();
          break;
        case "Spawn Trigger C":
          SpawnPede();
          break;
        case "Spawn Trigger D":
          SpawnPede();
          break;
        case "Spawn Trigger E":
          SpawnPede();
          break;
        case "Spawn Trigger F":
          SpawnPede();
          break;
        case "Spawn Trigger G":
          SpawnPede();
          break;
        case "Spawn Trigger H":
          SpawnPede();
          break;
        case "Spawn Trigger I":
          SpawnPede();
          break;
        case "Spawn Trigger J":
          SpawnPede();
          break;
        case "Spawn Trigger K":
          SpawnPede();
          break;
        case "Spawn Trigger L":
          SpawnPede();
          break;
        case "Spawn Trigger M":
          SpawnPede();
          break;
        case "Spawn Trigger N":
          SpawnPede();
          break;
        case "Spawn Trigger O":
          SpawnPede();
          break;
        case "Spawn Trigger P":
          SpawnPede();
          break;
        case "Spawn Trigger Q":
          SpawnPede();
          break;
        case "Spawn Trigger R":
          SpawnPede();
          break;
        case "Spawn Trigger S":
          SpawnPede();
          break;
        case "Spawn Trigger T":
          SpawnPede();
          break;
        case "Spawn Trigger U":
          SpawnPede();
          break;
        case "Spawn Trigger V":
          SpawnPede();
          break;
        case "Spawn Trigger W":
          SpawnPede();
          break;
        case "Spawn Trigger X":
          SpawnPede();
          break;
        case "Spawn Trigger Y":
          SpawnPede();
          break;
        case "Spawn Trigger Z":
          SpawnPede();
          break;
        case "Spawn Trigger A1":
          SpawnPede();
          break;
        case "Spawn Trigger B1":
          SpawnPede();
          break;
        case "Spawn Trigger C1":
          SpawnPede();
          break;
        case "Spawn Trigger D1":
          SpawnPede();
          break;
        case "Spawn Trigger E1":
          SpawnPede();
          break;
        case "Spawn Trigger F1":
          SpawnPede();
          break;
        case "Spawn Trigger G1":
          SpawnPede();
          break;
        case "Spawn Trigger H1":
          SpawnPede();
          break;
        case "Spawn Trigger I1":
          SpawnPede();
          break;
        case "Spawn Trigger J1":
          SpawnPede();
          break;
        case "Spawn Trigger K1":
          SpawnPede();
          break;
        case "Spawn Trigger L1":
          SpawnPede();
          break;
        case "Spawn Trigger M1":
          SpawnPede();
          break;
        case "Spawn Trigger N1":
          SpawnPede();
          break;
        case "Spawn Trigger O1":
          SpawnPede();
          break;
        case "Spawn Trigger P1":
          SpawnPede();
          break;
        case "Spawn Trigger Q1":
          SpawnPede();
          break;
        case "Spawn Trigger R1":
          SpawnPede();
          break;
        case "Spawn Trigger S1":
          SpawnPede();
          break;
        case "Spawn Trigger T1":
          SpawnPede();
          break;
        default:
          break;

      }
    
   
    }
  }

  private void SpawnPede()
  {
    if (isTriggered == false)
    {
      isTriggered = true;
      if (!newPede)
      {
        spawnLocation = spawnPoints[Random.Range(0, 7)];
        soundPlayer.volume = 1f;
        soundPlayer.PlayOneShot(soundManager.pedeSpawn);

        newPede = Instantiate(pede) as GameObject;
        newPede.transform.position = spawnLocation.transform.position;

        pedeScript = newPede.GetComponent<Pede>();
      }
     

    }
  }

  void Update()
    {


    if(isTriggered == true)
    {
      if(triggerTimer >= triggerTime)
      {
        isTriggered = false;
        triggerTimer = 0;
      }
    }

    triggerTimer += Time.deltaTime;
    }
}
