using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public GameObject optionsPanel;
    public GameObject creditsPanel;
    public GameObject mainPanel;
    public GameObject soundPanel;
    public GameObject controlsPanel;

    public GameObject eventSystem;
    private bool gamepadConected;

    // Use this for initialization
    private void Start ()
    {
        gamepadConected = Input.GetJoystickNames().Length > 0;
        if (gamepadConected)
            eventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(GameObject.Find("Start"));
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
            eventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(GameObject.Find("Sound"));
    }

    public void ShowControlsOptions()
    {
        controlsPanel.SetActive(true);
        soundPanel.SetActive(false);
        mainPanel.SetActive(false);

        if (gamepadConected)
            eventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(GameObject.Find("Controls"));
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
        if (gamepadConected)
            eventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(GameObject.Find("Start"));

        //play anim for opening game options panel
        //anim.Play("OptTweenAnim_on");
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("NicoSampleScene");
    }

    public void SetMusicEnabled(float value)
    {

    }

    public void SetMusicVolume(float value)
    {

    }
}
