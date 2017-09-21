using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class AvatarMove : MonoBehaviour
{
	public AudioClip deathClip;
	private Animator _eAnim;
	private AudioSource _eSound;
	
	private UnityEngine.AI.NavMeshAgent _nav;
	private GameObject _targetObj;
	private GameObject _wpObj;
	private Vector3 _waypoint;
	private bool _hasNewDestination = false;
	private bool _isTargeting = false;

	void Awake ()
	{
		_eAnim = GetComponent<Animator> ();
		_nav = GetComponent<UnityEngine.AI.NavMeshAgent> ();
		_eSound = GetComponent<AudioSource> ();
		_targetObj = GameObject.FindGameObjectWithTag (C.TAG_TARGET_POINT);
		_wpObj = GameObject.FindGameObjectWithTag (C.TAG_FOLLOW_POINT);
	}
	
	void Start ()
	{
		EventManager.OnFollowCommand += MoveCommand;
		EventManager.OnTargetLockOn += LookAtTarget;
		EventManager.OnDead += Death;
	}
	
	void Update ()
	{
		if (_nav.remainingDistance <= _nav.stoppingDistance) 
		{
			StopMovement ();
		}

		if (_isTargeting) 
		{
			transform.LookAt (_targetObj.transform);
		}

		Move ();
	}
	
	void OnDisable ()
	{
		EventManager.OnFollowCommand -= MoveCommand;
		EventManager.OnTargetLockOn -= LookAtTarget;
		EventManager.OnDead -= Death;
	}

	public void LookAtTarget ()
	{
		if (_isTargeting) 
		{
			_isTargeting = false;
		}
		else
		{
			_isTargeting = true;
		}
	}
	
	void MoveCommand ()
	{
		if (!Master.playerController.IsDead) 
		{
			_hasNewDestination = true;
		}
	}

	void Move ()
	{
		if (_hasNewDestination) 
		{
			StopMovement ();
			Master.playerController.SetPlayerToFollow ();
			_waypoint = _wpObj.transform.position;
			_nav.SetDestination (_waypoint);
			_eAnim.SetBool (C.ANIM_PLAYER_MOVING, true);
			_hasNewDestination = false;
		}
	}
	
	void StopMovement ()
	{
		if (!Master.playerController.IsDead) 
		{
			Master.playerController.SetPlayerToIdle ();
			_eAnim.SetBool (C.ANIM_PLAYER_MOVING, false);
			_nav.ResetPath ();
		}
	}
	
	void Death ()
	{
		if (!Master.playerController.IsDead)
		{	
			_nav.enabled = false;
			
			_eAnim.SetTrigger(C.ANIM_PLAYER_DEAD);
			
			_eSound.clip = deathClip;
			_eSound.Play ();
			
			Master.playerController.SetPlayerToDead ();
		}
	}
}

//using UnityEngine;
//using System.Collections;
//
//[RequireComponent(typeof(Rigidbody))]
//public class PlayerMovement : MonoBehaviour
//{
//	public AudioClip deathClip;
//
//	[SerializeField]
//	[Range(0f, 10f)]
//	private float _moveSpeed = 5f;
//
//	private bool _isDead = false;
//
//	private Vector3 _movementDirection;
////	private Vector3 lastSeenWaypointPosition;
//	private GameObject _followTargetObject;
//	private GameObject _targetingObject;
//
//	private Animator _pAnim;
//	private Rigidbody _pRigidbody;
//	private AudioSource _pSound;
////	private WaypointManager _wpManager;
//
//	void Awake ()
//	{
//		_pAnim = GetComponent<Animator> ();
//		_pRigidbody = GetComponent<Rigidbody> ();
//		_pSound = GetComponent<AudioSource> ();
////		_wpManager = GameObject.FindGameObjectWithTag (C.TAG_WAYPOINT_MANAGER).GetComponent <WaypointManager> ();
//		_followTargetObject = GameObject.FindGameObjectWithTag (C.TAG_FOLLOW_POINT);
//		_targetingObject = GameObject.FindGameObjectWithTag (C.TAG_TARGET_POINT);
//	}
//
//	void FixedUpdate ()
//	{
//		Animate ();
//		SetMovement ();
//	}
//
//	void SetMovement ()
//	{
//		switch (Master.playerController.currentState)
//		{
//			case PlayerController.PlayerState.Following:
//				Move ();
//				Rotate ();
//				break;
//			case PlayerController.PlayerState.Attacking:
//				Rotate ();
//				break;
//			case PlayerController.PlayerState.Waiting:
//				Rotate ();
//				break;
//			case PlayerController.PlayerState.Idle:
//				break;
//			case PlayerController.PlayerState.Dead:
//				break;
//			default:
//				break;
//		}
//	}
//
//	void Move ()
//	{
////		if (Master.eventManager.IsFollowCommand) 
////		{
////			_movementDirection = GetMovement ();
////			_pRigidbody.MovePosition (transform.position + _movementDirection);
////		}
////		//TODO: move towards follow point idle when reached
////		else if (_wpManager.previousWaypoint != null) 
////		{
////			//TODO: obsolete
////			lastSeenWaypointPosition = _wpManager.previousWaypoint.transform.position;
////			transform.position = Vector3.MoveTowards (transform.position, lastSeenWaypointPosition, _moveSpeed * Time.deltaTime);
////		}
////		else
////		{
////			Master.playerController.SetPlayerToIdle ();
////		}
//
//		if (!Master.eventManager.IsAttackCommand)
//		{
//			Vector3 targetPos = _followTargetObject.transform.position;
//			transform.position = Vector3.MoveTowards (transform.position, targetPos, _moveSpeed * Time.deltaTime);
//		}
//	}
//	
//	void Rotate ()
//	{
////		if (Master.eventManager.IsFollowCommand || Master.eventManager.IsAttackCommand) 
////		{
////			Vector3 toWaypoint = Vector3.zero;
////			//TODO: no more prev wp
////			if (_wpManager.previousWaypoint != null) 
////			{
////				//TODO: rotate to follow point if targeting point not active
////				toWaypoint = _wpManager.previousWaypoint.transform.position - transform.position;
////			}
////			else
////			{
////				toWaypoint = _wpManager.activeWaypoint.transform.position - transform.position;
////				//TODO:rotate to targeting point
////			}
////
////			toWaypoint.y = 0f;
////			
////			Quaternion rotation = Quaternion.LookRotation (toWaypoint);
////			_pRigidbody.MoveRotation (rotation);
////		}
//
//		Vector3 toLookPos;
//
//		if (Master.eventManager.IsAttackCommand) 
//		{
//			toLookPos = _targetingObject.transform.position - transform.position;
//		}
//		else
//		{
//			toLookPos = _followTargetObject.transform.position - transform.position;
//		}
//
//		toLookPos.y = 0.0f;
//
//		Quaternion rotation = Quaternion.LookRotation (toLookPos);
//		_pRigidbody.MoveRotation (rotation);
//	}
//	
//	void Animate ()
//	{
//		switch (Master.playerController.currentState)
//		{
//			case PlayerController.PlayerState.Following:	
//				_pAnim.SetBool(C.ANIM_PLAYER_MOVING, true);
//				break;
//			case PlayerController.PlayerState.Attacking:
//				_pAnim.SetBool (C.ANIM_PLAYER_MOVING, false);
//				break;
//			case PlayerController.PlayerState.Waiting:
//			case PlayerController.PlayerState.Idle:			
//				_pAnim.SetBool (C.ANIM_PLAYER_MOVING, false);
//				break;
//			case PlayerController.PlayerState.Dead:
//				Death ();
//				break;
//			default:			
//				_pAnim.SetBool (C.ANIM_PLAYER_MOVING, false);
//				break;
//		}
//	}
//	
//	void Death ()
//	{
//		if (!_isDead)
//		{
//			_isDead = true;
//			
//			_pAnim.SetTrigger(C.ANIM_PLAYER_DEAD);
//			
//			_pSound.clip = deathClip;
//			_pSound.Play ();
//
//			this.enabled = false;
//		}
//	}
//	
//	Vector3 GetMovement ()
//	{
//		return transform.forward.normalized * _moveSpeed * Time.deltaTime;
//	}
//}