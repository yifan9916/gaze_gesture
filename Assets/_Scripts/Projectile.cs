using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class Projectile : MonoBehaviour
{
	private float _timeToDisable;
	protected float timeToDisable 
	{
		get
		{
			return _timeToDisable;
		}

		set
		{
			_timeToDisable = value;
		}
	}

	protected abstract void Disable ();
}