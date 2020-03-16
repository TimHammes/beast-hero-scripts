using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Queen : MonoBehaviour
{
 
  GameObject player;
  GameObject queen;
  Hero heroScript;
  GameObject algae;
  AudioSource soundPlayer;
  GameObject soundObject;
  SoundManager soundManager;

  public Transform target;
  public bool dead;
  public bool isTriggered;
  float triggerTimer;
  float triggerTime = 10;
  public bool isHit;
  public float attackTimer;
  public float attackTime = .1f;
  public Animator spiderAnimate;
  public float distanceToPlayer;
  public float navigationUpdate;

  private float navigationTime = 0;
  private NavMeshAgent agent;
  private int numberCollisions = 0;
  


 
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
    algae = GameObject.FindGameObjectWithTag("goldenAlgae");

  }

  void OnTriggerEnter(Collider collider)
  {
    int deathBlow = 30;

    if (collider.gameObject.tag == "RightSword" || collider.gameObject.tag == "LeftSword")
    {
      if (!isHit && dead != true)
      {
        isHit = true;
        var magnitude = 2500;
        var force = transform.position - collider.transform.position;
        force.Normalize();
        gameObject.GetComponent<Rigidbody>().AddForce(force * magnitude);
        soundPlayer.volume = .7f;
        soundPlayer.PlayOneShot(soundManager.spinnerHurt);
        VibrationManager.singleton.TriggerVibration(20, 2, 155, OVRInput.Controller.RTouch);
        ++numberCollisions;

        if (numberCollisions >= deathBlow)
        {
          dead = true;
          algae.GetComponent<BoxCollider>().enabled = true;
          gameObject.GetComponent<AudioSource>().Stop();
          spiderAnimate.SetTrigger("spiderDie");
          soundPlayer.volume = .05f;
          soundPlayer.PlayOneShot(soundManager.spinnerDeath);

          StartCoroutine("deadSpiderTimer");
          GetComponent<BoxCollider>().enabled = false;
          agent.updatePosition = false;
          gameObject.GetComponent<Rigidbody>().isKinematic = true;
          numberCollisions = 0;
          deathBlow = 60;

        }
      }
    }
  
  }
  void OnCollisionEnter(Collision other)
  {

    if (other.gameObject.tag == "Player")
    {
      var magnitude = 1000;
      var force = transform.position - other.transform.position;
      force.Normalize();

      gameObject.GetComponent<Rigidbody>().AddForce(force * magnitude);

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

      if (heroScript.isDead == true)
      {
        agent.updatePosition = false;
      }

      distanceToPlayer = Vector3.Distance(this.transform.position, target.position);
      if (distanceToPlayer < 40.0f)
      {
        agent.speed = 10;
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
}

