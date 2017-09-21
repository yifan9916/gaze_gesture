using UnityEngine;
using System.Collections;

//light turns off after locking on
//need to be able to lock on manually during targeting mode
//targeting mode > able to manually lock on lock off > targeting mode toggle
//lock on yay or nay

public class TargetPoint : MonoBehaviour 
{
	private const float MAX_LIGHT_INTENSITY = 4.0f;
	private const float MIN_LIGHT_INTENSITY = 0.0f;
	
	[SerializeField]
	private float _followSpeed = 7.0f;
	private float _fadeSpeed = 0.05f;
	
	private Light _targetPoint;
	private Color _powerColor;
	private GameObject _target;
	private bool _isLockedOn = false;
	private float _lockOnTime = 2.0f;
	private bool _isTargeting = false;
	
	void Awake ()
	{
		_targetPoint = GetComponentInChildren<Light> ();
	}
	
	void Update ()
	{
		if (!_isLockedOn) 
		{
			FollowCursor ();
		}

		if (Master.eventManager.IsCharacterTargeting || Master.eventManager.IsCharacterLockedOn) 
		{
			_targetPoint.intensity = MAX_LIGHT_INTENSITY;
		}
		else
		{
			_targetPoint.intensity = MIN_LIGHT_INTENSITY;
		}

//		LockOn ();
	}
	
	void OnEnable ()
	{
//		EventManager.OnTargetLockOn += ToggleWaypoint;
//		EventManager.OnTargetLockOn	+= LockOn;
	}
	
	void OnTriggerStay (Collider other)
	{
		if (other.gameObject.layer == C.LAYER_SELECTABLE)
		{
			if (_target != other.gameObject)
			{
				_target = other.gameObject;
			}
		}
	}
	
	void OnTriggerExit (Collider other)
	{
		//TODO: better solution
		if (other.gameObject == _target) 
		{
			_target = null;
		}
	}
	
	void OnDisable ()
	{
//		EventManager.OnTargetLockOn -= ToggleWaypoint;
//		EventManager.OnTargetLockOn -= LockOn;
	}
	
//	void SetColor ()
//	{
//		if (Master.gestureController.IsAnyHandClenched)
//		{
//			if (Master.gestureController.IsLeftFistPower && Master.pickupManager.bulletCount != 0)
//			{
//				_powerColor = Master.colorManager.powerLeft;
//			}
//			else if (Master.gestureController.IsRightFistPower && Master.pickupManager.bulletCount != 0)
//			{
//				//				_powerColor = Master.colorManager.powerRight;
//				_powerColor = Master.colorManager.powerLeft;
//			}
//			else
//			{
//				_powerColor = Master.colorManager.wpDefaultColor;
//			}
//			
//			SetWaypointColor ();
//		}
//		else
//		{
//			ResetWaypointColor ();
//		}
//	}
	
	void FollowCursor ()
	{
		Vector3 targetWaypointPos = Selection.GetTargetHitPosition ();
		
		transform.position = Vector3.MoveTowards (transform.position, targetWaypointPos, _followSpeed * Time.deltaTime);
	}
	
	void LockOn ()
	{
		if (Master.eventManager.IsCharacterTargeting) 
		{
			if (_target != null)
			{
				Master.eventManager.ChangeCharacterMode (EventManager.CharacterCommand.LockedOn);
				transform.position = _target.transform.position;
				StartCoroutine ("SetLocked");
			}
		}
	}

	public void ToggleWaypoint ()
	{
		if (_isTargeting) 
		{
			FadeWaypointOut ();
			_isTargeting = false;
		}
		else
		{
			FadeWaypointIn ();
			_isTargeting = true;
		}
	}
	
	public void FadeWaypointIn ()
	{
		StopCoroutine ("FadeOut");
		StartCoroutine ("FadeIn");
	}
	
	public void FadeWaypointOut ()
	{
		StopCoroutine ("FadeIn");
		StartCoroutine ("FadeOut");
	}
	
//	public void SetWaypointColor ()
//	{
//		StopCoroutine ("ResetColor");
//		StartCoroutine ("SetPowerColor");
//	}
//	
//	public void ResetWaypointColor ()
//	{
//		StopCoroutine ("SetPowerColor");
//		StartCoroutine ("ResetColor");
//	}
	
	IEnumerator FadeIn ()
	{
		while (_targetPoint.intensity < MAX_LIGHT_INTENSITY)
		{
			_targetPoint.intensity += 0.1f;
			yield return new WaitForSeconds (_fadeSpeed);
		}
	}
	
	IEnumerator FadeOut ()
	{
		while (_targetPoint.intensity > MIN_LIGHT_INTENSITY)
		{
			_targetPoint.intensity -= 0.1f;
			yield return new WaitForSeconds (_fadeSpeed);
		}
	}

	IEnumerator SetLocked ()
	{
		_isLockedOn = true;
		yield return new WaitForSeconds (_lockOnTime);
		_isLockedOn = false;
		Master.eventManager.ChangeCharacterMode (EventManager.CharacterCommand.NoAction);
	}
	
//	IEnumerator SetPowerColor ()
//	{
//		while (_targetPoint.color != _powerColor)
//		{
//			_targetPoint.color = Color.Lerp (_targetPoint.color, _powerColor, _fadeSpeed);
//			yield return new WaitForSeconds (_fadeSpeed);
//		}
//	}
//	
//	IEnumerator ResetColor ()
//	{
//		_powerColor = Master.colorManager.wpDefaultColor;
//		while (_targetPoint.color != Master.colorManager.wpDefaultColor)
//		{
//			_targetPoint.color = Color.Lerp (_targetPoint.color, Master.colorManager.wpDefaultColor, _fadeSpeed);
//			yield return new WaitForSeconds (_fadeSpeed);
//		}
//	}
}