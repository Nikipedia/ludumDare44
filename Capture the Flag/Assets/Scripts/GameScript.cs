using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameScript : MonoBehaviour
{
    public static GameScript game;
    public int score;
    public float remainingTime;
    public float maxTime;
    public GameObject evilPrefab;
    public List<GameObject> evilInstances;
    public Vector3[] instancePoints;
    public int pointCounter;
    public FlagScript[] flags;
    public GameObject globalArrow;
    public CaptureScript player;
    public UIScript ui;
    public ShopCanvas shop;
    private int prevTime;
    // Use this for initialization
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        remainingTime = maxTime;
        score = 0;
        if(game == null)
        {
            game = this;
        }
        pointCounter = 0;
        for (int i = 0; i < 3; i++)
        {
            Instantiate(evilPrefab, instancePoints[pointCounter % instancePoints.Length], Quaternion.identity);
            pointCounter++;
        }
        Time.timeScale = 0;
        shop.gameObject.SetActive(true);
    }

    public static GameScript getGame()
    {
        if(game != null)
        {
            return game;
        }
        return null;
    }

    public void ReceivedFlag()
    {
        globalArrow.transform.Rotate(0, 0, 180);
    }

    public void LostFlag()
    {
        foreach (FlagScript flag in flags)
        {
            flag.Activate();
        }
        globalArrow.transform.Rotate(0, 0, 180);
    }

    public void IncreaseScore()
    {
        score++;
        ui.UpdateGameStats(prevTime, score);
        foreach (FlagScript flag in flags)
        {
            flag.Activate();
        }
        globalArrow.transform.Rotate(0, 0, 180);
        if (player.lives > 1)
        {
            Time.timeScale = 0;
            shop.gameObject.SetActive(true);
        }
    }

    public void SpawnNewEnemy()
    {
        Instantiate(evilPrefab, instancePoints[pointCounter % instancePoints.Length], Quaternion.identity);
        pointCounter++;
    }

    public void GameOver()
    {

    }

    public void GameWon()
    {

    }

    // Update is called once per frame
    void Update()
    {
        remainingTime -= Time.deltaTime;
        int timer = (int)remainingTime;
        if (prevTime != timer)
        {
            ui.UpdateGameStats(timer, score);
            prevTime = timer;
        }
        if(remainingTime < 0)
        {
            GameOver();
        }
    }
}
