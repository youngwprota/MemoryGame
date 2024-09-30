using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameState : GameState
{
	public EndGameState(GameManager gameManager) : base(gameManager)
	{
	}

	public override void EnterState()
	{
		base.EnterState();
        gameManager.audioManager.Play("Win");

        TimerController tc = GameObject.FindObjectOfType<TimerController>();
		tc.PauseGame();

		gameManager.uiController.ActivateEndPanel();
	}

	public override void UpdateAction()
	{
		return;
	}
}
