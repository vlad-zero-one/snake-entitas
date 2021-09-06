using Entitas.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public GameObject ContinueButton;
    public GameObject LoadGameButton;
    public GameObject NewGameButton;
    public GameObject ExitGameButton;

    public GameController GameController;

    private Contexts _contexts;

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
                ContinueButton.SetActive(true);
                NewGameButton.SetActive(true);
                ExitGameButton.SetActive(true);
            }
            else
            {
                Time.timeScale = 1;
                ContinueButton.SetActive(false);
                NewGameButton.SetActive(false);
                ExitGameButton.SetActive(false);
            }
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void SaveGame()
    {
        //new Feature("SaveGame");

        PlayerPrefs.DeleteAll();

        _contexts = Contexts.sharedInstance;

        var ed = new SaveEdibleSystem(_contexts);
        var sn = new SaveSnakeSystem(_contexts);
        var oc = new SaveOccupiedSystem(_contexts);
        ed.Execute();
        sn.Execute();
        oc.Execute();

        PlayerPrefs.Save();
    }

    public void LoadGame()
    {
        /*
        var s = new RestartSystem(_contexts);
        s.Execute();

        _contexts = Contexts.sharedInstance;

        _contexts.game.globals.value.BorderSize = PlayerPrefs.GetInt("BorderSize");

        // Set Borders here

        var le = new LoadEdibleSystem(_contexts);
        le.Execute();
        var ls = new LoadSnakeSystem(_contexts);
        ls.Execute();
        var lo = new LoadOccupiedSystem(_contexts);
        lo.Execute();
        */
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
}
