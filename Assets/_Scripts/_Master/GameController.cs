using UnityEngine;
using System.Collections;

[DisallowMultipleComponent]
public class GameController : MonoBehaviour
{
	public enum InputMethod
	{
		TraditionalInput,
		NaturalInput
	}

	private static InputMethod _currentInputMethod = InputMethod.NaturalInput;
	public static InputMethod currentInputMethod
	{
		get
		{
			return _currentInputMethod;
		}
	}

	public delegate void GameOver ();
	public static event GameOver OnGameOver;

	private float _timeScale;
	private bool _isPaused = false;
	public bool isPaused
	{
		get
		{
			return _isPaused;
		}
	}
//	private bool _isStageComplete = false;
//	public bool isStageComplete
//	{
//		get
//		{
//			return _isStageComplete;
//		}
//		set
//		{
//			_isStageComplete = value;
//		}
//	}

	void Awake ()
	{
		Master.SetupComponents ();
	}

	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Escape)) 
		{
			TogglePause ();
		}

		if (Input.GetKeyDown (KeyCode.I)) 
		{
			ToggleInputMethod ();
		}

//        if (Input.GetKeyDown (KeyCode.G))
//        {
//            Debug.Log ("SelectionMethod to gaze");
//            Selection.SetSelectionMethod (Selection.SelectionMethod.Gaze);
//        }

        if (Input.GetKeyDown (KeyCode.M))
        {
//            Debug.Log ("SelectionMethod to mouse");
//            Selection.SetSelectionMethod (Selection.SelectionMethod.Mouse);
			ToggleSelectionMethod ();
        }

		if (Master.playerController.IsDead) 
		{
			SetGameOver ();
		}

		if (Application.loadedLevel == 0) 
		{
			if (Input.GetKeyDown (KeyCode.Return))
			{
				Master.levelLoader.LoadNextLevel ();
			}
		}

//		Debug.Log ("input method: " + _currentInputMethod + "\nselection method: " + Selection.currentSelectionMethod);
	}

	public void SetGameOver ()
	{
//		Debug.Log ("Game Over");
		if (OnGameOver != null) 
		{
			OnGameOver ();
		}
	}

	public void ToggleInputMethod ()
	{
		switch (_currentInputMethod) 
		{
		case InputMethod.NaturalInput: _currentInputMethod = InputMethod.TraditionalInput;
			break;
		case InputMethod.TraditionalInput: _currentInputMethod = InputMethod.NaturalInput;
			break;
		default:
			_currentInputMethod = InputMethod.NaturalInput;
			break;
		}
	}

	public void ToggleSelectionMethod ()
	{
		switch (Selection.currentSelectionMethod) 
		{
		case Selection.SelectionMethod.Gaze: Selection.SetSelectionMethod (Selection.SelectionMethod.Mouse);
			break;
		case Selection.SelectionMethod.Mouse: Selection.SetSelectionMethod (Selection.SelectionMethod.Gaze);
			break;
		default:
			break;
		}
	}

	public void TogglePause ()
	{
		_isPaused = !_isPaused;

		if (_isPaused) 
		{
			PausGame ();
		}
		else
		{
			ResumeGame ();
		}
	}

	public void PausGame ()
	{
		if (!GameState.IsPaused) 
		{
			_timeScale = Time.timeScale;
			Time.timeScale = C.TIME_SCALE_PAUSED;
			GameState.ChangeState (GameState.States.Paused);
		} 
		else 
		{
			return;
		}
	}

	public void ResumeGame ()
	{
		if (!GameState.IsRunning) 
		{
			Time.timeScale = _timeScale;
			GameState.ChangeState (GameState.States.Running);
		} 
		else 
		{
			return;
		}
	}
}