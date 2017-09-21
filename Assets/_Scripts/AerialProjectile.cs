using UnityEngine;
using System.Collections;

public class AerialProjectile : Projectile
{
	private Rigidbody _rbody;
	private ParticleSystem _explosion;

	void Awake ()
	{
		_rbody = GetComponent<Rigidbody> ();
		_explosion = GetComponentInChildren<ParticleSystem> ();
		this.timeToDisable = 2.0f;
	}

	void OnCollisionEnter (Collision collision)
	{
		if (!_explosion.isPlaying) 
		{
			_explosion.Play ();
		}
	}

	void OnEnable ()
	{
		_explosion.Stop ();
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