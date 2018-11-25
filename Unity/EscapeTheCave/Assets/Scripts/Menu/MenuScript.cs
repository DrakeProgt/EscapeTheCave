using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    public GameObject optionsPanel;
    public GameObject creditsPanel;
    public GameObject mainPanel;
    public GameObject soundPanel;
    public GameObject controlsPanel;
    public GameObject B;

    public GameObject eventSystem;
    private bool gamepadConected;
    private GameObject lastButtonPressed;

    // Use this for initialization
    private void Start ()
    {
        lastButtonPressed = null;
        gamepadConected = Input.GetJoystickNames().Length > 0;
        if (gamepadConected)
            eventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(GameObject.Find("Start"));
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
            if (gamepadConected && lastButtonPressed != null)
            {
                eventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(lastButtonPressed);
                B.SetActive(false);
                if (lastButtonPressed.name == "VolumeButton")
                    lastButtonPressed = GameObject.Find("Sound");
                else
                    lastButtonPressed = null;
            }
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ShowOptions()
    {
        //enable respective panel
        optionsPanel.SetActive(true);
        mainPanel.SetActive(false);
        creditsPanel.SetActive(false);
        if (gamepadConected)
            eventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(GameObject.Find("Sound"));

        soundPanel.SetActive(true);
        controlsPanel.SetActive(false);

        //play anim for opening game options panel
        //anim.Play("OptTweenAnim_on");
    }

    public void ShowSoundOptions()
    {
        soundPanel.SetActive(true);
        controlsPanel.SetActive(false);
        mainPanel.SetActive(false);

        if (gamepadConected)
        {
            eventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(GameObject.Find("Sound"));

        }

        //GameObject.Find("TextSound").GetComponent<Text>().color = new Color(0, 1, 1, 1);
        //GameObject.Find("TextControls").GetComponent<Text>().color = new Color(1, 1, 1, 1);

        if (gamepadConected)
            eventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(GameObject.Find("MusicButton"));

        lastButtonPressed = GameObject.Find("Sound");
    }

    public void ShowControlsOptions()
    {
        controlsPanel.SetActive(true);
        soundPanel.SetActive(false);
        mainPanel.SetActive(false);

        if (gamepadConected)
            eventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(GameObject.Find("Controls"));

        //GameObject.Find("TextSound").GetComponent<Text>().color = new Color(1, 1, 1, 1);
        //GameObject.Find("TextControls").GetComponent<Text>().color = new Color(0, 1, 1, 1);

        lastButtonPressed = GameObject.Find("Controls");
    }

    public void ShowCredits()
    {
        //enable respective panel
        creditsPanel.SetActive(true);
        mainPanel.SetActive(false);
        optionsPanel.SetActive(false);
        if (gamepadConected)
            eventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(GameObject.Find("BackCredits")); ;

        //play anim for opening game options panel
        //anim.Play("OptTweenAnim_on");
    }

    public void ShowMain()
    {
        //enable respective panel
        mainPanel.SetActive(true);
        optionsPanel.SetActive(false);
        creditsPanel.SetActive(false);
        controlsPanel.SetActive(false);
        soundPanel.SetActive(false);
        if (gamepadConected)
            eventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(GameObject.Find("Start"));

        //play anim for opening game options panel
        //anim.Play("OptTweenAnim_on");
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("NicoSampleScene");
    }

    public void EditMusic()
    {
        float value = GameObject.Find("SliderMusic").GetComponent<Slider>().value;
        if (value == 0)
            value = 1;
        else
            value = 0;
        GameObject.Find("SliderMusic").GetComponent<Slider>().value = value;
    }

    public void EditVolume()
    {
        if (gamepadConected)
            eventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(GameObject.Find("SliderVolume"));
        
        lastButtonPressed = GameObject.Find("VolumeButton");
        if (gamepadConected)
            B.SetActive(true);
    }

    public void SetMusicEnabled(float value)
    {
        GameObject sliderHandle = GameObject.Find("HandleMusic");
        if (sliderHandle == null)
            return;

        if (value == 0)
            sliderHandle.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        else
            sliderHandle.GetComponent<Image>().color = new Color(0, 1, 1, 1);

        //TODO: set music in GameManager
    }

    public void SetMusicVolume(float value)
    {
        //TODO: set volume in GameManager
    }
}
