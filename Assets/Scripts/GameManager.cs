using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    GameState gameState;

    public UIController uiController;
    public AudioManager audioManager;

    public CardSelectionState cardSelectionState;
    public PairSelectionState pairSelectionState;
    public MemorizeCardsState memorizeCardsState;
    public MatchingCardsState matchingCardsState;
    public EndGameState endGameState;
    public PauseGameState pauseGameState;

    public GameObject[] selectedCards;

    int cardCount;
    int movesCount;
    private bool canSelect = true;  

    public int CardCount
    {
        set
        {
            cardCount = value;
        }
        get
        {
            return cardCount;
        }
    }

    void Start()
    {
        movesCount = 0;
        selectedCards = new GameObject[2];
        selectedCards[0] = null;
        selectedCards[1] = null;

        InitStates();
    }

    void Update()
    {
        gameState.UpdateAction();

        if (cardCount <= 0)
        {
            TransitionState(endGameState);
        }
    }

    void InitStates()
    {
        cardSelectionState = new CardSelectionState(this);
        pairSelectionState = new PairSelectionState(this);
        memorizeCardsState = new MemorizeCardsState(this, 0.1f);
        matchingCardsState = new MatchingCardsState(this, 0.1f);
        pauseGameState = new PauseGameState(this);
        endGameState = new EndGameState(this);

        gameState = cardSelectionState;
    }

    public void TransitionState(GameState newState)
    {
        gameState.EndState();
        gameState = newState;
        gameState.EnterState();
    }

    public void SetSelectedCard(GameObject selectedCard)
    {
        movesCount++;
        uiController.ChangeMovesCount(movesCount);

        if (!canSelect) return; 

        if (selectedCards[0] == null)
        {
            selectedCards[0] = selectedCard;
            TransitionState(pairSelectionState);
        }
        else if (selectedCards[1] == null)
        {
            selectedCards[1] = selectedCard;

            if (MatchSelectedCards())
            {
                TransitionState(matchingCardsState);
            }
            else
            {
                StartCoroutine(WaitAndMemorizeCards()); 
            }
        }
    }

    private IEnumerator WaitAndMemorizeCards()
    {
        canSelect = false;  
        yield return new WaitForSeconds(0.5f);  

        TransitionState(memorizeCardsState);
        canSelect = true; 
    }

    public void RemoveSelectedCards()
    {
        selectedCards[0] = null;
        selectedCards[1] = null;
    }

    bool MatchSelectedCards()
    {
        CardSO first = selectedCards[0].GetComponent<CardController>().cardType;
        CardSO second = selectedCards[1].GetComponent<CardController>().cardType;

        return first != null && second != null && first.cardName == second.pairName && first.pairName == second.cardName;
    }

    public bool CanSelectCards
    {
        get { return canSelect; }
    }

}
