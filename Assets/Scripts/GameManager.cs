using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour 
{
	public BirdController m_birdController;
	public CatController[] m_catcontrollers;
	public List<Bird> m_levelFinishedBirds;
	public UIController m_uiController;

	public enum GameState
	{
		Start,
		InGame,
		GameOver
	}

	public GameState m_currentGameState;

	// Use this for initialization
	void Start () 
	{
		
	}

	// Update is called once per frame
	void Update () 
	{
		if (m_currentGameState == GameState.Start) 
		{
			for(int i= 0; i <m_catcontrollers.Length; i++)
			{
				m_catcontrollers [i].UpdateCatController ();
			}
		}
		else if(m_currentGameState == GameState.InGame)
		{
			m_birdController.UpdateBirdController ();

			for(int i= 0; i <m_catcontrollers.Length; i++)
			{
				m_catcontrollers [i].UpdateCatController ();
			}
		}
		else if(m_currentGameState == GameState.GameOver)
		{

		}
	}

	public void BirdReachedTargetPosition(Bird bird)
	{
		m_levelFinishedBirds.Add (bird);

		m_birdController.InitializeNextBird ();
		SoundManager.GetInstance ().PlayBirdFinishDessSfx ();

		if (bird.m_id == 4) // all birds reach target position level up
		{
			m_currentGameState = GameState.GameOver;
			m_uiController.ShowLevelUpText ();  // t
			SoundManager.GetInstance ().PlayGameWinSfx ();
		}
	}

	public void BirdCollidedWithCat()
	{
		m_currentGameState = GameState.GameOver;
		m_uiController.ShowGameOverText ();
		SoundManager.GetInstance ().PlayGameOverSfx ();
	}

	public void StartGame()
	{
		m_currentGameState = GameState.InGame;
		m_birdController.InitializeBird (0);
		//
	}

}
