﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlippingState : CardState
{
	public FlippingState(CardController cardController) : base(cardController)
	{
	}

	public override void EnterState()
	{
		base.EnterState();
		cardController.audioManager.Play("Flip");
	}

	public override void UpdateActivity()
	{
        if (cardController != null)
        {
            cardController.Flip();
        }
        else
        {
            Debug.LogError("cardController is null in FlippingState");
        }
    }
}
