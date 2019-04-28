using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIScript : MonoBehaviour
{
    public Text hpText;
    public Text time;
    public Text score;
    public Text dashCD;
    public GameObject dashing;
    public GameObject WinningScreen;
    public Button PlayAgain;
    public GameObject LosingScreen;
    public Button loseAgain;
    // Start is called before the first frame update
    void Start()
    {
        PlayAgain.onClick.AddListener(PlayOnceMore);
        loseAgain.onClick.AddListener(PlayOnceMore);
    }

    public void PlayOnceMore()
    {
        SceneManager.LoadScene(1);
    }

    public void Win()
    {
        WinningScreen.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        if(PlayerPrefs.GetInt("monsters") < 6)
        PlayerPrefs.SetInt("monsters", PlayerPrefs.GetInt("monsters") + 1);
    }

    public void Lose()
    {
        LosingScreen.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }

    public void EnableDashing()
    {
        dashing.SetActive(true);
    }

    public void DisableDashing()
        {
        dashing.SetActive(false);
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
        score.text = "Flags: " + newScore + "/3";
        time.text = "Time Remaining: " + timeRem + "s";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
