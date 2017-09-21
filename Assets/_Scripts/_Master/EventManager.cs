using UnityEngine;
using System.Collections;
using Thalmic.Myo;

[DisallowMultipleComponent]
public class EventManager : MonoBehaviour
{
	public enum CommandMode
	{
		Follow,
//		Attack, 
		Roam
	}

	public enum CharacterCommand
	{
		NoAction,
		Targeting,
		LockedOn
	}

	private CharacterCommand _currentCharacterCommand = CharacterCommand.NoAction;
	public CharacterCommand currentCharacterCommand
	{
		get
		{
			return _currentCharacterCommand;
		}
	}

	[SerializeField]
	private CommandMode _currentMode = CommandMode.Roam;
	public CommandMode currentMode
	{
		get
		{
			return _currentMode;
		}
	}

	public delegate void Follow ();
	public delegate void PlayerLockOn ();
	public delegate void TargetLockOn ();
	public delegate void Roam ();
	public delegate void Dead ();

	public static event Follow OnFollowCommand;
	public static event PlayerLockOn OnPlayerLockOn;
	public static event TargetLockOn OnTargetLockOn;
	public static event Roam OnRoamCommand;
	public static event Dead OnDead;

	private ThalmicMyo _thalmicMyo;

	void Awake ()
	{
		_thalmicMyo = GameObject.FindGameObjectWithTag (C.TAG_MYO).GetComponent<ThalmicMyo> ();
	}

	void Update ()
	{
//		if (!IsRoamCommand && !IsAttackCommand) 
//		{
//			CommandRoam ();
//		}
//
//		TraditionalInput ();
//		NaturalInput ();

//		Debug.Log ("character mode: " + _currentCharacterCommand);
		SetInputMethod ();
	}

	void SetInputMethod ()
	{
		switch (GameController.currentInputMethod) 
		{
		case GameController.InputMethod.NaturalInput:
			NaturalInput ();
			break;
		case GameController.InputMethod.TraditionalInput:
			TraditionalInput ();
			break;
		default:
			TraditionalInput ();
			break;
		}
	}
	
	void TraditionalInput ()
	{
		if (Input.GetMouseButton (0))
		{
			CommandFollow ();
		}

		if (Input.GetKeyDown (KeyCode.Space)) 
		{
			CommandTogglePlayerLockOn ();
		}

		if (Input.GetKeyDown (KeyCode.Tab)) 
		{
			CommandTargetLockOn ();
		}
	}
	
	void NaturalInput ()
	{
		if (_thalmicMyo.pose == Pose.Fist) 
		{
			CommandPlayerLockOn ();
		}

		if (_thalmicMyo.pose == Pose.FingersSpread) 
		{
			CommandFollow ();
		}

		if (_thalmicMyo.pose == Pose.WaveIn) 
		{
			CommandTargetLockOn ();
		}

		if (_thalmicMyo.pose == Pose.Rest) 
		{
			CommandRoam ();
		}
	}

	public void CommandFollow ()
	{
		if (OnFollowCommand != null)
		{
			OnFollowCommand ();
		}
	}

	public void CommandPlayerLockOn ()
	{
		if (!IsFollowCommand) 
		{
			ChangeMode (CommandMode.Follow);

			if (OnPlayerLockOn != null) 
			{
				OnPlayerLockOn ();
			}
		}
	}

	public void CommandTogglePlayerLockOn ()
	{
		if (!IsFollowCommand) 
		{
			ChangeMode (CommandMode.Follow);
			
			if (OnPlayerLockOn != null) 
			{
				OnPlayerLockOn ();
			}
		}
		else
		{
			CommandRoam ();
		}
	}

	public void CommandTargetLockOn ()
	{
		if (!IsCharacterTargeting && !IsCharacterLockedOn) 
		{
			_currentCharacterCommand = CharacterCommand.Targeting;
		}
		else
		{
			_currentCharacterCommand = CharacterCommand.NoAction;
		}

		if (OnTargetLockOn != null) 
		{
			OnTargetLockOn ();
		}
	}

//	public void CommandToggleTargetLockOn ()
//	{
//		if (!IsAttackCommand) 
//		{
//			ChangeMode (CommandMode.Attack);
//
//			if (OnTargetLockOn != null) 
//			{
//				OnTargetLockOn ();
//			}
//		}
//		else
//		{
//			CommandRoam ();
//		}
//	}

	public void CommandRoam ()
	{
		if (!IsRoamCommand) 
		{
			ChangeMode (CommandMode.Roam);

			if (OnRoamCommand != null)
			{
				OnRoamCommand ();
			}
		}
	}
	
	public void PlayerDeath ()
	{
		if (OnDead != null)
		{
			OnDead ();
		}
	}

	public void ChangeCharacterMode (CharacterCommand newMode)
	{
		if (_currentCharacterCommand == newMode) 
		{
			return;
		}
		else
		{
			_currentCharacterCommand = newMode;
		}
	}

	public bool IsCharacterLockedOn
	{
		get
		{
			return _currentCharacterCommand == CharacterCommand.LockedOn;
		}
	}

	public bool IsCharacterTargeting
	{
		get
		{
			return _currentCharacterCommand == CharacterCommand.Targeting;
		}
	}

	public bool IsCharacterNoAction
	{
		get
		{
			return _currentCharacterCommand == CharacterCommand.NoAction;
		}
	}
	
	private void ChangeMode (CommandMode newMode)
	{
		if (IsMode (newMode))
		{
			return;
		}
		else
		{
			_currentMode = newMode;
		}
	}
	
	public bool IsMode (CommandMode mode)
	{
		if (_currentMode == mode)
		{
			return true;
		}
		else
		{
			return false;
		}
	}
	
	public bool IsFollowCommand
	{
		get
		{
			return IsMode (CommandMode.Follow);
		}
	}

//	public bool IsAttackCommand
//	{
//		get
//		{
//			return IsMode (CommandMode.Attack);
//		}
//	}

	public bool IsRoamCommand
	{
		get
		{
			return IsMode (CommandMode.Roam);
		}
	}
}