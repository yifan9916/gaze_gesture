using UnityEngine;
using System.Collections;

public class EnemeyTrigger : MonoBehaviour
{	
	public float _timeUntilAttack = 0.1f;
	private GameObject _enemy;
	private EnemyController _controller;
	private EnemySight _sight;
	private float _timer;

	void Awake ()
	{
		_enemy = transform.parent.gameObject;
		_controller = _enemy.GetComponent<EnemyController> ();
		_sight = _enemy.GetComponentInChildren<EnemySight> ();
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.CompareTag (C.TAG_PLAYER)) 
		{
			if (!Master.playerController.IsDead && !_controller.IsDead && _sight.isPlayerInSight) 
			{
				Invoke ("KillPlayer", _timeUntilAttack);
			}
		}
	}

	void OnTriggerStay (Collider other)
	{
//		if (other.CompareTag (C.TAG_PLAYER))
//		{
//			if (!Master.playerController.IsDead && !_controller.IsDead && _sight.isPlayerInSight) 
//			{
//				_timer += Time.deltaTime;
//				
//				if (_timer >= _timeUntilAttack) 
//				{
//					_controller.Attack ();
//				}
//			}
//		}
	}
	
	void OnTriggerExit (Collider other)
	{
//		if (other.CompareTag (C.TAG_PLAYER)) 
//		{
//			_timer = 0;
//		}
		CancelInvoke ();
	}

	void KillPlayer ()
	{
		_controller.Attack ();
	}
}