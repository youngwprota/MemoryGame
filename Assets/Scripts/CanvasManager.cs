using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public GameObject cardPrefab;
    public CardCollectionSO collection;

    public GameDataSO easyData;
    public GameDataSO normalData;
    public GameDataSO hardData;

    GameDataSO gameDatas;
    List<CardController> cardControllers;

    public void Awake()
    {
        cardControllers = new List<CardController>();

        GetGameDataDifficulty();
        SetCardGridLayoutParams();
        GenerateCards();
    }

    private void GetGameDataDifficulty()
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

    private void SetCardGridLayoutParams()
    {
        CardGridLayout cardGridLayout = this.GetComponent<CardGridLayout>();

        cardGridLayout.preferredTopPadding = gameDatas.preferredTopBottomPadding;
        cardGridLayout.rows = gameDatas.rows;
        cardGridLayout.columns = gameDatas.columns;
        cardGridLayout.spacing = gameDatas.spacing;
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
    }
}
