using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    public GameObject optionsPanel;
    public GameObject creditsPanel;
    public GameObject mainPanel;
    public GameObject eventSystem;

    // Use this for initialization
    private void Start ()
    {
        if (Input.GetJoystickNames().Length > 0)
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
        eventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(GameObject.Find("Back1"));

        //play anim for opening game options panel
        //anim.Play("OptTweenAnim_on");
    }

    public void ShowCredits()
    {
        //enable respective panel
        creditsPanel.SetActive(true);
        mainPanel.SetActive(false);
        optionsPanel.SetActive(false);
        eventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(GameObject.Find("Back2")); ;

        //play anim for opening game options panel
        //anim.Play("OptTweenAnim_on");
    }

    public void ShowMain()
    {
        //enable respective panel
        mainPanel.SetActive(true);
        optionsPanel.SetActive(false);
        creditsPanel.SetActive(false);
        if (Input.GetJoystickNames().Length > 0)
            eventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(GameObject.Find("Start"));

        //play anim for opening game options panel
        //anim.Play("OptTweenAnim_on");
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("NicoSampleScene");
    }
}
