using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Spinner : MonoBehaviour
{

  GameObject player;
  GameObject queen;
  Hero heroScript;

  AudioSource soundPlayer;
  GameObject soundObject;
  SoundManager soundManager;

  public bool isTriggered;
  float triggerTimer;
  float triggerTime = 10;
  public Transform target;
  public bool dead;
  public float attackTimer;
  public float attackTime = .2f;
  public bool isHit;
  public float navigationUpdate;
  public float distanceToPlayer;
  public Animator spiderAnimate;

  private NavMeshAgent agent;
  private int numberCollisions = 0;
  private float navigationTime = 0;


  

  void Start()
  {

    agent = GetComponent<NavMeshAgent>();
    agent.updateRotation = false;
    spiderAnimate = agent.GetComponentInChildren<Animator>();

    player = GameObject.Find("OVRPlayerController");
    heroScript = player.GetComponentInChildren<Hero>();

    soundObject = GameObject.Find("SoundManager");
    soundManager = soundObject.GetComponent<SoundManager>();
    soundPlayer = gameObject.GetComponentInChildren<AudioSource>();

 
  }

  private void OnCollisionEnter(Collision collision)
  {
    if (collision.gameObject.tag == "Player")
    {
      var magnitude = 1500;
      var force = transform.position - collision.transform.position;
      force.Normalize();

      gameObject.GetComponent<Rigidbody>().AddForce(force * magnitude);


    }
  }
  void OnTriggerEnter(Collider collider)
  {

    int deathBlow = 8;

    if (collider.gameObject.tag == "RightSword" || collider.gameObject.tag == "LeftSword")
    {
      if (!isHit && dead != true)
      {
        isHit = true;
        var magnitude = 2000;
        var force = transform.position - collider.transform.position;
        force.Normalize();
        gameObject.GetComponent<Rigidbody>().AddForce(force * magnitude);
        soundPlayer.volume = .5f;
        soundPlayer.PlayOneShot(soundManager.spinnerHurt);
        VibrationManager.singleton.TriggerVibration(20, 2, 155, OVRInput.Controller.RTouch);
        ++numberCollisions;

        if (numberCollisions >= deathBlow)
        {
          dead = true;

          gameObject.GetComponent<AudioSource>().Stop();
          spiderAnimate.SetTrigger("spiderDie");
          soundPlayer.volume = .05f;
          soundPlayer.PlayOneShot(soundManager.spinnerHurt);
          soundManager.PlayOneShot(soundManager.spinnerDeath);

          StartCoroutine("deadSpiderTimer");
          GetComponent<BoxCollider>().enabled = false;
          agent.updatePosition = false;
          gameObject.GetComponent<Rigidbody>().isKinematic = true;
          numberCollisions = 0;
          deathBlow = 30;

        }
      }
    }

    
   
  }

  IEnumerator deadSpiderTimer()
  {
    yield return new WaitForSeconds(3);
    Destroy(gameObject);
  }

  void Update()
  {
  

    target = player.transform;
    Vector3 targetRotation = new Vector3(target.position.x, agent.transform.position.y, target.position.z);
    if (target != null)
    {

      navigationTime += Time.deltaTime;
      if (navigationTime > navigationUpdate)
      {
        
          targetRotation = new Vector3(target.transform.position.x, agent.transform.position.y, target.transform.position.z);
          transform.LookAt(targetRotation);
          agent.destination = target.position;
          navigationTime = 0;
       
     
      }

    }

    if(heroScript.isDead == true)
    {
      agent.updatePosition = false;
    }

    distanceToPlayer = Vector3.Distance(this.transform.position, target.position);
    if (distanceToPlayer < 8.0f)
    {
      agent.speed = 6;
    }

    if (isHit)
    {
      attackTimer += Time.deltaTime;
      if (attackTimer > attackTime)
      {

        isHit = false;
        attackTimer = 0;
      }

    }
  }
}
