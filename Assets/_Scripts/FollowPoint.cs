using UnityEngine;
using System.Collections;

public class FollowPoint : MonoBehaviour 
{
	private const float MAX_LIGHT_INTENSITY = 4.0f;
	private const float MIN_LIGHT_INTENSITY = 0.0f;
	
	[SerializeField]
	private float _fadeSpeed = 0.005f;
	
	private Light _followPoint;
	
	void Awake ()
	{
		_followPoint = GetComponentInChildren<Light> ();
	}
	
	void OnEnable ()
	{
		EventManager.OnFollowCommand 	+= FadeWaypointIn;
		EventManager.OnFollowCommand	+= SetFollowPoint;
//		EventManager.OnTargetLockOn 	+= FadeWaypointOut;
//		EventManager.OnRoamCommand 		+= FadeWaypointOut;
	}

	/**
	 * 
	 **/

	void OnDisable ()
	{
		EventManager.OnFollowCommand 	-= FadeWaypointIn;
		EventManager.OnFollowCommand	-= SetFollowPoint;
//		EventManager.OnTargetLockOn 	-= FadeWaypointOut;
//		EventManager.OnRoamCommand 		-= FadeWaypointOut;
	}
	
	void SetFollowPoint ()
	{
		Vector3 targetWaypointPos = Selection.GetTargetHitPosition ();
		transform.position = targetWaypointPos;
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
	
	IEnumerator FadeIn ()
	{
		while (_followPoint.intensity < MAX_LIGHT_INTENSITY)
		{
			_followPoint.intensity += 0.1f;
			yield return new WaitForSeconds (_fadeSpeed);
		}
	}
	
	IEnumerator FadeOut ()
	{
		while (_followPoint.intensity > MIN_LIGHT_INTENSITY)
		{
			_followPoint.intensity -= 0.1f;
			yield return new WaitForSeconds (_fadeSpeed);
		}
	}
}