using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class InputController : MonoBehaviour 
{

	public delegate void keyPress(KeyCode key);
	public static keyPress OnKeyPress ;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown (KeyCode.Space)) 
		{
			OnKeyPress (KeyCode.Space);
		}
	}
}
