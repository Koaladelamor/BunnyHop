using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager_Scene : MonoBehaviour
{

    public static Manager_Scene _SCENE_MANAGER;

    private void Awake()
    {
        if (_SCENE_MANAGER != null && _SCENE_MANAGER != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _SCENE_MANAGER = this;
            DontDestroyOnLoad(this);
        }
    }

    
    // Start is called before the first frame update
    private void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == "Intro" || currentScene.name == "MainMenu")
        {
            Manager_Input._INPUT_MANAGER.EnableUIInput();
        }
        else 
        {
            Manager_Input._INPUT_MANAGER.EnablePlayerInput();
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
        Manager_Input._INPUT_MANAGER.EnablePlayerInput();
        SceneManager.LoadScene("FirstLevel");
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
}
