using UnityEngine;
using System.Collections;

public class Bird : MonoBehaviour 
{
	public int m_id;
	public RooMBirdPoints m_LastPoint;
	public RooMBirdPoints m_nextPoint;
	public float m_birdWalkingDir = 1.0f; // it will be -1.0f for opposit direction
	public Transform m_thisTransform;
	public RoomManager.RoomStages m_currentStage;
	public Animator m_animatorController;

	public BirdController m_birdController;
	public enum BirdState
	{
		Idle,
		Walking,
		Finished_Idle
	}

	public BirdState m_currentBirdState;

	public Vector3 CurrentPosition
	{
		get{ return m_thisTransform.position; }
	}

	public void SetPosition(Vector3 pos)
	{
		m_thisTransform.position = pos;
	}


	void Awake()
	{
		m_thisTransform = transform;
		m_animatorController = gameObject.GetComponent<Animator> ();
	}
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	void OnTriggerEnter2D(Collider2D other) 
	{
		if (other.tag == TagManager.Tags.Cat.ToString()) 
		{
			m_birdController.OnCollidedWithCat ();
		}
	}


	public void PlayIdleAnimation()
	{
		m_animatorController.SetTrigger ("Idle");
	}
}
