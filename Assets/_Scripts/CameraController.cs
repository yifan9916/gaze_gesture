using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
	public enum CameraMode
	{
		Following,
		Roam,
		LockOn
	}

	[SerializeField]
	private CameraMode _currentMode = CameraMode.Roam;
	public CameraMode currentMode 
	{
		get
		{
			return _currentMode;
		}
	}

	public float smoothing = 1.0f;
	public int screenBounds = 50;
	public float scrollSpeed = 5;

	private Transform _player;
	private Vector3 _targetCameraPosition;
	private Vector3 _offset;

	private int _screenWidth;
	private int _screenHeight;

	void Awake ()
	{
		_player = GameObject.FindGameObjectWithTag (C.TAG_PLAYER).transform;
        
		_offset = transform.position - _player.transform.position;

		_screenWidth = Screen.width;
		_screenHeight = Screen.height;
	}

	void Start ()
	{
		EventManager.OnPlayerLockOn += SetCameraFollow;
		EventManager.OnRoamCommand += SetCameraRoam;
	}

	void LateUpdate ()
	{
		MoveCamera ();
	}
	
	void MoveCamera ()
	{
		switch (_currentMode)
		{
			case CameraMode.Following:
				SetCameraFollowPosition ();
				break;
			case CameraMode.LockOn:
				SetCameraFollowPosition ();
				break;
			case CameraMode.Roam:
				SetCameraRoamPosition ();
				break;
			default:
				SetCameraRoamPosition ();
				break;
		}

		//TODO: follow not working in traditional input
		if (GameController.currentInputMethod == GameController.InputMethod.NaturalInput) 
		{
			transform.position = Vector3.Lerp (transform.position, _targetCameraPosition, smoothing * Time.deltaTime);
		}
	}

	public void SetCameraFollow ()
	{
		ChangeCameraMode (CameraMode.Following);
	}

//	public void SetCameraLockOn ()
//	{
//		ChangeCameraMode (CameraMode.LockOn);
//	}

	public void SetCameraRoam ()
	{
		ChangeCameraMode (CameraMode.Roam);
	}

	private void ChangeCameraMode (CameraMode newMode)
	{
		if (_currentMode == newMode)
		{
			return;
		}
		else
		{
			_currentMode = newMode;
		}
	}

//	public bool InFollowMode
//	{
//		get
//		{
//			return _currentMode == CameraMode.Following;
//		}
//	}
//
//	public bool InRoamMode
//	{
//		get
//		{
//			return _currentMode == CameraMode.Roam;
//		}
//	}
//
//	public bool IsLocked
//	{
//		get
//		{
//			return _currentMode == CameraMode.LockOn;
//		}
//	}

	void SetCameraFollowPosition ()
	{
		//TODO: set camera between player and waypoint

		switch (GameController.currentInputMethod) 
		{
		case GameController.InputMethod.TraditionalInput:
			transform.position = _player.position + _offset;
			break;
		case GameController.InputMethod.NaturalInput:
			_targetCameraPosition = _player.position + _offset;
			break;
		default:
			transform.position = _player.position + _offset;
			break;
		}
	}

	void SetCameraRoamPosition ()
	{
		switch (GameController.currentInputMethod) 
		{
		case GameController.InputMethod.TraditionalInput:
			TraditionalInputCameraPositioning ();
			break;
		case GameController.InputMethod.NaturalInput:
			NaturalInputCameraPositioning ();
			break;
		default:
			break;
		}
	}

	void NaturalInputCameraPositioning ()
	{
		GameObject camFocusPoint = GameObject.FindGameObjectWithTag (C.TAG_TARGET_POINT);
		Vector3 camFocusPos = camFocusPoint.transform.position;
		_targetCameraPosition = camFocusPos + _offset;
	}

	void TraditionalInputCameraPositioning ()
	{
		if (Input.mousePosition.x > _screenWidth - screenBounds) 
		{
			transform.Translate (Vector3.right * scrollSpeed * Time.deltaTime);
		}
		
		if (Input.mousePosition.x < 0 + screenBounds) 
		{
			transform.Translate (Vector3.left * scrollSpeed * Time.deltaTime);
		}
		
		if (Input.mousePosition.y > _screenHeight - screenBounds) 
		{
			transform.Translate (Vector3.up * scrollSpeed * Time.deltaTime);
		}
		
		if (Input.mousePosition.y < 0 + screenBounds) 
		{
			transform.Translate (Vector3.down * scrollSpeed * Time.deltaTime);
		}
	}
}