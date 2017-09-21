using UnityEngine;
using System.Collections;

public class GunProjectile : Projectile
{
	private Rigidbody _rbody;

	void Awake ()
	{
		_rbody = GetComponent<Rigidbody> ();
		this.timeToDisable = 0.75f;
	}

	void OnEnable ()
	{
		Invoke ("Disable", timeToDisable);
	}

	void OnDisable ()
	{
		CancelInvoke ();
	}

	protected override void Disable ()
	{
		_rbody.velocity = Vector3.zero;
		gameObject.SetActive (false);
	}
}