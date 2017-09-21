//using UnityEngine;
//using System.Collections;
//using System.Collections.Generic;
//
//[DisallowMultipleComponent]
//public class WaypointManager : MonoBehaviour
//{
//	private const float WAYPOINT_INITIAL_INTENSITY = 1.0f;
//	public GameObject waypointObj;
//
//	private GameObject _activeWaypoint;
//	public GameObject activeWaypoint
//	{
//		get
//		{
//			return _activeWaypoint;
//		}
//	}
//
//	private GameObject _previousWaypoint;
//	public GameObject previousWaypoint
//	{
//		get 
//		{
//			return _previousWaypoint;
//		}
//	}
//	
//	private List<GameObject> pool;
//	[SerializeField] 
//	private int poolSize = 2;
//	[SerializeField] 
//	private bool _isExpandable = true;
//	
//	private Vector3 _waypointPosition;
//	
//	private bool _isWaypointSet;
//	public bool isWaypointSet 
//	{
//		get
//		{
//			return _isWaypointSet;
//		}
//	}
//
//	void Awake ()
//	{
//		_isWaypointSet = false;
//
//		pool = new List<GameObject> ();
//
//		for (int i = 0; i < poolSize; i++)
//		{
//			GameObject wp = (GameObject) Instantiate (waypointObj);
//			wp.SetActive (false);
//			pool.Add (wp);
//		}
//	}
//
//	void Start ()
//	{
////		EventManager.OnLockOn += SetWayPoint;
//		SetWayPoint ();
//	}
//
//	public void SetWayPoint ()
//	{	
//		DisablePreviousWaypoint ();
//
//		if (_isWaypointSet && _activeWaypoint != null) 
//		{
//			ResetActiveWaypoint ();
//		}
//
//		if (!_isWaypointSet) 
//		{
//			SetNewActiveWaypoint ();
//		}
//	}
//
//	void SetNewActiveWaypoint ()
//	{
//		GameObject newWaypoint = GetPooledWaypoint ();
//		
//		if (newWaypoint != null) 
//		{
//			_isWaypointSet = true;
//			newWaypoint.SetActive (true);
//			
//			if (Master.eventManager.IsFollowCommand) 
//			{
//				newWaypoint.GetComponentInChildren<Light> ().intensity = WAYPOINT_INITIAL_INTENSITY;
//			}
//			
//			newWaypoint.GetComponent<Waypoint> ().enabled = true;
//			newWaypoint.transform.position = Selection.GetTargetHitPosition ();
//			_activeWaypoint = newWaypoint;
//		}
//	}
//
//	void ResetActiveWaypoint ()
//	{
//		_isWaypointSet = false;
//		_activeWaypoint.GetComponent<Waypoint> ().Deactivate ();
//		_previousWaypoint = _activeWaypoint;
//		_activeWaypoint = null;
//	}
//
//	public void DisablePreviousWaypoint ()
//	{
//		if (_previousWaypoint != null) 
//		{
//			_previousWaypoint.SetActive (false);
//			_previousWaypoint = null;
//		}
//	}
//
//	public GameObject GetActiveWaypoint ()
//	{
//		return _activeWaypoint;
//	}
//	
//	public GameObject GetPooledWaypoint ()
//	{
//		for (int i = 0; i < pool.Count; i++)
//		{
//			if (!pool[i].activeInHierarchy)
//			{
//				return pool[i];
//			}
//		}
//		
//		if (_isExpandable)
//		{
//			GameObject wp = (GameObject)Instantiate (waypointObj);
//			pool.Add (wp);
//			return wp;
//		}
//		
//		return null;
//	}
//}