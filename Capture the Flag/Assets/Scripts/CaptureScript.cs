using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptureScript : MonoBehaviour
{
    public bool hasFlag;
    public int lives;
    public Vector3 startPoint;
    public UIScript ui;
    public AudioSource ouch;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = startPoint;
        ui.UpdatePlayerStats(lives);
    }

    public void receiveFlag()
    {
        hasFlag = true;
    }

    public void loseLives(int delta)
    {
        lives -= delta;
        ui.UpdatePlayerStats(lives);
    }

    public void removeFlag()
    {
        hasFlag = false;
    }

    public void toStartAgain()
    {
        ouch.Play();
        lives--;
        ui.UpdatePlayerStats(lives);
        if (hasFlag)
        {
            hasFlag = false;
            GameScript.getGame().LostFlag();
        }
        transform.position = startPoint;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < -20)
        {
            toStartAgain();
        }
    }
}
