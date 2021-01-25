using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using Mirror;

public class Settings : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider VolumeSlider;
    public Slider FOVSlider;
    public Slider SensitivitySlider;

    // public void saveRouted(float sliderValue)
    // {
    //     if(transform.GetComponent<MenuInterface>().Player)
    //         transform.GetComponent<MenuInterface>().Player.transform.GetComponent<PlayerScripts>().SaveVolume(sliderValue);
    //     else SaveVolume(sliderValue);
    // }
    public void SaveVolume(float sliderValue)
    {
        //Debug.Log("update");
        mixer.SetFloat("Volume", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("Volume", sliderValue);
    }
    public void LoadVolume()
    {
        float sliderValue = PlayerPrefs.GetFloat("Volume");
        VolumeSlider.value = sliderValue;
        mixer.SetFloat("Volume", Mathf.Log10(sliderValue) * 20);
    }
    public void SaveFOV(float sliderValue)
    {
        if(transform.GetComponent<MenuInterface>().Player)
            transform.GetComponent<MenuInterface>().Player.transform.GetChild(3).gameObject.transform.GetChild(1).gameObject.GetComponent<Camera>().fieldOfView = sliderValue;
        PlayerPrefs.SetFloat("FOV", sliderValue);
    }
    public void LoadFOV()
    {
        FOVSlider.value = PlayerPrefs.GetFloat("FOV");
    }
    public void insFOV()
    {
       transform.GetComponent<MenuInterface>().Player.transform.GetChild(3).gameObject.transform.GetChild(1).gameObject.GetComponent<Camera>().fieldOfView = FOVSlider.value;
    }
    public void SaveSensitivity(float sliderValue)
    {
        if(transform.GetComponent<MenuInterface>().Player)
            transform.GetComponent<MenuInterface>().Player.GetComponent<Movement>().mouseSensitivity = sliderValue;
        PlayerPrefs.SetFloat("Sensitivity", sliderValue);
    }
    public void LoadSensitivity()
    {
        SensitivitySlider.value = PlayerPrefs.GetFloat("Sensitivity");
    }
    public void insSensitivity()
    {
       transform.GetComponent<MenuInterface>().Player.GetComponent<Movement>().mouseSensitivity = SensitivitySlider.value;
    }

}
