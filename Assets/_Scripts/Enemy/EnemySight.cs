using UnityEngine;
using System.Collections;

public class EnemySight : MonoBehaviour
{
	private EnemyController _controller;
	private bool _isPlayerInSight = false;
	public bool canChase = true;
	public bool isPlayerInSight
	{
		get
		{
			return _isPlayerInSight;
		}
	}

	void Awake ()
	{
		_controller = GetComponentInParent<EnemyController> ();
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.CompareTag (C.TAG_PLAYER)) 
		{
			_isPlayerInSight = true;
			if (canChase) 
			{
				_controller.SetToChasing ();
			}
		}
	}

	void OnTriggerStay (Collider other)
	{
//		if (other.CompareTag (C.TAG_PLAYER))
//		{
//			_isPlayerInSight = true;
//			_controller.SetToChasing ();
//		}
	}

	void OnTriggerExit (Collider other)
	{
		if (other.CompareTag (C.TAG_PLAYER)) 
		{
			_isPlayerInSight = false;
			_controller.SetToPatrolling ();
		}
	}
}