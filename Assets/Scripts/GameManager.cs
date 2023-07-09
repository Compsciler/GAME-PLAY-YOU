using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    private GameState state = GameState.GameNotStarted;
    public GameState State => state;

    public static event Action<GameState> OnGameStateChanged;
    [SerializeField] UnityEvent OnGameStarted;
    [SerializeField] UnityEvent OnGameFinished;

    void ChangeState(GameState newState)
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
}

public enum GameState
{
    GameNotStarted,
    GameStarted,
    GameFinished
}
