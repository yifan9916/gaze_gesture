using UnityEngine;
using System.Collections;

public class SmallPickup : Pickup
{
	void Awake ()
	{
		this.pickupAmount = SMALL_PICKUP_AMOUNT;
		this.turnSpeed = 75.0f;
	}

	void Update ()
	{
		RotatePickup ();
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.CompareTag (C.TAG_PLAYER)) 
		{
			PickupProjectiles ();
		}
	}
	
	protected override void RotatePickup ()
	{
		transform.Rotate (Vector3.up * turnSpeed * Time.deltaTime);
	}

	protected override void PickupProjectiles ()
	{
		Master.pickupManager.AddProjectiles (pickupAmount);
		gameObject.SetActive (false);
	}
}