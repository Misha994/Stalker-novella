using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Naninovel;
using TMPro;

public class MemoryGameController : MonoBehaviour
{
    public static MemoryGameController Instance { get; private set; }

    [SerializeField] private GameObject game;
    [SerializeField] private Transform gridContainer;
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private List<Sprite> cardImages;
    [SerializeField] private Sprite cardBack;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private int pairsToMatch = 6;

    private MemoryCard firstCard, secondCard;
    private int matchedPairs = 0;
    private float gameTime = 60f;
    private bool isGameRunning = false;

    private void Awake()
    {
        Instance = this;
    }

    public void InitializeGame()
    {
        // Очистити попередні карти, якщо вони є
        foreach (Transform child in gridContainer)
        {
            Destroy(child.gameObject);
        }

        List<int> cardIds = new List<int>();
        for (int i = 0; i < pairsToMatch; i++)
        {
            cardIds.Add(i);
            cardIds.Add(i);
        }

        cardIds.Shuffle();

        foreach (var id in cardIds)
        {
            var cardObject = Instantiate(cardPrefab, gridContainer);
            var card = cardObject.GetComponent<MemoryCard>();
            card.Initialize(id, cardImages[id], cardBack);
        }

        StartCoroutine(FixCardPositions());
    }

    public void StartGame()
    {
        game.SetActive(true);

        matchedPairs = 0;
        gameTime = 60f;
        isGameRunning = true;

        InitializeGame();

        StartCoroutine(StartTimer());
    }

    public void StartGameWithCallback(System.Action<bool> onGameEndCallback)
    {
        StartGame();
        StartCoroutine(WaitForGameEnd(onGameEndCallback));
    }

    private IEnumerator WaitForGameEnd(System.Action<bool> onGameEndCallback)
    {
        while (isGameRunning)
            yield return null;

        onGameEndCallback?.Invoke(matchedPairs == pairsToMatch);
    }

    private IEnumerator FixCardPositions()
    {
        yield return null;

        foreach (Transform child in gridContainer)
        {
            var cardPosition = child.position;
            child.position = cardPosition;
        }

        var gridLayout = gridContainer.GetComponent<GridLayoutGroup>();
        if (gridLayout != null)
        {
            gridLayout.enabled = false;
        }
    }

    public void OnCardFlipped(MemoryCard card)
    {
        if (firstCard == null)
        {
            firstCard = card;
            firstCard.Flip();
        }
        else if (secondCard == null)
        {
            secondCard = card;
            secondCard.Flip();
            StartCoroutine(CheckMatch());
        }
    }

    private IEnumerator CheckMatch()
    {
        yield return new WaitForSeconds(0.5f);

        if (firstCard.Id == secondCard.Id)
        {
            Destroy(firstCard.gameObject);
            Destroy(secondCard.gameObject);
            matchedPairs++;

            if (matchedPairs == pairsToMatch)
                EndGame(true);
        }
        else
        {
            firstCard.Flip();
            secondCard.Flip();
        }

        firstCard = null;
        secondCard = null;
    }

    private IEnumerator StartTimer()
    {
        while (gameTime > 0 && matchedPairs < pairsToMatch)
        {
            gameTime -= Time.deltaTime;
            timerText.text = $"Time: {gameTime:F1}s";
            yield return null;
        }

        isGameRunning = false;
        if (matchedPairs < pairsToMatch)
            EndGame(false);
    }

    private void EndGame(bool won)
    {
        Debug.Log(won ? "You did it!" : "You failed!");

        // Повертаємо гру до початкового стану
        ResetGame();

        var miniGameService = Engine.GetService<MiniGameService>();
        miniGameService?.SetGameResult(won);

        game.SetActive(false);
    }

    private void ResetGame()
    {
        // Очищення сітки
        foreach (Transform child in gridContainer)
        {
            Destroy(child.gameObject);
        }

        // Скидання змінних стану
        firstCard = null;
        secondCard = null;
        matchedPairs = 0;
        gameTime = 60f;
        isGameRunning = false;

        // Очищення тексту таймера
        if (timerText != null)
        {
            timerText.text = "Time: 60.0s";
        }
    }
}
