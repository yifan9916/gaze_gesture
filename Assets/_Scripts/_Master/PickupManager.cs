using UnityEngine;
using System.Collections;

public class PickupManager : MonoBehaviour
{
	private int _aerialProjectileCount;
	public int aerialProjectileCount
	{
		get
		{
			return _aerialProjectileCount;
		}
	}

	private int _bulletCount;
	public int bulletCount
	{
		get
		{
			return _bulletCount;
		}
	}

	public bool isAerialProjectilesEmpty
	{
		get
		{
			return _aerialProjectileCount == 0;
		}
	}

	public bool isBulletsEmpty
	{
		get
		{
			return _bulletCount == 0;
		}
	}

	private GameObject[] _pickups;

	void Awake ()
	{
		_aerialProjectileCount = 0;
		_bulletCount = 0;
		_pickups = GameObject.FindGameObjectsWithTag (C.TAG_PICKUP);
	}

	void Update ()
	{
		ActivatePickups ();

//		if (Input.GetKeyDown (KeyCode.Q)) 
//		{
//			AddProjectiles (1);
//		}

//		Debug.Log ("bullets: " + _bulletCount + " projectiles: " + _aerialProjectileCount);
	}

	void ActivatePickups()
	{
		if (_bulletCount == 0) 
		{
			for (int i = 0; i < _pickups.Length; i++) 
			{
				_pickups[i].SetActive (true);
			}
		}
	}

	public void AddProjectiles (int amount)
	{
		for (int i = 0; i < amount; i++) 
		{
			_bulletCount++;
		}
		_aerialProjectileCount = _bulletCount / 2;
	}

	public void RemoveAerialProjectile ()
	{
		_bulletCount -= 2;
		_aerialProjectileCount = _bulletCount / 2;
	}

	public void RemoveBullet ()
	{
		_bulletCount--;
		_aerialProjectileCount = _bulletCount / 2;
	}
}