using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class GameManager : MonoBehaviour
{
  //game state
  public int gameLevel = 0;
  private Scene scene;
  public static GameManager Instance = null;
  public int difficulty;

  public GameObject player;
  public Hero playerScript;
  public int playerHealth = 20;
  public Pede[] pedes;
  public Spinner[] spinners;
  public static GameObject Algae;
  public bool isRightHanded = true;

  GameObject queen;
  GameObject algae;
  OVRManager ovrManager;
  AudioSource soundPlayer;
  GameObject soundObject;
  SoundManager soundManager;
  GameObject soundObject2;
  AudioSource soundPlayer2;


  private void Awake()
  {
    DontDestroyOnLoad(gameObject);


    if(soundObject != null)
    {
      soundObject = GameObject.FindGameObjectWithTag("DesertDanger");
      soundPlayer = soundObject.GetComponent<AudioSource>();
    }
   
  

    if(soundObject2 != null)
    {
      soundObject2 = GameObject.FindGameObjectWithTag("CavesDanger");
      soundPlayer2 = soundObject2.GetComponent<AudioSource>();
    }
  
   
    ovrManager = GameObject.Find("OVRCameraRig").GetComponent<OVRManager>();




  }
  void Start()
  {
    if (Instance == null)
    {
      Instance = this;

    }
    else if (Instance != this)
    {
      Destroy(gameObject);

    }


    ovrManager.resetTrackerOnLoad = true;

    ovrManager.headPoseRelativeOffsetRotation = Vector3.zero;


  }



  void Update()
  {
    GetGameLevel();


    if (Input.GetKey(KeyCode.Space)){
      Application.Quit();
    }
    
 
  }

  public int GetGameLevel()
  {
    scene = SceneManager.GetActiveScene();

    switch (scene.name)
    {
      case "StartGame":
        gameLevel = 0;
        return gameLevel;
        
      case "Introduction":
        gameLevel = 1;
        return gameLevel;
      case "DesertOfIllDestiny":
        gameLevel = 2;
        return gameLevel;
      case "CavesOfPeril2":
        gameLevel = 3;
        return gameLevel;
      case "VictoryScene":
        gameLevel = 4;
        return gameLevel;
      default:
        return gameLevel;
    }
  }

}

