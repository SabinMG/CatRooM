using UnityEngine;
using System.Collections;

public class CatController : MonoBehaviour 
{
	public Transform[] m_catMovePoints;
	public float m_moveSpeed;
	public int m_catRestIndex;
	public int m_currentCatPintIndex;

	private Transform m_thisTransfom;
	private int m_lastCatPintIndex;
	private Animator m_animatorController;
	private int m_moveDirection = 1;

	void Awake()
	{
		m_thisTransfom = this.transform;
		m_animatorController = gameObject.GetComponent<Animator> ();
	}

	public void UpdateCatController() 
	{
		float step = m_moveSpeed * Time.deltaTime;
		float m_distanceToNextPoint = Vector3.Distance (m_thisTransfom.position,m_catMovePoints[m_currentCatPintIndex].position);
		bool reachedNextPoint = MathUtility.IsAproximately (0.0f, m_distanceToNextPoint, 0.2f);
		if (reachedNextPoint) 
		{
			int randomDirection =  (int)Mathf.Sign(Random.Range (-1.0f,1.0f));
			int nextIndex = m_currentCatPintIndex - m_moveDirection*randomDirection;
			m_lastCatPintIndex = m_currentCatPintIndex;
			m_currentCatPintIndex = nextIndex;

			if (nextIndex <0) 
			{
				m_currentCatPintIndex = 1;
			}

			if(nextIndex>m_catMovePoints.Length-1)
			{
				//m_lastCatPintIndex = m_currentCatPintIndex;
				m_currentCatPintIndex = m_catMovePoints.Length-2;
			}

			Vector3 direction = m_catMovePoints [m_currentCatPintIndex].position - m_catMovePoints [m_lastCatPintIndex].position;

			if (direction.x > 0 && direction.y > 0) 
			{
				m_animatorController.SetTrigger ("+Xdirection");
			}
			else if(direction.x < 0 && direction.y < 0) 
			{
				m_animatorController.SetTrigger ("-Xdirection");
			}
			else if(direction.x > 0 && direction.y < 0) 
			{
				m_animatorController.SetTrigger ("-Ydirection");
			}
			else if(direction.x < 0 && direction.y > 0) 
			{
				m_animatorController.SetTrigger ("+Ydirection");
			}
		}
	
		Vector3 targetPos = m_catMovePoints [m_currentCatPintIndex].position;
		transform.position = Vector3.MoveTowards(m_thisTransfom.position, targetPos, step);
	}
}
