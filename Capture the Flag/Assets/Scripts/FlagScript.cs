using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagScript : MonoBehaviour
{
    public bool givesFlag;
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
            }
            else if(!givesFlag && other.GetComponent<CaptureScript>().hasFlag)
            {
                other.GetComponent<CaptureScript>().removeFlag();
                GameScript.getGame().IncreaseScore();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
