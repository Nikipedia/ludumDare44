using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public GameObject coverCanvas;
    public GameObject instructionCanvas;
    public Button toInstruc;
    public Button toScene;
    public Toggle evening;
    // Start is called before the first frame update
    void Start()
    {
        toInstruc.onClick.AddListener(toInstructions);
        toScene.onClick.AddListener(toGame);
        PlayerPrefs.SetInt("monsters", 3);
    }

    public void toInstructions()
    {
        instructionCanvas.SetActive(true);
        coverCanvas.SetActive(false);
    }

    public void toGame()
    {
        PlayerPrefs.SetInt("sky", evening.isOn ? 0 : 1);
        SceneManager.LoadScene(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
