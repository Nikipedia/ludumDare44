using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class EvilScript : MonoBehaviour
{
    public NavMeshAgent agent;
    public bool foundPlayer;
    private Transform player;
    // Use this for initialization
    void Start()
    {
        StartCoroutine(moveRandom());
    }

    IEnumerator moveRandom()
    {
        while(!foundPlayer)
            {
            Vector3 aimLoc = Random.insideUnitCircle * 10;
            aimLoc.x = aimLoc.x + transform.position.x;
            aimLoc.z = aimLoc.y + transform.position.z;
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
            agent.SetDestination(player.position);
            foundPlayer = true;
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
