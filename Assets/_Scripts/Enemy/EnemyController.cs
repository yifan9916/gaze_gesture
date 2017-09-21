using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{
	public enum EnemyState
	{
		Chasing,
		Patrolling,
		Idle,
		Dead
	}

	[SerializeField]
	private EnemyState _currentState = EnemyState.Patrolling;
	public EnemyState currentState
	{
		get
		{
			return _currentState;
		}
	}

	private AudioSource _eSound;
	private ParticleSystem _hitParticles;

	private float _destroyTime = 2.0f;
	private float _sinkSpeed = 2.0f;
	private bool _isSinking = false;

	void Awake ()
	{
		_eSound = GetComponent<AudioSource> ();
		_hitParticles = GetComponentInChildren<ParticleSystem> ();
	}

	void Update ()
	{
		if (_isSinking) 
		{
			transform.Translate (-Vector3.up * _sinkSpeed * Time.deltaTime);
		}
	}

	public void Attack ()
	{
		if (!Master.playerController.IsDead) 
		{
			Master.eventManager.PlayerDeath ();
		}
	}

	public void TakeDamage ()
	{
		if (IsDead)
		{
			return;
		}
		
		_eSound.Play ();
		_hitParticles.Play ();
	}

	void ChangeStateTo (EnemyState newState)
	{
		if (IsState (newState))
		{
			return;
		}
		else
		{
			_currentState = newState;
		}
	}

	public bool IsState (EnemyState state)
	{
		if (_currentState == state)
		{
			return true;
		}
		else
		{
			return false;
		}
	}

	public void SetToChasing ()
	{
		if (!Master.playerController.IsDead && !IsState (EnemyState.Dead)) 
		{
			ChangeStateTo (EnemyState.Chasing);
		}
	}

	public void SetToPatrolling ()
	{
		if (!Master.playerController.IsDead && !IsState (EnemyState.Dead)) 
		{
			ChangeStateTo (EnemyState.Patrolling);
		}
	}

	public void SetToIdle ()
	{
		if (!IsState (EnemyState.Dead)) 
		{
			ChangeStateTo (EnemyState.Idle);
		}
	}

	public void SetToDead ()
	{
		if (!IsState (EnemyState.Dead)) 
		{
			ChangeStateTo (EnemyState.Dead);
		}
	}
	
	public void Sink ()
	{
		_isSinking = true;
//		Destroy (gameObject, _destroyTime);
		Invoke ("Disable", _destroyTime);
	}

	void Disable ()
	{
		gameObject.SetActive (false);
	}

	#region check enemy states
	public bool IsChasing
	{
		get
		{
			return IsState (EnemyState.Chasing);
		}
	}

	public bool IsPatrolling
	{
		get
		{
			return IsState (EnemyState.Patrolling);
		}
	}

	public bool IsIdle
	{
		get
		{
			return IsState (EnemyState.Idle);
		}
	}

	public bool IsDead
	{
		get
		{
			return IsState (EnemyState.Dead);
		}
	}
	#endregion
}