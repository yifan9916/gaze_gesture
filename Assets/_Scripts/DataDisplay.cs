using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Leap;

public class DataDisplay : MonoBehaviour
{
	private const string NO_HAND = "No hand.";

	public Text leftHandDataDisplay;
	public Text rightHandDataDisplay;

	private string _leftHandData;
	private string _rightHandData;

	void Update ()
	{		
		ResetHandDataText ();
		SetHandDataText ();
	}

	string GetHandData (Hand hand)
	{
		if (hand != null)
		{
			string data = "Hand ID: " + hand.Id 
					+ "\nHand pos:\n" + hand.PalmPosition
					+ "\nGrab strength:\n" + hand.GrabStrength;
			return data;
		}
		else
		{
			return NO_HAND;
		}
	}

	void SetHandDataText ()
	{
		_leftHandData = GetHandData (Master.gestureController.leftHand);
		leftHandDataDisplay.text = _leftHandData;

		_rightHandData = GetHandData (Master.gestureController.rightHand);
		rightHandDataDisplay.text = _rightHandData;	
	}

	void ResetHandDataText ()
	{
		_leftHandData = NO_HAND;
		_rightHandData = NO_HAND;
	}
}