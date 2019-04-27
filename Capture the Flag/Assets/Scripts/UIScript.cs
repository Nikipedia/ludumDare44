using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    public Text hpText;
    public Text time;
    public Text score;
    public Text dashCD;
    public GameObject dashing;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ToggleDashing()
        {
        if(dashing.activeInHierarchy)
        {
            dashing.SetActive(false);
        }
        else
        {
            dashing.SetActive(true);
        }
    }

    public void UpdatePlayerStats(int hp)
    {
        hpText.text = "" + hp;
    }

    public void UpdateDashCD(string remCD)
    {
        dashCD.text = "" + remCD;
    }

    public void UpdateGameStats(int timeRem, int newScore)
    {
        score.text = "Flags: " + newScore;
        time.text = "Time Remaining: " + timeRem + "s";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
