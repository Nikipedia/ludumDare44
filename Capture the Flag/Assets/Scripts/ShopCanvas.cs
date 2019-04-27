using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopCanvas : MonoBehaviour
{
    public Button dash, doubleJump, extraTime, none;
    public MovementScript move;
    public CaptureScript player;
    // Start is called before the first frame update
    void Start()
    {
        dash.onClick.AddListener(Dash);
        doubleJump.onClick.AddListener(DoubleJ);
        extraTime.onClick.AddListener(ExtraT);
        none.onClick.AddListener(Exit);
    }

    private void OnEnable()
    {
        move.dash = false;
        move.doubleJump = false;
        Cursor.lockState = CursorLockMode.None;
    }

    void Dash()
    {
        move.dash = true;
        player.lives -= 2;
        Exit();
    }

    void DoubleJ()
    {
        move.doubleJump = true;
        player.lives -= 1;
        Exit();
    }

    void ExtraT()
    {
        GameScript.getGame().remainingTime += 30;
        player.lives -= 3;
        Exit();
    }

    private void Exit()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        gameObject.SetActive(false);
    }
}
