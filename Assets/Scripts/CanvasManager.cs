using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    public GameObject cardPrefab;
    public CardCollectionSO cardCollection;

    public GameDatasSO easyData;
    public GameDatasSO normalData;
    public GameDatasSO hardData;

    GameDatasSO gameDatas;
    CardGridGenerator cardGridGenerator;

    List<CardController> cardControllers;

    void Awake()
    {
        cardControllers = new List<CardController>();
        GetGameDatasByDifficulty();

        cardGridGenerator = new CardGridGenerator(cardCollection, gameDatas);

        SetCardGridLayoutParams();
        GenerateCards();

        Debug.Log("BAAAAAACK^" + gameDatas.background);

        GameManager gameManager = (GameManager)FindObjectOfType(typeof(GameManager));
        gameManager.CardCount = gameDatas.rows * gameDatas.columns;
    }

    private void SetCardGridLayoutParams()
    {
        CardGridLayout cardGridLayout = this.GetComponent<CardGridLayout>();

        cardGridLayout.preferredTopPadding = gameDatas.preferredTopBottomPadding;
        cardGridLayout.rows = gameDatas.rows;
        cardGridLayout.columns = gameDatas.columns;
        cardGridLayout.spacing = gameDatas.spacing;

        cardGridLayout.Invoke("CalculateLayoutInputHorizontal", 0.1f);
    }

    private void GetGameDatasByDifficulty()
    {
        Difficulty difficulty = (Difficulty)PlayerPrefs.GetInt("Difficulty", (int)Difficulty.NORMAL);

        switch (difficulty)
        {
            case Difficulty.EASY:
                gameDatas = easyData;
                break;
            case Difficulty.NORMAL:
                gameDatas = normalData;
                break;
            case Difficulty.HARD:
                gameDatas = hardData;
                break;
        }
    }

    private void GenerateCards()
    {
        int cardCount = gameDatas.rows * gameDatas.columns;

        for (int i = 0; i < cardCount; i++)
        {
            GameObject card = Instantiate(cardPrefab, this.transform);
            card.transform.name = "Card (" + i.ToString() + ")";

            cardControllers.Add(card.GetComponent<CardController>());
        }

        for (int i = 0; i < cardCount / 2; i++)
        {
            CardSO randomCard = cardGridGenerator.GetRandomAvailableCardSO();
            if (randomCard == null)
            { Debug.LogError("randomCard is NULL"); 
            }
            SetRandomCardToGrid(randomCard);

            CardSO randomCardPair = cardGridGenerator.GetCardPairSO(randomCard.cardName);
            if (randomCardPair == null) { Debug.LogError("randomCardPair is NULL"); }
            SetRandomCardToGrid(randomCardPair);
        }
    }

    private void SetRandomCardToGrid(CardSO randomCard)
    {
        int index = cardGridGenerator.GetRandomCardPositionIndex();
        CardController cardObject = cardControllers[index];
        Debug.Log($"Card Controllers Count: {cardControllers.Count}");
        if (index < cardControllers.Count)
        {
            Debug.Log($"Card Object at Index {index}: {cardControllers[index]}");
        }
        else
        {
            Debug.LogWarning($"Index {index} is out of range for Card Controllers.");
        }
        if (randomCard == null)
        {
            Debug.LogError("Random Card is null!");
        }
        if (gameDatas.background == null)
        {
            Debug.LogError("Background sprite is null!");
        }
        cardObject.SetCardDatas(gameDatas.background, randomCard);
    }
}
