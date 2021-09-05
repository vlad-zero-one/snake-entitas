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
        PlayerPrefs.DeleteAll();

        _contexts = Contexts.sharedInstance;

        PlayerPrefs.SetInt("BorderSize", _contexts.game.globals.value.BorderSize);

        PlayerPrefs.SetInt("EdibleX", _contexts.game.edibleEntity.position.value.X);
        PlayerPrefs.SetInt("EdibleY", _contexts.game.edibleEntity.position.value.Y);

        var barriers = _contexts.game.globals.value.BarrierPositions;
        PlayerPrefs.SetInt("BarrierSize", barriers.Count);
        for(int i = 0; i < barriers.Count; i++)
        {
            PlayerPrefs.SetInt("Barrier" + i + "X", barriers[i].X);
            PlayerPrefs.SetInt("Barrier" + i + "Y", barriers[i].Y);
        }

        PlayerPrefs.SetInt("Direction", (int)_contexts.game.direction.value);
        PlayerPrefs.SetInt("LastMovementDirection", (int)_contexts.game.lastMovementDirection.value);

        var snake = _contexts.game.globals.value.SnakePositionsExceptHead;
        PlayerPrefs.SetInt("SnakeSize", snake.Count + 1);
        for (int i = 0; i < snake.Count; i++)
        {
            PlayerPrefs.SetInt("Snake" + i + "X", snake[i].X);
            PlayerPrefs.SetInt("Snake" + i + "Y", snake[i].Y);
        }
        PlayerPrefs.SetInt("Snake" + snake.Count + "X", _contexts.game.headEntity.position.value.X);
        PlayerPrefs.SetInt("Snake" + snake.Count + "Y", _contexts.game.headEntity.position.value.Y);

        int isGrowing = _contexts.game.headEntity.isGrowing == true ? 1 : 0;
        PlayerPrefs.SetInt("IsGrowing", isGrowing);

        PlayerPrefs.Save();
    }

    public void LoadGame()
    {

        var s = new RestartSystem(_contexts);
        s.Execute();

        _contexts = Contexts.sharedInstance;

        _contexts.game.globals.value.BorderSize = PlayerPrefs.GetInt("BorderSize");

        // Set Borders here

        LoadEdible(_contexts);

        //Debug.Log(PlayerPrefs.GetInt("SnakeSize"));
        //Debug.Log(PlayerPrefs.GetInt("BarrierSize"));


        _contexts.game.direction.value = (DirectionEnum)PlayerPrefs.GetInt("Direction");
        _contexts.game.lastMovementDirection.value = (DirectionEnum)PlayerPrefs.GetInt("LastMovementDirection");

        _contexts.game.globals.value.SnakePositionsExceptHead.Clear();

        GameObject snake = GameObject.Find("Snake");
        var cellPrefab = _contexts.game.globals.value.SnakeCell;
        var snakePositionsExceptHead = _contexts.game.globals.value.SnakePositionsExceptHead;
        IntVec2 position = new IntVec2(PlayerPrefs.GetInt("Snake0X"), PlayerPrefs.GetInt("Snake0Y"));
        GameEntity head, tail;
        //if (!_contexts.game.isTail)
        //{
        tail = _contexts.game.CreateEntity();
        tail.isTail = true;
        tail.isSnake = true;
        tail.ReplacePosition(position);
        snakePositionsExceptHead.Add(position);
        var go = SnakeSegment.Instantiate(cellPrefab, position.X, position.Y, snake);
        go.Link(tail);
        tail.ReplaceGameObject(go);
        //}
        _contexts.game.tailEntity.ReplacePosition(position);

        GameEntity entityForQueue = tail;
        for (int i = 1; i < PlayerPrefs.GetInt("SnakeSize") - 1; i++)
        {
            position = new IntVec2(PlayerPrefs.GetInt("Snake" + i + "X"), PlayerPrefs.GetInt("Snake" + i + "Y"));
            var segment = _contexts.game.CreateEntity();
            segment.isSnake = true;
            segment.ReplacePosition(position);
            snakePositionsExceptHead.Add(position);
            go = SnakeSegment.Instantiate(cellPrefab, position.X, position.Y, snake);
            go.Link(segment);
            segment.ReplaceGameObject(go);

            entityForQueue.ReplacePreviousSegment(segment);
            entityForQueue = segment;
        }

        int lastIndex = PlayerPrefs.GetInt("SnakeSize") - 1;
        position = new IntVec2(PlayerPrefs.GetInt("Snake" + lastIndex + "X"), PlayerPrefs.GetInt("Snake" + lastIndex + "Y"));
        head = _contexts.game.CreateEntity();
        head.isHead = true;
        head.isSnake = true;
        head.ReplacePosition(position);
        go = SnakeSegment.Instantiate(cellPrefab, position.X, position.Y, snake);
        go.Link(head);
        head.ReplaceGameObject(go);
        _contexts.game.headEntity.ReplacePosition(position);
        entityForQueue.ReplacePreviousSegment(head);

        int isGrowing = PlayerPrefs.GetInt("IsGrowing");
        _contexts.game.headEntity.isGrowing = isGrowing == 1 ? true : false;

    }

    private void LoadEdible(Contexts contexts)
    {
        var ediblePrefab = _contexts.game.globals.value.EdibleCell;
        GameObject edibleParent = GameObject.Find("Edible");

        GameEntity edibleEntity;
        if (!_contexts.game.isEdible)
        {
            Debug.Log("Recreate edible");
            edibleEntity = _contexts.game.CreateEntity();
            edibleEntity.ReplacePosition(new IntVec2(PlayerPrefs.GetInt("EdibleX"), PlayerPrefs.GetInt("EdibleY")));
            edibleEntity.isEdible = true;

            var go = GameObject.Instantiate(
                ediblePrefab,
                new Vector2(edibleEntity.position.value.X, edibleEntity.position.value.Y),
                Quaternion.identity,
                edibleParent.transform);
            go.Link(edibleEntity);
            edibleEntity.ReplaceGameObject(go);
        }
        else
        {
            Debug.Log("Try get from contexts");
            edibleEntity = _contexts.game.edibleEntity;
            edibleEntity.ReplacePosition(new IntVec2(PlayerPrefs.GetInt("EdibleX"), PlayerPrefs.GetInt("EdibleY")));

        }
        _contexts.game.edibleEntity.gameObject.value.transform.position = new Vector2(_contexts.game.edibleEntity.position.value.X,
            _contexts.game.edibleEntity.position.value.Y);
    }

    public void NewGame()
    {
        
    }

    public void Restart()
    {
        GameController.Restart();
    }
}
