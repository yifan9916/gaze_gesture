using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class LevelTransition : MonoBehaviour
{
	private const string COMPLETION_TIME = "\nStage completion time: ";
	private const string TOTAL_TIME = "Total play time: ";
	public GameObject[] _targetsList;
	private GameObject _canvas;
	public Text infoText;

	private bool _completionRequirementsMet = false;
	private float _completionTime;
	private float _totalTime;

	void Awake ()
	{
		_canvas = GameObject.FindGameObjectWithTag (C.TAG_END_OF_STAGE);
	}

	void Start ()
	{
		_canvas.SetActive (false);
	}

	void OnTriggerEnter (Collider other)
	{
//		_enemiesList = GameObject.FindGameObjectsWithTag (C.TAG_ENEMY);
//		float completionTime = Time.time;
//		if (other.CompareTag (C.TAG_PLAYER)) 
//		{
//			if (!(_enemiesList.Length > 0))
//			{
//				Master.eventManager.CommandRoam ();
//				_canvas.SetActive (true);
//				buttonText.text = BUTTON_TEXT + COMPLETION_TIME + completionTime;
//			}
//			else
//			{
//				foreach (GameObject go in _enemiesList)
//				{
//					Debug.Log ("Name: " + go.name);
//				}
//
//				Debug.Log ("Kill all stuffed animals. " + _enemiesList.Length + " enemies remain.");
//			}
//		}
		if (other.CompareTag (C.TAG_PLAYER)) 
		{
			CheckStageCompletionRequirements ();
		}
	}

	void CheckStageCompletionRequirements ()
	{
//		if (_targetsList.Length == 0) 
//		{
//			_completionRequirementsMet = true;
//		}

		if (!_completionRequirementsMet) 
		{
			for (int i = 0; i < _targetsList.Length; i++) 
			{
				if (_targetsList[i].activeInHierarchy) 
				{
					return;
				}
			}

			_completionRequirementsMet = true;
			_totalTime = Time.time;
			_completionTime = Time.timeSinceLevelLoad;
			infoText.text = TOTAL_TIME + _totalTime + COMPLETION_TIME + _completionTime;
			_canvas.SetActive (true);
		}
	}
}