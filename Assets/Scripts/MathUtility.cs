using UnityEngine;
using System.Collections;

public class MathUtility : MonoBehaviour 
{

	public static bool IsAproximately (float a,float b, float tolerance )
	{
		return Mathf.Abs (a - b) < tolerance;

	}
}
