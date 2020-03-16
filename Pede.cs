using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Pede : MonoBehaviour
{

  GameObject player;
  Hero heroScript;
  AudioSource soundPlayer;
  GameObject soundObject;
  SoundManager soundManager;

  private NavMeshAgent agent;
  private int numberCollisions = 0;
  private float navigationTime = 0;
  private bool isSpawned;

  public Animator pedeAnimate;
  public ParticleSystem spawned;
  public Transform target;
  public float distanceToTarget;
  public bool dead = false;   //was static
  public float navigationUpdate;
  public float attackTimer;
  public float attackTime = .2f;
  public float distanceToPlayer;
  public bool isHit;
  public bool isDying;


  void Awake()
  {
    spawned = GetComponentInChildren<ParticleSystem>();


  }
  void Start()
  {

    agent = GetComponent<NavMeshAgent>();

    player = GameObject.Find("OVRPlayerController");

    soundObject = GameObject.Find("SoundManager");
    soundManager = soundObject.GetComponent<SoundManager>();
    soundPlayer = gameObject.GetComponentInChildren<AudioSource>();
    heroScript = player.GetComponentInChildren<Hero>();

    NavMeshHit closestHit;

    if (NavMesh.SamplePosition(transform.position, out closestHit, 100f, NavMesh.AllAreas))
    {
      transform.position = closestHit.position;
      agent.enabled = true;
    }
  
    agent.updateRotation = false;


    //simulates pede bursting out of sand
    pedeAnimate = agent.GetComponentInChildren<Animator>();
    spawned.Play();
    isSpawned = spawned.emission.enabled;


  }

  void OnCollisionEnter(Collision collider)
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

        soundPlayer.volume = 1f;
        soundPlayer.PlayOneShot(soundManager.pedeHurt);
        VibrationManager.singleton.TriggerVibration(20, 2, 155, OVRInput.Controller.RTouch);
        ++numberCollisions;
      }

      if (numberCollisions >= deathBlow)
      {

        dead = true;
        gameObject.GetComponent<AudioSource>().Stop();
        pedeAnimate.SetTrigger("pedeDie");
        soundPlayer.PlayOneShot(soundManager.pedeHurt);
        StartCoroutine("deadPedeTimer");
        agent.updatePosition = false;
        agent.GetComponent<Rigidbody>().isKinematic = true;

        numberCollisions = 0;
        deathBlow = 30;

      }

    }

    if (collider.gameObject.tag == "Player")
    {
      var magnitude = 1500;
      var force = transform.position - collider.transform.position;
      force.Normalize();

      gameObject.GetComponent<Rigidbody>().AddForce(force * magnitude);


    }

  }

  IEnumerator deadPedeTimer()
  {
    yield return new WaitForSeconds(3);
    Destroy(gameObject);

  }


  void Update()
  {

    target = player.transform;
    Vector3 targetRotation = new Vector3(target.position.x, agent.transform.position.y, target.position.z);
    if (target != null && dead == false)
    {

      navigationTime += Time.deltaTime;
      if (navigationTime > navigationUpdate)
      {
        if (heroScript.isDead != true && target != null)
        {
          targetRotation = new Vector3(target.transform.position.x, agent.transform.position.y, target.transform.position.z);
          transform.LookAt(targetRotation);
          agent.destination = target.position;
          navigationTime = 0;
        }
       


      }
      if (heroScript.isDead == true)
      {
        agent.updatePosition = false;
      }
   

    }

    distanceToPlayer = Vector3.Distance(this.transform.position, target.position);
    if (distanceToPlayer < 8.0f)
    {
      agent.speed = 8;
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
