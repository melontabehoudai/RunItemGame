using UnityEngine;
using Unity.Cinemachine;

//自作の列挙型
public enum GameState
{
    None,
    IsPlay,
    GameClear,
    GameOver,
    GameEnd
}

public class GameManager : MonoBehaviour
{
    //自作した型を扱うStatic変数
    public static GameState gameState;

    private GameObject mixingCamera;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameState = GameState.IsPlay;

        //mixingcameraを探索
        mixingCamera = GameObject.FindGameObjectWithTag("mixingCamera");
    }

    // Update is called once per frame
    void Update()
    {
        if (gameState == GameState.GameOver)
        {
            Debug.Log("Gameover");
            //UIControllerを探し出してゲームオーバー用のUI表示命令
            UIController contoller = GameObject.FindAnyObjectByType<UIController>();
            contoller.GameOverDisplay();

            gameState = GameState.GameEnd;
            Debug.Log("GameEnd");
        }
    }
}
