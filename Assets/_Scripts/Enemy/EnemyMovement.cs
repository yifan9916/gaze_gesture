using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody))]
public class EnemyMovement : MonoBehaviour
{
	public AudioClip deathClip;
	public List<GameObject> waypoints;

	private EnemyController _controller;

	private Transform _player;

	private Vector3 _spawnPosition;
	private Quaternion _spawnRotation;
	private Animator _eAnim;
	private AudioSource _eSound;
	private Rigidbody _rbody;
	private CapsuleCollider _collider;
	private Light[] _eLights;

	private UnityEngine.AI.NavMeshAgent _nav;
	private float _distance;
	private int _currentNavPoint = 0;
	private float _restTime = 2.0f;

	private bool _isResting = false;
	private bool _isDead = false;

	void Awake ()
	{
		_player = GameObject.FindGameObjectWithTag (C.TAG_PLAYER).transform;

		_controller = GetComponent<EnemyController> ();
		_eAnim = GetComponent<Animator> ();
		_nav = GetComponent<UnityEngine.AI.NavMeshAgent> ();
		_eSound = GetComponent<AudioSource> ();
		_rbody = GetComponent<Rigidbody> ();
		_collider = GetComponentInChildren<CapsuleCollider> ();
		_eLights = GetComponentsInChildren<Light> ();
	}

	void Start ()
	{
		_spawnPosition = GetComponent<Transform> ().position;
		_spawnRotation = GetComponent<Transform> ().rotation;
	}

	void Update ()
	{
		SetMovement ();
	}

	void SetMovement ()
	{
		switch (_controller.currentState) 
		{
			case EnemyController.EnemyState.Chasing:
				ChasePlayer ();
				break;
			case EnemyController.EnemyState.Patrolling:
				Patrol ();
				break;
			case EnemyController.EnemyState.Idle:
				StopMovement ();
				break;
			case EnemyController.EnemyState.Dead:
				Death ();
				break;
			default:
				break;
		}
	}

	void Patrol ()
	{
		if (waypoints.Count != 0) 
		{
			_nav.SetDestination (waypoints[_currentNavPoint].transform.position);
			
			if (!_nav.pathPending) 
			{
				if (_nav.remainingDistance <= _nav.stoppingDistance)
				{
					RestAtWaypoint ();
				}
				else
				{
					MoveToNextWaypoint ();
				}
			}
		}
		else
		{
			ReturnToSpawn ();
		}
	}

	void ReturnToSpawn()
	{
		if (_nav.remainingDistance <= _nav.stoppingDistance) 
		{
			//TODO: set to idle
			transform.rotation = _spawnRotation;
			_eAnim.SetBool (C.ANIM_ENEMY_MOVING, false);
		}
		else
		{
			_nav.SetDestination (_spawnPosition);
		}
	}

	void MoveToNextWaypoint ()
	{
		StopCoroutine ("Rest");
		_isResting = false;
		_eAnim.SetBool (C.ANIM_ENEMY_MOVING, true);
	}

	void RestAtWaypoint ()
	{
		if (!_isResting) 
		{
			_isResting = true;
			_eAnim.SetBool (C.ANIM_ENEMY_MOVING, false);
			StartCoroutine ("Rest");
		}
	}

	void SetNextPatrolPoint ()
	{
		if (_currentNavPoint == waypoints.Count-1) 
		{
			_currentNavPoint = 0;
		}
		else
		{
			_currentNavPoint++;
		}
	}

	void ChasePlayer ()
	{
		if (!Master.playerController.IsDead) 
		{
			_eAnim.SetBool (C.ANIM_ENEMY_MOVING, true);
			_nav.SetDestination (_player.position);
		}
		else
		{
			_controller.SetToIdle ();
		}
	}

	void StopMovement ()
	{
		_eAnim.SetBool (C.ANIM_ENEMY_MOVING, false);
		_nav.ResetPath ();
	}
	
	void Death ()
	{
		if (!_isDead)
		{
			_isDead = true;

			for (int i = 0; i < _eLights.Length; i++) 
			{
				_eLights[i].enabled = false;
			}

			_nav.enabled = false;
			_collider.isTrigger = true;
			_rbody.isKinematic = true;

			_eAnim.SetTrigger(C.ANIM_ENEMY_DEAD);

			_eSound.clip = deathClip;
			_eSound.Play ();
		}
	}

	IEnumerator Rest ()
	{
		yield return new WaitForSeconds (_restTime);
		SetNextPatrolPoint ();
	}
}