using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    
    private GameState state = GameState.GameNotStarted;
    public GameState State => state;

    public static event Action<GameState> OnGameStateChanged;
    [SerializeField] UnityEvent OnGameStarted;
    [SerializeField] UnityEvent OnGameFinished;

    public void ChangeState(GameState newState)
    {
        state = newState;
        OnGameStateChanged?.Invoke(state);
        
        switch (state)
        {
            case GameState.GameNotStarted:
                break;
            case GameState.GameStarted:
                OnGameStarted?.Invoke();
                break;
            case GameState.GameFinished:
                OnGameFinished?.Invoke();
                break;
            default:
                break;
        }
    }

    float t = 0.0f;
    void Update()
    {
        t += Time.deltaTime;
        if (t > 10.0f)
        {
            ChangeState(GameState.GameFinished);
        }
    }
}

public enum GameState
{
    GameNotStarted,
    GameStarted,
    GameFinished
}
