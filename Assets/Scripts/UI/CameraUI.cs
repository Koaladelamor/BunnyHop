using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraUI : MonoBehaviour
{
    private GameObject _GUI;
    private GameObject _beatBar;
    private GameObject _menu;
    private Conductor _conductor;

    private bool menuOnScreen;

    private void Awake()
    {
        _GUI = GameObject.FindGameObjectWithTag("GUI");
        _beatBar = GetComponentInChildren<BeatBar>().gameObject;
        _menu = GameObject.FindGameObjectWithTag("MenuInGame");
        _conductor = GameObject.FindGameObjectWithTag("Conductor").GetComponent<Conductor>();
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
            //Debug.Log("menu OFF");
            //menuOnScreen = false;
            ContinueButton();
            //InputManager.Instance.EnablePlayerInput();
            //Cursor.lockState = CursorLockMode.Locked;
            //GameManager.Instance.SetGamePaused(false);
            _conductor.UnpauseSong();
        }
        else if (InputManager.Instance.GetMenuButtonDown() && !menuOnScreen)
        {
            //Debug.Log("menu ON");
            menuOnScreen = true;
            DisplayMenuInGame();
            //InputManager.Instance.EnableUIInput();
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            GameManager.Instance.SetGamePaused(true);
            _conductor.PauseSong();
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
        //Debug.Log("menu OFF");
        menuOnScreen = false;
        //InputManager.Instance.EnablePlayerInput();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        GameManager.Instance.SetGamePaused(false);
    }

    public void RestartButton() 
    {
        GameManager.Instance.LoadFirstLevel();
    }

    public void MainMenuButton()
    {
        GameManager.Instance.LoadMainMenu();
    }

    public void ExitButton()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
