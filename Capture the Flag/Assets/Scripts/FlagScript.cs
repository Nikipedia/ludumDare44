using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagScript : MonoBehaviour
{
    public bool givesFlag;
    public AudioSource getFlag;
    public GameObject successParticles;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Activate()
    {
        gameObject.SetActive(true);
    }


    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            
            if(givesFlag && !other.GetComponent<CaptureScript>().hasFlag)
            {
                other.GetComponent<CaptureScript>().receiveFlag();
                gameObject.SetActive(false);
                GameScript.getGame().ReceivedFlag();
                getFlag.Play();
                successParticles.GetComponent<ParticleSystem>().Play();
            }
            else if(!givesFlag && other.GetComponent<CaptureScript>().hasFlag)
            {
                other.GetComponent<CaptureScript>().removeFlag();
                GameScript.getGame().IncreaseScore();
                getFlag.Play();
                successParticles.GetComponent<ParticleSystem>().Play();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
