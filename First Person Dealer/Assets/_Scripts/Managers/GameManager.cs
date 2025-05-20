using UnityEngine;
using System;

public enum GameState
{
    InGame,         // 游戏进行中
    Paused,         // 游戏暂停（菜单）
    ViewingDeck,    // 查看牌组
    Shopping        // 商店购物
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameState CurrentGameState { get; private set; }
    public event Action<GameState> OnGameStateChanged;
    private bool cursorLocked = true;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            CurrentGameState = GameState.InGame; // 默认状态
            SetLockCursor(true); // 默认锁定光标
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ChangeGameState(GameState newState)
    {
        if (CurrentGameState == newState) return;

        CurrentGameState = newState;
        OnGameStateChanged?.Invoke(newState);
        
        HandleStateChange(newState);
    }

    private void HandleStateChange(GameState newState)
    {
        switch (newState)
        {
            case GameState.InGame:
                Time.timeScale = 1f;
                SetLockCursor(true);
                break;
            
            case GameState.Paused:
            case GameState.ViewingDeck:
            case GameState.Shopping:
                Time.timeScale = 0f;
                SetLockCursor(false);
                break;
        }
    }

    public void SetLockCursor(bool shouldLock)
    {
        cursorLocked = shouldLock;
        Cursor.visible = !shouldLock;
        Cursor.lockState = shouldLock ? CursorLockMode.Locked : CursorLockMode.None;
    }

    // 便捷方法用于状态切换
    public void PauseGame() => ChangeGameState(GameState.Paused);
    public void ResumeGame() => ChangeGameState(GameState.InGame);
    public void OpenDeckView() => ChangeGameState(GameState.ViewingDeck);
    public void OpenShop() => ChangeGameState(GameState.Shopping);

    #if UNITY_EDITOR
    private void OnGUI()
    {
        // 在编辑器中显示当前游戏状态（调试用）
        GUI.Label(new Rect(10, 10, 200, 20), $"Game State: {CurrentGameState}");
    }
    #endif
} 