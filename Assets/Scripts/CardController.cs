using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class CardController : MonoBehaviour, IPointerDownHandler
{
    public Image frontFace;
    public Image backFace;
    public CardSO cardType;

    GameManager gameManager;
    public AudioManager audioManager;

    public CardState actualState;
    public FrontState frontState;
    public BackState backState;
    public FlippingState flippingState;
    public BackFlippingState backFlippingState;
    public MemorizeState memorizeState;
    public HideAwayState hideAwayState;

    float cardScale = 1.0f;
    float flipSpeed = 10.0f;
    float flipTolerance = 0.05f;

    void Start()
    {
        gameManager = (GameManager)FindObjectOfType(typeof(GameManager));
        audioManager = gameManager.audioManager;

        frontState = new FrontState(this);
        backState = new BackState(this);
        flippingState = new FlippingState(this);
        backFlippingState = new BackFlippingState(this);
        hideAwayState = new HideAwayState(this);

        actualState = backState;
    }

    void Update()
    {
        actualState.UpdateActivity();
    }

    internal void SetCardDatas(Sprite background, CardSO card)
    {
        this.cardType = card;

        frontFace.sprite = card.cardImage;
        backFace.sprite = background;

        backFace.gameObject.SetActive(true);
        frontFace.gameObject.SetActive(false);
    }

    public void TransitionState(CardState newState)
    {
        this.actualState.EndState();
        this.actualState = newState;
        this.actualState.EnterState();
    }

    public void SwitchFaces()
    {
        backFace.gameObject.SetActive(!backFace.gameObject.activeSelf);
        frontFace.gameObject.SetActive(!frontFace.gameObject.activeSelf);
    }

    public void InactivateCard()
    {
        backFace.gameObject.SetActive(false);
        frontFace.gameObject.SetActive(false);

        Image cardImage = this.GetComponent<Image>();
        Color newColor = cardImage.color;
        newColor.a = 0.0f;
        cardImage.color = newColor;
    }

    public void ChangeScale(float newScale)
    {
        this.transform.localScale = new Vector3(newScale, 1, 1);
    }

    public void Flip()
    {
        if (!gameManager.CanSelectCards) return;

        if (backFace.gameObject.activeSelf == true)
        {
            cardScale -= flipSpeed * Time.deltaTime;
            ChangeScale(cardScale);

            if (flipTolerance > cardScale)
            {
                SwitchFaces();
            }
        }
        else
        {
            cardScale += flipSpeed * Time.deltaTime;
            ChangeScale(cardScale);

            if (cardScale >= 1.0f)
            {
                ChangeScale(1.0f);
                TransitionState(this.frontState);
                gameManager.SetSelectedCard(this.gameObject);  
            }
        }
    }

    public void BackFlip()
    {
        if (backFace.gameObject.activeSelf == false)
        {
            cardScale -= flipSpeed * Time.deltaTime;
            ChangeScale(cardScale);

            if (flipTolerance > cardScale)
            {
                SwitchFaces();
            }
        }
        else
        {
            cardScale += flipSpeed * Time.deltaTime;
            ChangeScale(cardScale);

            if (cardScale >= 1.0f)
            {
                ChangeScale(1.0f);
                TransitionState(this.backState);
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (gameManager.CanSelectCards)
        {
            actualState.OnClickAction();
        }
    }
}