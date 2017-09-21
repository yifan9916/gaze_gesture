using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ProjectileSpawn : MonoBehaviour
{
	public GameObject projectileObj;

	private List<GameObject> _projectilePool;
	[SerializeField]
	private int _poolSize = 5;

	private Vector3 _targetPosition;
	private float _force = 1000.0f;
	private GameObject _targetingObject;
//	private ThalmicMyo _thalmicMyo;

//	private bool _isOnCooldown = false;
//	private float _cooldownTime = 2.0f;

	void Awake ()
	{
		_projectilePool = new List<GameObject> ();

		for (int i = 0; i < _poolSize; i++) 
		{
			GameObject projectile = (GameObject) Instantiate (projectileObj);
			projectile.SetActive (false);
			_projectilePool.Add (projectile);
		}

		_targetingObject = GameObject.FindGameObjectWithTag (C.TAG_TARGET_POINT);
//		_thalmicMyo = GameObject.FindGameObjectWithTag (C.TAG_MYO).GetComponent<ThalmicMyo> ();
	}

	void Update ()
	{
		SetSpawnPosition ();

		if (Input.GetKeyDown (KeyCode.P)) 
		{
			SpawnProjectile ();
		}

//		if (Master.gestureController.IsVerticalSwipePowerLeft) 
//		{
//			SpawnProjectile ();
//		}

//		if (!_isOnCooldown) 
//		{
//			if (_thalmicMyo.pose == Thalmic.Myo.Pose.WaveOut) 
//			{
//				SpawnProjectile ();
//				StartCoroutine ("ProjectileCooldown");
//			}
//		}
	}

	void SetSpawnPosition ()
	{
//		if (_wpManager.activeWaypoint != null)
//		{
//			_targetPosition = _wpManager.activeWaypoint.transform.position;
//			_targetPosition.y = 20.0f;
//			transform.position = _targetPosition;
//		}

		_targetPosition = _targetingObject.transform.position;
		_targetPosition.y = 20.0f;
		transform.position = _targetPosition;
	}

	void SpawnProjectile ()
	{
		if (!Master.pickupManager.isAerialProjectilesEmpty) 
		{
			for (int i = 0; i < _projectilePool.Count; i++) 
			{
				if (!_projectilePool[i].activeInHierarchy) 
				{
					Master.pickupManager.RemoveAerialProjectile ();
					_projectilePool[i].transform.position = transform.position;
					_projectilePool[i].transform.rotation = transform.rotation;
					_projectilePool[i].SetActive (true);
					_projectilePool[i].GetComponent<Rigidbody> ().AddForce (-transform.up * _force);
					break;
				}
			}
		}
	}

//	IEnumerator ProjectileCooldown ()
//	{
//		_isOnCooldown = true;
//		yield return new WaitForSeconds (_cooldownTime);
//		_isOnCooldown = false;
//	}
}