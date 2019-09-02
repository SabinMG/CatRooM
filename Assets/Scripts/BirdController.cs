using UnityEngine;
using System;
using System.Collections;


public class BirdController : MonoBehaviour 
{
	public Bird[] m_birds;

	public Bird m_currentBird;
	public float m_birdSpeed;
	public int m_birdWalkingDirection = 1;

	public RoomManager m_roomManager;
	public GameManager m_gameManager;

	void Awake()
	{
		InputController.OnKeyPress += OnKeyPress;
	}

	void OnDestroy()
	{
		InputController.OnKeyPress -= OnKeyPress;	
	}

	void Start()
	{
		
		//InitializeBird (0); // initializingfirst bird
	}

	public void	InitializeNextBird()
	{
		int nextbirdIndex = m_currentBird.m_id + 1;
		if (nextbirdIndex < m_birds.Length) 
		{
			InitializeBird (nextbirdIndex);
		}
	}

	public void InitializeBird(int birdIndex)
	{
		m_currentBird = m_birds[birdIndex];
		m_currentBird.m_id = birdIndex;
		m_currentBird.m_LastPoint = RooMBirdPoints.F_PointA;
		m_currentBird.m_nextPoint = RooMBirdPoints.F_PointB;
		m_currentBird.SetPosition (m_roomManager.m_birdMovePoints [0].position);
		m_currentBird.m_currentStage = RoomManager.RoomStages.GroundFloor;
		m_currentBird.m_currentBirdState =  (Bird.BirdState.Walking);
		m_currentBird.m_animatorController.SetTrigger ("+Xdirection");

		m_currentBird.m_birdController = this;
	}

	public void UpdateBirdController() 
	{
		if (m_currentBird == null)
			return;
		
		if (m_currentBird.m_currentStage == RoomManager.RoomStages.GroundFloor &&m_currentBird.m_currentBirdState == Bird.BirdState.Walking ) 
		{
			MoveBird (m_currentBird.m_nextPoint);
		}
		
	}
		
	void OnKeyPress(KeyCode key)
	{
		if (key == KeyCode.Space) 
		{
			if (m_gameManager.m_currentGameState == GameManager.GameState.InGame) 
			{
				SoundManager.GetInstance ().PlayBirdTurnSfx ();
				SwitchBirdDirection ();
			}
		}
	}
		
	void MoveBird(RooMBirdPoints nextPoint)
	{
		Vector3 toPosition = m_roomManager.m_birdMovePoints [(int)nextPoint].position;
		float step = m_birdSpeed * Time.deltaTime;
		Vector3 birdNexPositons = Vector3.MoveTowards (m_currentBird.CurrentPosition, toPosition, step);
		m_currentBird.SetPosition (birdNexPositons);

		float m_distanceToNextPoint = Vector3.Distance (m_currentBird.CurrentPosition,toPosition);
		bool reachedNextPoint = MathUtility.IsAproximately (0.0f, m_distanceToNextPoint, 0.2f);
		if (reachedNextPoint) 
		{
			if (m_currentBird.m_nextPoint == RooMBirdPoints.F_PointD) 
			{
				//m_currentBird.m_currentStage = RoomManager.RoomStages.Ladder;

				if (m_currentBird.m_LastPoint == RooMBirdPoints.F_PointA) 
				{
					m_birdWalkingDirection *= -1;	
				}
			}
			SwitchNextPoint ();
		}
	}

	void SwitchBirdDirection()
	{
		RooMBirdPoints lastPoint = m_currentBird.m_nextPoint;
		m_currentBird.m_nextPoint = m_currentBird.m_LastPoint;
		m_currentBird.m_LastPoint = lastPoint;
		m_birdWalkingDirection *= -1;

		Vector3 toPosition = m_roomManager.m_birdMovePoints [(int)m_currentBird.m_nextPoint].position;
		Vector3 fromPosition = m_roomManager.m_birdMovePoints [(int)m_currentBird.m_LastPoint].position;
		SwitchBirdAnimation (toPosition,fromPosition);
	}
		
	void SwitchNextPoint()
	{
		int currentPointIndex = (int)m_currentBird.m_nextPoint;
		int lastPointIndex = (int)m_currentBird.m_LastPoint;

		int NextPointIndex = currentPointIndex + m_birdWalkingDirection;

		if (NextPointIndex == Enum.GetNames (typeof(RooMBirdPoints)).Length) 
		{
			m_currentBird.m_currentBirdState = Bird.BirdState.Finished_Idle;
			m_currentBird.SetPosition( m_roomManager.m_birdFinishPoints[m_currentBird.m_id].position);
			m_currentBird.PlayIdleAnimation ();

			m_gameManager.BirdReachedTargetPosition (m_currentBird);
		
			return;
		}

		if (NextPointIndex < 0)
			NextPointIndex = 3;

		RooMBirdPoints lastPoint = m_currentBird.m_nextPoint;
		m_currentBird.m_nextPoint = (RooMBirdPoints)NextPointIndex;
		m_currentBird.m_LastPoint = lastPoint;

		Vector3 toPosition = m_roomManager.m_birdMovePoints [(int)m_currentBird.m_nextPoint].position;
		Vector3 fromPosition = m_roomManager.m_birdMovePoints [(int)m_currentBird.m_LastPoint].position;
		SwitchBirdAnimation (toPosition,fromPosition);
	}

	void SwitchBirdAnimation(Vector3 nextPos,Vector3 lastPoint )
	{
		Vector3 direction = nextPos - lastPoint;
		if (direction.x > 0 && direction.y > 0) 
		{
			m_currentBird.m_animatorController.SetTrigger ("+Xdirection");
		}
		else if(direction.x < 0 && direction.y < 0) 
		{
			m_currentBird.m_animatorController.SetTrigger ("-Xdirection");
		}
		else if(direction.x > 0 && direction.y < 0) 
		{
			m_currentBird.m_animatorController.SetTrigger ("-Ydirection");
		}
		else if(direction.x < 0 && direction.y > 0) 
		{
			m_currentBird.m_animatorController.SetTrigger ("+Ydirection");
		}
	}

	public void OnCollidedWithCat()
	{
		m_gameManager.BirdCollidedWithCat ();
	}

}
