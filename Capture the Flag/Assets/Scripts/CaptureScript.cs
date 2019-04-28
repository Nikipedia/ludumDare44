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
    public AudioSource calm, aggressive;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = startPoint;
        ui.UpdatePlayerStats(lives);
    }

    public void receiveFlag()
    {
        hasFlag = true;
        ui.EnableFlagHint();
        calm.Stop();
        aggressive.Play();
        
    }

    public void loseLives(int delta)
    {
        lives -= delta;
        ui.UpdatePlayerStats(lives);
        if(lives < 1 )
        {
            StopAllMusic();
            GameScript.getGame().GameOver();
        }
    }

    public void removeFlag()
    {
        hasFlag = false;
        ui.DisableFlagHint();
        aggressive.Stop();
        calm.Play();
    }

    public void StopAllMusic()
    {
        calm.Stop();
        aggressive.Stop();
    }

    public void toStartAgain()
    {
        ouch.Play();
        lives--;
        ui.UpdatePlayerStats(lives);
        if (hasFlag)
        {
            hasFlag = false;
            ui.DisableFlagHint();
            GameScript.getGame().LostFlag();
            aggressive.Stop();
            calm.Play();
        }
        transform.position = startPoint;
        if (lives < 1)
        {
            StopAllMusic();
            GameScript.getGame().GameOver();
        }
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
