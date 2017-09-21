using UnityEngine;
using System.Collections;

public class Waypoint : MonoBehaviour
{
	private const float MAX_LIGHT_INTENSITY = 4.0f;
	private const float MIN_LIGHT_INTENSITY = 0.0f;
	
	[SerializeField]
	private float _followSpeed = 7.0f;
	private float _fadeSpeed = 0.05f;
	private float _disableTime = 2.5f;
	
	private Light _waypoint;
	private Color _powerColor;
	private GameObject _target;

	void Awake ()
	{
		_waypoint = GetComponentInChildren<Light> ();
	}
	
	void Update ()
	{
		FollowCursor ();
//		LockOn ();
		SetColor ();
	}
	
	void OnEnable ()
	{
		EventManager.OnFollowCommand 	+= FadeWaypointIn;
		EventManager.OnTargetLockOn 	+= FadeWaypointIn;
		EventManager.OnRoamCommand 		+= FadeWaypointOut;
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
		EventManager.OnFollowCommand 	-= FadeWaypointIn;
		EventManager.OnTargetLockOn 			-= FadeWaypointIn;
		EventManager.OnRoamCommand 		-= FadeWaypointOut;
	}

	void SetColor ()
	{
		if (Master.gestureController.IsAnyHandClenched)
		{
			if (Master.gestureController.IsLeftFistPower && Master.pickupManager.bulletCount != 0)
			{
				_powerColor = Master.colorManager.powerLeft;
			}
			else if (Master.gestureController.IsRightFistPower && Master.pickupManager.bulletCount != 0)
			{
//				_powerColor = Master.colorManager.powerRight;
				_powerColor = Master.colorManager.powerLeft;
			}
			else
			{
				_powerColor = Master.colorManager.wpDefaultColor;
			}

			SetWaypointColor ();
		}
		else
		{
			ResetWaypointColor ();
		}
	}

	void FollowCursor ()
	{
		//TODO: and if not attack command
		if (!Master.gestureController.IsAnyHandClenched)
		{
			Vector3 targetWaypointPos = Selection.GetTargetHitPosition ();

			transform.position = Vector3.MoveTowards (transform.position, targetWaypointPos, _followSpeed * Time.deltaTime);
		}
	}
	
//	void LockOn ()
//	{
//		if (Master.eventManager.IsAttackCommand) 
//		{
//			if (Master.gestureController.IsAnyHandClenched)
//			{
//				if (_target != null)
//				{
//					FadeWaypointIn ();
//					transform.position = _target.transform.position;
//				}
//			}
//		}
//	}

	public void Deactivate ()
	{
		StartCoroutine ("DisableThenDeactivate");
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

	public void SetWaypointColor ()
	{
		StopCoroutine ("ResetColor");
		StartCoroutine ("SetPowerColor");
	}

	public void ResetWaypointColor ()
	{
		StopCoroutine ("SetPowerColor");
		StartCoroutine ("ResetColor");
	}

	IEnumerator DisableThenDeactivate ()
	{
		GetComponent<Waypoint> ().enabled = false;
		yield return new WaitForSeconds (_disableTime);
		//TODO: duplicate
		while (_waypoint.intensity > MIN_LIGHT_INTENSITY)
		{
			_waypoint.intensity -= 0.1f;
			yield return new WaitForSeconds (_fadeSpeed);
		}

//		Master.waypointManager.DisablePreviousWaypoint ();
	}

	IEnumerator FadeIn ()
	{
		while (_waypoint.intensity < MAX_LIGHT_INTENSITY)
		{
			_waypoint.intensity += 0.1f;
			yield return new WaitForSeconds (_fadeSpeed);
		}
	}

	IEnumerator FadeOut ()
	{
		while (_waypoint.intensity > MIN_LIGHT_INTENSITY)
		{
			_waypoint.intensity -= 0.1f;
			yield return new WaitForSeconds (_fadeSpeed);
		}
	}

	IEnumerator SetPowerColor ()
	{
		while (_waypoint.color != _powerColor)
		{
			_waypoint.color = Color.Lerp (_waypoint.color, _powerColor, _fadeSpeed);
			yield return new WaitForSeconds (_fadeSpeed);
		}
	}

	IEnumerator ResetColor ()
	{
		_powerColor = Master.colorManager.wpDefaultColor;
		while (_waypoint.color != Master.colorManager.wpDefaultColor)
		{
			_waypoint.color = Color.Lerp (_waypoint.color, Master.colorManager.wpDefaultColor, _fadeSpeed);
			yield return new WaitForSeconds (_fadeSpeed);
		}
	}
}