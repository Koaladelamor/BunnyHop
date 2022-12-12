using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraUI : MonoBehaviour
{
    private GameObject _GUI;
    private GameObject _beatBar;
    private GameObject _menu;

    private bool menuOnScreen;

    private void Awake()
    {
        _GUI = GameObject.FindGameObjectWithTag("GUI");
        _beatBar = GetComponentInChildren<BeatBar>().gameObject;
        _menu = GameObject.FindGameObjectWithTag("MenuInGame");
    }

    // Start is called before the first frame update
    private void Start()
    {
        _menu.SetActive(false);
        menuOnScreen = false;
    }

    // Update is called once per frame
    private void Update()
    {
        if (InputManager.Instance.GetMenuButtonDown() && menuOnScreen) 
        {
            Debug.Log("menu OFF");
            menuOnScreen = false;
            InputManager.Instance.EnablePlayerInput();
            ContinueButton();
        }
        else if (InputManager.Instance.GetMenuButtonDown() && !menuOnScreen)
        {
            Debug.Log("menu ON");
            menuOnScreen = true;
            InputManager.Instance.EnableUIInput();
            DisplayMenuInGame();
            Time.timeScale = 0;
        }

    }

    public void DisplayMenuInGame() 
    {
        _GUI.SetActive(false);
        _beatBar.SetActive(false);
        _menu.SetActive(true);
    }

    public void HideMenuInGame()
    {
        _GUI.SetActive(true);
        _beatBar.SetActive(true);
        _menu.SetActive(false);
    }

    public void DisplayGUI()
    {
        _GUI.SetActive(true);
    }

    public void DisplayBeatBar()
    {
        _beatBar.SetActive(true);
    }

    public void ContinueButton() 
    {
        HideMenuInGame();
        Time.timeScale = 1;
    }

    public void RestartButton() 
    {
        GameManager.Instance.LoadFirstLevel();
    }

    public void ExitButton()
    {
        GameManager.Instance.LoadMainMenu();
    }
}
