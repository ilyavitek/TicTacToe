using System;
using System.Collections;
using Bot;
using Components;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TicTacToe : MonoBehaviour
{
    [Header("Bot")]
    [SerializeField] private bool AgainstBot;
    [SerializeField] private bool BotPlaysFirst;
    [SerializeField] private BotType BotType;

    [Header("Time")]
    [SerializeField] private float SecondsForBotToMove = 1.5f;

    [SerializeField] private float SecondsToWaitBeforeRestart = 3f;

    [Header("FirstMark")]
    [SerializeField] private PlayerMark FirstPlayer;

    [Header("References")]
    [SerializeField] private BoardComponent BoardComponent;
    [SerializeField] private TextMeshProUGUI GameStatusText;
    [SerializeField] private GameObject InputBlocker;

    private PlayerMark _currentPlayerMark;
    private bool _botTurn;

    private IBot _bot;
    private WinChecker _winChecker;

    private void Start()
    {
        InputBlocker.SetActive(false);

        _botTurn = AgainstBot && BotPlaysFirst;
        _currentPlayerMark = FirstPlayer;
        _winChecker = new WinChecker(BoardComponent);
        
        if (AgainstBot)
            PickBot();

        Print($"{_currentPlayerMark} Turn");

        TileComponent.TileClickedEvent += TileClicked;

        if (_botTurn)
            StartCoroutine(WaitAndBotTurn());
    }

    private void OnDestroy()
    {
        TileComponent.TileClickedEvent -= TileClicked;
    }
    
    private void PickBot()
    {
        switch (BotType)
        {
            case BotType.Random:
                _bot = new RandomBot();
                break;
            case BotType.WinMoveSeeker:
                _bot = new WinMoveSeekerBot(BoardComponent, _currentPlayerMark, _winChecker);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void TileClicked(int x, int y)
    {
        BoardComponent.MarkTile(x, y, _currentPlayerMark);

        var currentPlayerWon = _winChecker.DidPlayerWon(x, y, _currentPlayerMark);
        var isDraw = _winChecker.IsDraw();
        var isGameOver = currentPlayerWon || isDraw;

        if (isGameOver)
        {
            if (currentPlayerWon)
            {
                Print($"{_currentPlayerMark} Won!");
                StartCoroutine(WaitAndRestart());
            } else
            {
                Print("Draw!");
                StartCoroutine(WaitAndRestart());
            }
        } else
        {
            SwitchTurn();
        }
    }

    private IEnumerator WaitAndRestart()
    {
        yield return new WaitForSeconds(SecondsToWaitBeforeRestart);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void SwitchTurn()
    {
        _currentPlayerMark = _currentPlayerMark.SwitchTurn();

        Print($"{_currentPlayerMark} Turn");

        BotTurnCheck();
    }

    private void BotTurnCheck()
    {
        if (!AgainstBot)
            return;

        _botTurn = !_botTurn;

        if (_botTurn)
            StartCoroutine(WaitAndBotTurn());
    }

    private IEnumerator WaitAndBotTurn()
    {
        InputBlocker.SetActive(true);

        yield return new WaitForSeconds(SecondsForBotToMove);

        _bot.PlayTurn();

        InputBlocker.SetActive(false);
    }

    private void Print(string message)
    {
        GameStatusText.text = message;
        Debug.Log(message);
    }
}