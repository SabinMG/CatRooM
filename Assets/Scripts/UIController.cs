using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class UIController : MonoBehaviour 
{

	public GameObject m_gameOverObject;
	public GameObject m_startBtnObject;
	public GameObject m_replayBtnObject;
	public GameObject m_levelUpObject;
	public GameManager m_gameManager;

	private bool m_canReplay;
	void Awake()
	{
		InputController.OnKeyPress += OnKeyPress;
	}

	void OnDestroy()
	{
		InputController.OnKeyPress -= OnKeyPress;	
	}

	// Update is called once per frame
	void Update () 
	{
	
	}

	void OnKeyPress(KeyCode key)
	{
		if (key == KeyCode.Space) 
		{
			if (m_gameManager.m_currentGameState == GameManager.GameState.Start) 
			{
				StartGame ();
			}

			if (m_gameManager.m_currentGameState == GameManager.GameState.GameOver) 
			{
				if (m_canReplay)
					ReplayGame ();
			}

		}
	}
		
	public void ShowGameOverText()
	{
		m_canReplay = false;
		m_gameOverObject.SetActive (true);
		Invoke ("ShowReplayBtn",1.5f);
	}

	public void ShowLevelUpText()
	{
		m_canReplay = false;
		m_levelUpObject.SetActive (true);
		Invoke ("ShowReplayBtn",1.5f);
	}
		
	public void ShowReplayBtn()
	{
		m_canReplay = true;
		m_replayBtnObject.SetActive (true);
	}

	public void StartGame()
	{
		SoundManager.GetInstance ().PlayBtnPressSfx ();
		m_gameManager.StartGame ();
		m_startBtnObject.SetActive (false);
	}
		
	public void ReplayGame()
	{
		SceneManager.LoadScene ("GameScene");
		SoundManager.GetInstance ().PlayBtnPressSfx ();
	}
}
