using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public enum Difficulty { EASY, MEDIUM, HARD }
    private Difficulty difficulty = Difficulty.EASY;

    public static GameManager Instance;

    [SerializeField] private bool gamePaused;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }

    
    // Start is called before the first frame update
    private void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == "Intro" || currentScene.name == "MainMenu")
        {
            InputManager.Instance.EnableUIInput();
        }
        else 
        {
            InputManager.Instance.EnablePlayerInput();
        }
    }
    /*
    // Update is called once per frame
    private void Update()
    {

    }
    */

    public void LoadSceneByName(string sceneName) 
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadFirstLevel()
    {
        InputManager.Instance.EnablePlayerInput();
        SceneManager.LoadScene("FirstLevel");
    }

    public void LoadSecondLevel()
    {
        InputManager.Instance.EnablePlayerInput();
        SceneManager.LoadScene("SecondLevel");
    }

    public void LoadTutorial()
    {
        InputManager.Instance.EnablePlayerInput();
        SceneManager.LoadScene("Tutorial");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ExitGame() {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public string GetCurrentSceneName() { return SceneManager.GetActiveScene().name; }

    public void SetGamePaused(bool paused) { gamePaused = paused; }

    public bool GetGamePaused() { return gamePaused; }

    public Difficulty GetDifficulty() {
        return difficulty;
    }

    public void SetDifficulty(Difficulty dif)
    {
        difficulty = dif;
    }
}
