using UnityEngine;
using System.Collections;

public enum RooMBirdPoints
{
	F_PointA, // first floor
	F_PointB,
	F_PointC,
	F_PointD,
	L_PointE, // ladder
	L_PointF, 
	S_PointG,  // second floor
	S_PointI
};



public class RoomManager : MonoBehaviour 
{
	public enum RoomStages
	{
		GroundFloor,
		Ladder,
		FirstFloor
	};
		
	public Transform[] m_firstFloorPoints;
	public Transform[] m_birdMovePoints;
	public Transform[] m_birdFinishPoints;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		DrawGroundFloorPath ();
	}
		
	void DrawGroundFloorPath()
	{
		for (var i=1; i<m_birdMovePoints.Length; i++)
		{
			Debug.DrawLine(m_birdMovePoints[i-1].position, m_birdMovePoints[i].position, Color.green);
		}

		//Debug.DrawLine(m_birdMovePoints[0].position, m_firstFloorPoints[m_firstFloorPoints.Length-1].position, Color.green);
	}
}
