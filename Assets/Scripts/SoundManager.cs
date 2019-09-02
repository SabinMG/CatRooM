using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour 
{

	public AudioSource m_audioSource;
	public AudioClip m_birdTurnClip;
	public AudioClip m_gameOverClip;
	public AudioClip m_btnPressClip;
	public AudioClip m_birdFinishClip;
	public AudioClip m_gameWinClip;

	static SoundManager ms_soundManager;

	public static SoundManager GetInstance()
	{
		return ms_soundManager;
	}

	public void PlayBirdTurnSfx()
	{
		m_audioSource.PlayOneShot (m_birdTurnClip,1.0f);
	}

	public void PlayGameOverSfx()
	{
		m_audioSource.PlayOneShot (m_gameOverClip,1.0f);
	}

	public void PlayBtnPressSfx()
	{
		m_audioSource.PlayOneShot (m_btnPressClip,1.0f);
	}

	public void PlayBirdFinishDessSfx()
	{
		m_audioSource.PlayOneShot (m_birdFinishClip,1.0f);
	}

	public void PlayGameWinSfx()
	{
		m_audioSource.PlayOneShot (m_gameWinClip,1.0f);
	}


	void Awake()
	{
		ms_soundManager = this;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
