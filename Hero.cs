using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;
using UnityEngine.Playables;
using TMPro;



//THIS IS THE WARRIOR PLAYER SCRIPT
public class Hero : MonoBehaviour
{


  public AudioClip playerDeath;
  public AudioClip playerHurt;

  public bool isRightHanded = true;
  public int health;
  public float timeBetweenHits = .001f;
  public float distanceToPlayer;
  public bool isMoving;
  public bool isDead;

  CharacterController characterController;
  OVRManager ovrManager;
  GameObject player;
  GameObject playerCube;
  GameObject sword;
  GameObject[] swordEye;
  GameObject grip;
  GameObject swordLeft;
  GameObject swordRight;
  GameObject gManager;
  GameManager gScript;
  OvrAvatar avatar;
  GameObject gameOverCanvas;
  GameObject trackingAnchor;
  AudioSource soundPlayer;
  AudioSource soundPlayer2;
  GameObject soundObject;
  SoundManager soundManager;

 
  private bool isHit = false;
  private float timeSinceHit = 0;
  private int healthDamage = 1;
 
  float footstepTimer;

  private void Awake()
  {
    swordLeft = GameObject.FindGameObjectWithTag("LeftSword");
    swordRight = GameObject.FindGameObjectWithTag("RightSword");
  }
  void Start()
  {
    player = GameObject.Find("OVRPlayerController");
    playerCube = GameObject.Find("Cube");
    characterController = player.GetComponent<CharacterController>();
    ovrManager = GameObject.Find("OVRCameraRig").GetComponent<OVRManager>();
    avatar = GameObject.Find("LocalAvatar").GetComponent<OvrAvatar>();
    sword = GameObject.Find("Weapon");
    grip = GameObject.Find("RightHandAnchor");
    gameOverCanvas = GameObject.FindGameObjectWithTag("GameOverCanvas");
    swordEye = GameObject.FindGameObjectsWithTag("swordEye");
    trackingAnchor = GameObject.Find("TrackerAnchor");
    gManager = GameObject.Find("GameManager");
    gScript = gManager.GetComponent<GameManager>();

    soundObject = GameObject.Find("SoundManager");
    soundManager = soundObject.GetComponent<SoundManager>();
    soundPlayer = gameObject.GetComponent<AudioSource>();
    soundPlayer2 = gameObject.GetComponentInParent<AudioSource>();


    if(gScript.difficulty == 0  && gScript.GetGameLevel() == 3)
    {
      health = 20;
    }
   

    if(gScript.difficulty == 1 && gScript.GetGameLevel() == 3)
    {
      health = gScript.playerHealth;
    }


   

  }
  public void TakeDamage(int healthDamage)
  {
    gScript.playerHealth -= healthDamage;
    health -= healthDamage;

    if (health == 0)
    {
      isDead = true;
      grip.transform.DetachChildren();

      soundPlayer2.PlayOneShot(soundManager.playerDeath);
      soundPlayer.volume = .6f;
      soundPlayer.clip = soundManager.gameOver;
      soundPlayer.Play();

      sword.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
      sword.GetComponent<Rigidbody>().useGravity = true;
      sword.GetComponent<BoxCollider>().isTrigger = true;

      gameObject.GetComponentInParent<OVRPlayerController>().enabled = false;

      avatar.ShowFirstPerson = false;
      //ovrManager.headPoseRelativeOffsetRotation = Vector3.Lerp(ovrManager.headPoseRelativeOffsetRotation, gameObject.transform.position, .2f);    turns player parallel to ground

      gameOverCanvas.GetComponent<Canvas>().enabled = true;
      gameOverCanvas.transform.parent = trackingAnchor.transform;
   
    }
  }
  

  void OnCollisionEnter(Collision other)
  {
    if(gScript.gameLevel == 2)
    {

      CheckPedeCollision(other);
    }
    else if(gScript.gameLevel == 3)
    {
      CheckSpiderCollision(other);

      CheckQueenCollision(other);
    }
  

  }

  private void CheckPedeCollision(Collision other)
  {
    if (other.gameObject.tag == "Pede")
    {
      ProcessEnemyStrike(other);
      Pede pede = other.gameObject.GetComponentInChildren<Pede>();
      if (pede != null && pede.dead != true && playerCube != null)
      {
        playDamageEFX(healthDamage);
      }
    }
  }

  private void playDamageEFX(int healthDamage)
  {
    if (!isHit)
    {

      isHit = true;


      TakeDamage(healthDamage);
      soundPlayer2.volume = 1;
      soundPlayer2.PlayOneShot(soundManager.playerHurt);
      VibrationManager.singleton.TriggerVibration(40, 2, 255, OVRInput.Controller.LTouch);
      VibrationManager.singleton.TriggerVibration(40, 2, 255, OVRInput.Controller.RTouch);


    }
  }

  private void CheckQueenCollision(Collision other)
  {
    if (other.gameObject.tag == "Queen")
    {
      ProcessEnemyStrike(other);
      Queen queen = other.gameObject.GetComponentInChildren<Queen>();
      if (queen != null && queen.dead != true && playerCube != null)
      {
        playDamageEFX(healthDamage * 4);
      }
    }
  }

  private void CheckSpiderCollision(Collision other)
  {
    if (other.gameObject.tag == "Spider")
    {
    

      ProcessEnemyStrike(other);

      Spinner spinner = other.gameObject.GetComponentInChildren<Spinner>();
      if (spinner != null &&  playerCube != null)
      {
        playDamageEFX(healthDamage);
      }
    }
  }

  private void ProcessEnemyStrike(Collision other)
  {
    
    var magnitude = .3f;
  
    var force = transform.position - other.transform.position;
  
    force.Normalize();
  
    gameObject.GetComponentInParent<CharacterController>().Move(force * magnitude);
  }

  private void OnTriggerEnter(Collider other)
  {
    if (other.gameObject.tag == "CaveEntrance")
    {
      
      Debug.Log("Entered Cave");
      soundPlayer.volume = 0.5f;
      soundPlayer.PlayOneShot(soundManager.transport);
      SceneManager.LoadScene(3);
    }

 
  }


  private void FixedUpdate()
  {
    if (characterController.isGrounded == true && characterController.velocity.magnitude > 0.5f)
    {
      isMoving = true;
      if (footstepTimer > .7f)
      {
        soundPlayer.volume = .1f;
        soundPlayer.PlayOneShot(soundManager.footstepsWalk);


        footstepTimer = 0;
      }

    }
    if (isMoving == true && OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger) > 0)
    {
      if (footstepTimer > .4f)
      {
        soundPlayer.volume = .1f;
        soundPlayer.PlayOneShot(soundManager.footstepsWalk);


        footstepTimer = 0;
      }
    }

    

    footstepTimer += Time.deltaTime;
  }


  void Update()
  {
    ChooseHands();
    
   
    if (isHit)
    {
      timeSinceHit += Time.deltaTime;
      if (timeSinceHit > timeBetweenHits)
      {

        isHit = false;
        timeSinceHit = 0;
      }

    }


    //Lets player know his health is low
    if (health <= 5)
    {
      foreach (var eye in swordEye)
      {
        
        eye.GetComponent<PlayableDirector>().enabled = true;
      
       
      }
    }

    if (OVRInput.GetDown(OVRInput.RawButton.A))
    {
      player = GameObject.Find("OVRPlayerController");

      CharacterController character = player.GetComponent<CharacterController>();
      character.center = new Vector3(0, 1, 0);
      Debug.Log(character.name);
    }

    if (isDead == true)
    {
      StartCoroutine("ReturnToMainMenu");
    
    }
  }

  

  IEnumerator ReturnToMainMenu()
  {
    yield return new WaitForSeconds(10);
    soundPlayer.PlayOneShot(soundManager.transport);
    SceneManager.LoadScene(0);
  }

  
  public void ChooseHands()
  {
    if (isRightHanded)
    {
      swordLeft.SetActive(false);
      swordRight.SetActive(true);
    }
    else
    {
      swordRight.SetActive(false);
      swordLeft.SetActive(true);
    }
  }


}
