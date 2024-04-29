using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour 
{
    public TextMeshProUGUI startButtonText;
    public TextMeshProUGUI settingsButtonText;
    public TextMeshProUGUI exitButtonText;

    
    [Header("Loading Levels")]
    public string newGame;
    private string loadGameLevel;
    [SerializeField] private GameObject noSaveToLoad = null;


    [Header("Volume Settings")]
    [SerializeField] private TextMeshProUGUI volumeText = null;
    [SerializeField] private Slider volumeSlider = null; 


    [Header("Graphical Settings")]
    [SerializeField] private Slider brightnessSlider = null;
    [SerializeField] private TextMeshProUGUI brightnessText = null;
    [SerializeField] private float brightness = 1;
    private bool isFullScreen;
    private float brightnessValue;


    public GameObject confirmationPrompt = null;

    
    public void NewGameSelectionYes(){
        SceneManager.LoadScene(newGame);
    }


    public void LoadGameSelectionYes(){
        if (PlayerPrefs.HasKey("SavedLevel")){
            loadGameLevel = PlayerPrefs.GetString("SavedLevel");
            SceneManager.LoadScene(loadGameLevel);
        }
        else {
            noSaveToLoad.SetActive(true);
        }

    }

    public void OpenSettings(){
        Debug.Log("Opening Settings");
        //SceneManager.LoadScene("SettingsMenu");

    }


    public void ExitGame(){
        Debug.Log("Quiting Game");
        Application.Quit();
    }


    public void SetVolumeSetting(float volume){
        AudioListener.volume = volume;
        volumeText.text = volume.ToString("0.0");
    }

    public void ApplyVolume() {
        PlayerPrefs.SetFloat("masterVolume", AudioListener.volume);
        StartCoroutine(ConfirmationPrompt());
    }


    public IEnumerator ConfirmationPrompt(){
        confirmationPrompt.SetActive(true);
        yield return new WaitForSeconds(3);
        confirmationPrompt.SetActive(false);
    }


    public void SetBrightness(float brightness){
        brightnessValue = brightness;
        brightnessText.text = brightness.ToString("0.0");
    }


    public void FullScreen(bool currentScreen){
        isFullScreen = currentScreen;
    }
    

    public void ApplyGraphics(){
        PlayerPrefs.SetFloat("masterBrightness", brightnessValue);

        PlayerPrefs.SetInt("masterFullScreen", (isFullScreen) ? 0 : 1);
        Screen.fullScreen = isFullScreen;

        StartCoroutine(ConfirmationPrompt());

    }



}




