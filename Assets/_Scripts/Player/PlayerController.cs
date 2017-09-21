using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	public enum PlayerState
	{
		Following,
		Waiting,
		Idle,
		Attacking,
		Dead
	}

	[SerializeField]
	private PlayerState _currentState = PlayerState.Idle;
	public PlayerState currentState 
	{
		get
		{
			return _currentState;
		}
	}

	private ParticleSystem _powerIndicator;

	void Awake ()
	{
		_powerIndicator = gameObject.GetComponentInChildren<ParticleSystem> ();
	}

	void Start ()
	{
		EventManager.OnTargetLockOn += SetPlayerToAttacking;
	}

	void Update ()
	{
		DisplayPowerAvailable ();
	}

	void DisplayPowerAvailable ()
	{
//		if (Master.pickupManager.bulletCount > 0 && Master.eventManager.IsAttackCommand) 
		if (Master.pickupManager.bulletCount > 0 && (Master.eventManager.IsCharacterTargeting || Master.eventManager.IsCharacterLockedOn))
		{
			if (!_powerIndicator.isPlaying) 
			{
				_powerIndicator.Play ();
			}
		}
		else
		{
			if (_powerIndicator.isPlaying) 
			{
				_powerIndicator.Stop ();
			}
		}
	}

//	public void ShowBulletsAvailable ()
//	{
//		if (Master.pickupManager.bulletCount > 0) 
//		{
//			if (!_powerIndicator.isPlaying) 
//			{
//				_powerIndicator.Play ();
//			}
//		}
//		else
//		{
//			if (_powerIndicator.isPlaying) 
//			{
//				_powerIndicator.Stop ();
//			}
//		}
//	}

	void ChangePlayerState (PlayerState newState)
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
	
	public bool IsState (PlayerState state)
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

	#region set player states
	public void SetPlayerToFollow ()
	{
		if (!IsDead)
		{
			ChangePlayerState (PlayerState.Following);
		}
	}

//	public void SetPlayerToWaiting ()
//	{
//		if (!IsDead && Master.eventManager.IsFollowCommand)
//		{
//			ChangePlayerState (PlayerState.Waiting);
//		}
//	}

	public void SetPlayerToIdle ()
	{
		if (!IsDead) 
		{	
			ChangePlayerState (PlayerState.Idle);
		}
	}

	public void SetPlayerToAttacking ()
	{
//		if (!IsDead && Master.eventManager.IsAttackCommand)
		if (!IsDead)
		{
			ChangePlayerState (PlayerState.Attacking);
		}
	}

	public void SetPlayerToDead ()
	{
		ChangePlayerState (PlayerState.Dead);
	}
	#endregion

	#region check player states
	public bool IsFollowing
	{
		get
		{
			return IsState (PlayerState.Following);
		}
	}

	public bool IsAttacking
	{
		get
		{
			return IsState (PlayerState.Attacking);
		}
	}

	public bool IsWaiting
	{
		get
		{
			return IsState (PlayerState.Waiting);
		}
	}

	public bool IsIdle
	{
		get
		{
			return IsState (PlayerState.Idle);
		}
	}
	
	public bool IsDead
	{
		get
		{
			return IsState (PlayerState.Dead);
		}
	}
	#endregion
}