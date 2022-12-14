using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject pnl_menu;
    [SerializeField] private GameObject pnl_settings;
    [SerializeField] private GameObject pnl_levelSelect;
    [SerializeField] private GameObject pnl_credits;

    [SerializeField] private Button btn_easy;
    [SerializeField] private Button btn_medium;
    [SerializeField] private Button btn_hard;

    [SerializeField] private EventSystem eventSystem;

    /*[SerializeField] private Button btn_play;
    [SerializeField] private Button btn_settings;
    [SerializeField] private Button btn_credits;
    [SerializeField] private Button btn_exit;

    [SerializeField] private Button btn_tutorial;
    [SerializeField] private Button btn_level_01;

    [SerializeField] private Button btn_back_settings;
    [SerializeField] private Button btn_back_levelSelect;*/

    private void Awake()
    {
        pnl_menu = GameObject.Find("pnl_MainMenu");
        pnl_settings = GameObject.Find("pnl_Settings");
        pnl_levelSelect = GameObject.Find("pnl_LevelSelect");
        pnl_credits = GameObject.Find("pnl_Credits");

        btn_easy = GameObject.Find("btn_easy").GetComponent<Button>();
        btn_medium = GameObject.Find("btn_medium").GetComponent<Button>();
        btn_hard = GameObject.Find("btn_hard").GetComponent<Button>();

        eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();

        /*Button[] _allButtons = GetComponentsInChildren<Button>();
        foreach (Button _button in _allButtons)
        {
            if (_button.name == "Play") 
            {
                btn_play = _button;
            }
            else if (_button.name == "Settings")
            {
                btn_settings = _button;
            }
            else if (_button.name == "Credits")
            {
                btn_credits = _button;
            }
            else if (_button.name == "Exit")
            {
                btn_exit = _button;
            }
            else if (_button.name == "btn_GoBack_Settings")
            {
                btn_back_settings = _button;
            }
            else if (_button.name == "btn_GoBack_LevelSelect")
            {
                btn_back_levelSelect = _button;
            }
            else if (_button.name == "btn_tutorial")
            {
                btn_tutorial = _button;
            }
            else if (_button.name == "btn_level_01")
            {
                btn_level_01 = _button;
            }
        }*/
    }

    // Start is called before the first frame update
    private void Start()
    {
        /*if (btn_play.onClick == null) {
            btn_play.onClick.AddListener(DisplayLevelSelect);
        }
        if (btn_settings.onClick == null)
        {
            btn_settings.onClick.AddListener(DisplaySettings);
        }
        if (btn_credits.onClick == null)
        {
            btn_credits.onClick.AddListener(DisplaySettings);
        }*/

        /*if (btn_exit.onClick == null)
        {
            btn_exit.onClick.AddListener(GameManager.Instance.ExitGame);
        }*/
        /*if (btn_exit)
        {
            btn_exit.onClick.RemoveAllListeners();
            btn_exit.onClick.AddListener(GameManager.Instance.ExitGame);
        }*/

        /*if (btn_back_levelSelect.onClick == null)
        {
            btn_back_levelSelect.onClick.AddListener(DisplayMainMenu);
        }
        if (btn_back_settings.onClick == null)
        {
            btn_back_settings.onClick.AddListener(DisplayMainMenu);
        }*/

        /*if (btn_tutorial.onClick == null)
        {
            btn_tutorial.onClick.AddListener(GameManager.Instance.LoadTutorial);
        }
        if (btn_level_01.onClick == null)
        {
            btn_level_01.onClick.AddListener(GameManager.Instance.LoadFirstLevel);
        }*/

        /*if (btn_tutorial)
        {
            btn_tutorial.onClick.RemoveAllListeners();
            btn_tutorial.onClick.AddListener(GameManager.Instance.LoadTutorial);
        }
        if (btn_level_01)
        {
            btn_level_01.onClick.RemoveAllListeners();
            btn_level_01.onClick.AddListener(GameManager.Instance.LoadFirstLevel);
        }*/

        pnl_settings.SetActive(false);
        pnl_levelSelect.SetActive(false);
        pnl_credits.SetActive(false);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Update is called once per frame
    /*private void Update()
    {
        
    }*/

    public void DisplaySettings() 
    {
        pnl_menu.SetActive(false);
        pnl_settings.SetActive(true);

        if (GameManager.Instance) {
            GameManager.Difficulty currentDifficulty = GameManager.Instance.GetDifficulty();
            switch (currentDifficulty)
            {
                case GameManager.Difficulty.EASY:
                    eventSystem.SetSelectedGameObject(btn_easy.gameObject);
                    break;
                case GameManager.Difficulty.MEDIUM:
                    eventSystem.SetSelectedGameObject(btn_medium.gameObject);
                    break;
                case GameManager.Difficulty.HARD:
                    eventSystem.SetSelectedGameObject(btn_hard.gameObject);
                    break;
                default:
                    break;
            }
        }
    }

    public void DisplayMainMenu()
    {
        pnl_settings.SetActive(false);
        pnl_levelSelect.SetActive(false);
        pnl_credits.SetActive(false);
        pnl_menu.SetActive(true);
    }

    public void DisplayCredits() 
    {
        pnl_menu.SetActive(false);
        pnl_credits.SetActive(true);
    }

    public void DisplayLevelSelect()
    {
        pnl_menu.SetActive(false);
        pnl_levelSelect.SetActive(true);
    }

    public void ExitButton() {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void LoadTutorial() {
        if (GameManager.Instance)
        {
            GameManager.Instance.LoadTutorial();
        }
        else Debug.Log("GameManager is NULL");
    }

    public void LoadFirstLevel()
    {
        if (GameManager.Instance)
        {
            GameManager.Instance.LoadFirstLevel();
            InputManager.Instance.EnablePlayerInput();
        }
        else Debug.Log("GameManager is NULL");
    }

    public void LoadSecondLevel()
    {
        if (GameManager.Instance)
        {
            GameManager.Instance.LoadSecondLevel();
            InputManager.Instance.EnablePlayerInput();
        }
        else Debug.Log("GameManager is NULL");
    }

    public void EasyDifficulty() 
    {
        GameManager.Instance.SetDifficulty(GameManager.Difficulty.EASY);
    }

    public void MediumDifficulty()
    {
        GameManager.Instance.SetDifficulty(GameManager.Difficulty.MEDIUM);
    }

    public void HardDifficulty()
    {
        GameManager.Instance.SetDifficulty(GameManager.Difficulty.HARD);
    }

}
