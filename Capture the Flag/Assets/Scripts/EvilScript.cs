using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class EvilScript : MonoBehaviour
{
    public NavMeshAgent agent;
    public bool foundPlayer;
    private Transform player;
    private Vector3 startPos;
    public AudioSource barking;
    // Use this for initialization
    void Start()
    {
        startPos = transform.position;
        StartCoroutine(moveRandom());
    }

    IEnumerator moveRandom()
    {
        while(!foundPlayer)
            {
            Vector3 aimLoc = Random.insideUnitCircle * 10;
            aimLoc.x = aimLoc.x + startPos.x;
            aimLoc.z = aimLoc.y + startPos.z;
            aimLoc.y = 0;
            agent.SetDestination(aimLoc);
            yield return new WaitForSeconds(5);
        }
    }

    IEnumerator movePlayer()
    {
        while(foundPlayer)
        {
            agent.SetDestination(player.position);
            yield return new WaitForSeconds(0.25f);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            player = other.transform;
            foundPlayer = true;
            StartCoroutine(movePlayer());
            if(!barking.isPlaying)
            {
                barking.Play();
            }
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<CaptureScript>()!=null)
        {
            collision.gameObject.GetComponent<CaptureScript>().toStartAgain();
            GameScript.getGame().SpawnNewEnemy();
            Destroy(gameObject);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            StopAllCoroutines();
            foundPlayer = false;
            StartCoroutine(moveRandom());
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
