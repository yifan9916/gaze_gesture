using UnityEngine;
using System.Collections;

public abstract class Pickup : MonoBehaviour
{
	protected const int BIG_PICKUP_AMOUNT = 10;
	protected const int SMALL_PICKUP_AMOUNT = 1;

	private int _pickupAmount;
	protected int pickupAmount
	{
		get
		{
			return _pickupAmount;
		}

		set
		{
			_pickupAmount = value;
		}
	}

	private float _turnSpeed;
	public float turnSpeed
	{
		get
		{
			return _turnSpeed;
		}

		set
		{
			_turnSpeed = value;
		}
	}

	protected abstract void RotatePickup ();
	protected abstract void PickupProjectiles ();
}