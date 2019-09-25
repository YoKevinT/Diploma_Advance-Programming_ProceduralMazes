using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(MazeConstructor))]

public class GameController : MonoBehaviour
{
    [SerializeField] private FpsMovement player;
    [SerializeField] private Text timeLabel;
    [SerializeField] private Text scoreLabel;

    private MazeConstructor generator;

    private DateTime startTime;
    private int timeLimit;
    private int reduceLimitBy;

    private int score;
    private bool goalReached;

    void Start()
    {
        generator = GetComponent<MazeConstructor>();
        StartNewGame();
    }

    //Start the game from the beginning, rather than switching levels
    private void StartNewGame()
    {
        timeLimit = 80;
        reduceLimitBy = 5;
        startTime = DateTime.Now;

        score = 0;
        scoreLabel.text = score.ToString();

        StartNewMaze();
    }

    //Next level = place player at the start, reset goal and reduce the time limit
    private void StartNewMaze()
    {
        generator.GenerateNewMaze(13, 15, OnStartTrigger, OnGoalTrigger);

        float x = generator.startCol * generator.hallWidth;
        float y = 1;
        float z = generator.startRow * generator.hallWidth;
        player.transform.position = new Vector3(x, y, z);

        goalReached = false;
        player.enabled = true;

        // restart timer
        timeLimit -= reduceLimitBy;
        startTime = DateTime.Now;
    }

    //Check if the player is active, update time remanining and go to next level
    void Update()
    {
        if (!player.enabled)
        {
            return;
        }

        int timeUsed = (int)(DateTime.Now - startTime).TotalSeconds;
        int timeLeft = timeLimit - timeUsed;

        if (timeLeft > 0)
        {
            timeLabel.text = timeLeft.ToString();
        }
        else
        {
            timeLabel.text = "TIME UP";
            player.enabled = false;

            Invoke("StartNewGame", 4);
        }
    }

    //Increment score when finding goal
    private void OnGoalTrigger(GameObject trigger, GameObject other)
    {
        Debug.Log("Goal!");
        goalReached = true;

        score += 1;
        scoreLabel.text = score.ToString();

        Destroy(trigger);

        // *Instead of OnStartTrigger*
        // Finish the game when reaching the goal

        Debug.Log("and Finished!");

        // *Is a bit laggy with this*
        //player.enabled = false;

        Invoke("StartNewMaze", 4);
    }

    // Go to next level when going back to the start
    private void OnStartTrigger(GameObject trigger, GameObject other)
    {
        if (goalReached)
        {
            Debug.Log("Finish!");
            player.enabled = false;

            Invoke("StartNewMaze", 4);
        }
    }
}