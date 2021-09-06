using Entitas;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public GameObject NewGameButton;
    public GameObject LoadGameButton;
    public GameObject SaveGameButton;
    public GameObject RestartGameButton;
    public GameObject ExitGameButton;
    public GameObject SelectLevel;

    public GameController GameController;

    private Contexts _contexts;

    private Systems _saveSystems;

    void Start()
    {
        Contexts _contexts = Contexts.sharedInstance;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale != 0)
            {
                Time.timeScale = 0;
                NewGameButton.SetActive(true);
                LoadGameButton.SetActive(true);
                SaveGameButton.SetActive(true);
                RestartGameButton.SetActive(true);
                ExitGameButton.SetActive(true);
            }
            else
            {
                Time.timeScale = 1;
                DeactivateAll();
            }
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void SaveGame()
    {
        PlayerPrefs.DeleteAll();

        _contexts = Contexts.sharedInstance;

        if (_saveSystems == null)
        {
            _saveSystems = new Systems();
            _saveSystems
                .Add(new SaveEdibleSystem(_contexts))
                .Add(new SaveSnakeSystem(_contexts))
                .Add(new SaveOccupiedSystem(_contexts))
                ;
        }

        _saveSystems.Execute();
        PlayerPrefs.Save();
    }

    public void LoadGame()
    {
        GameController.LoadGame();
    }
 
    public void NewGame()
    {
        GameController.NewGame();
    }

    public void Restart()
    {
        GameController.Restart();
    }

    private void DeactivateAll()
    {
        NewGameButton.SetActive(false);
        LoadGameButton.SetActive(false);
        SaveGameButton.SetActive(false);
        RestartGameButton.SetActive(false);
        ExitGameButton.SetActive(false);
        SelectLevel.SetActive(false);
    }
}
