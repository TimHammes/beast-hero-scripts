using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinnerSpawnTrigger : MonoBehaviour
{
 
  GameObject newQueen;
  GameObject newSpinner;
  GameObject newSpinner2;
  GameObject newSpinner3;
  GameObject newSpinner4;
  Spinner spinnerScript;
  Queen queenScript;

  public GameObject spinner;
  public GameObject queen;
  public GameObject spawnLocation1;
  public GameObject[] spawnPointsA;
  public GameObject queenSpawnPoint;
  public bool isTriggered;
  float triggerTimer;
  float triggerTime = 60;

  void Start()
  {

  }

  private void OnTriggerEnter(Collider other)
  {

    if (other.gameObject.tag == "Player")
    {
      switch (gameObject.name)
      {
        case "Spawn Trigger A":

          if (this.isTriggered == false)
          {
            this.isTriggered = true;
            Invoke("SpawnSpinner", 1.0f);
           

          }
          break;

        case "Spawn Trigger B":
          if (this.isTriggered == false)
          {
            this.isTriggered = true;
            Invoke("SpawnSpinner", 1.0f);

          }
          break;

        case "Spawn Trigger C":
          if (this.isTriggered == false)
          {
            this.isTriggered = true;
            Invoke("SpawnMultipleSpinnerA", 1.0f);
          
          }
          break;

        case "Spawn Trigger D":
          if (this.isTriggered == false)
          {
            this.isTriggered = true;
            Invoke("SpawnSpinner", 1.0f);
          }
          break;

        case "Spawn Trigger E":
          if (this.isTriggered == false)
          {
            this.isTriggered = true;
            Invoke("SpawnMultipleSpinnerA", 1.0f);
          }
          break;

        case "Spawn Trigger F":
          if (this.isTriggered == false)
          {
            this.isTriggered = true;
        
            Invoke("SpawnMultipleSpinnerB", 1.0f);

          }
          break;

        case "Spawn Trigger G":
          if (this.isTriggered == false)
          {
            this.isTriggered = true;
            Invoke("SpawnSpinner", 1.0f);

          }
          break;

        default:
          break;
      }
    }
  }

  private void SpawnSpinner()
  {
    if (!newSpinner)
    {
      spawnLocation1 = spawnPointsA[Random.Range(0, spawnPointsA.Length)];
      newSpinner = Instantiate(spinner, spawnLocation1.transform.position, spawnLocation1.transform.rotation) as GameObject;
      spinnerScript = newSpinner.GetComponent<Spinner>();
    }

  
  
  }
  private void SpawnMultipleSpinnerA()
  {
    if (!newSpinner)
    {
      spawnLocation1 = spawnPointsA[Random.Range(0, spawnPointsA.Length)];
      newSpinner = Instantiate(spinner, spawnLocation1.transform.position, spawnLocation1.transform.rotation) as GameObject;
      spinnerScript = newSpinner.GetComponent<Spinner>();
    }
    StartCoroutine("SpawnAgain");

  }

  IEnumerator SpawnAgain()
  {
    yield return new WaitForSeconds(4);
    if (!newSpinner2)
    {
      spawnLocation1 = spawnPointsA[Random.Range(0, spawnPointsA.Length)];
      newSpinner2 = Instantiate(spinner, spawnLocation1.transform.position, spawnLocation1.transform.rotation) as GameObject;
      spinnerScript = newSpinner2.GetComponent<Spinner>();
    }
  }

  private void SpawnMultipleSpinnerB()
  {
    if (!newSpinner3)
    {
      spawnLocation1 = spawnPointsA[Random.Range(0, spawnPointsA.Length)];
      newSpinner3 = Instantiate(spinner, spawnLocation1.transform.position, spawnLocation1.transform.rotation) as GameObject;
      spinnerScript = newSpinner3.GetComponent<Spinner>();
    }
    StartCoroutine("SpawnAgain");

  }

  IEnumerator SpawnAgainB()
  {
    yield return new WaitForSeconds(4);
    if (!newSpinner4)
    {
      spawnLocation1 = spawnPointsA[Random.Range(0, spawnPointsA.Length)];
      newSpinner4 = Instantiate(spinner, spawnLocation1.transform.position, spawnLocation1.transform.rotation) as GameObject;
      spinnerScript = newSpinner4.GetComponent<Spinner>();
    }
  }


  void Update()
  {

    if (this.isTriggered == true)
    {
      if (triggerTimer >= triggerTime)
      {
        this.isTriggered = false;
        triggerTimer = 0;
      }
    }




    triggerTimer += Time.deltaTime;
  }
}
