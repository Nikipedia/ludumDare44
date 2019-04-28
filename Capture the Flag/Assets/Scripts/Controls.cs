using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Controls : MonoBehaviour
{
    public Slider sounds;
    public Slider music;
    public Slider sensitivityMult;
    public AudioMixer master;
    public MovementScript mover;
    public Button exit;
    // Start is called before the first frame update
    void Start()
    {
        sounds.onValueChanged.AddListener(SoundChanged);
        music.onValueChanged.AddListener(MusicChanged);
        exit.onClick.AddListener(ExitFunc);
    }

    void ExitFunc()
    {
        mover.ChangeMouseSensitivity(sensitivityMult.value);
        PlayerPrefs.SetFloat("mouseSens", sensitivityMult.value);
        //Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }

    public void SoundChanged(float value)
    {
        master.SetFloat("SoundVol", -Mathf.Pow(value-1,2) * 50);
        PlayerPrefs.SetFloat("soundVol", value);
    }

    public void MusicChanged(float value)
    {
        master.SetFloat("MusicVol", -Mathf.Pow(value - 1, 2) * 50);
        PlayerPrefs.SetFloat("musicVol", value);
    }
}
