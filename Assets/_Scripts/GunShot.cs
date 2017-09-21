using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GunShot : MonoBehaviour
{
	public List<GameObject> bullets;
	public List<GameObject> bulletSpawnPoints;
	public AudioClip shotSound;
	private AudioSource _shotSoundSource;
	private List<GameObject> _bulletPool;
	private int _poolSize = 6;

	private float _force = 500.0f;
	private bool _isOnCooldown = false;
	private float _cooldownTime = 2.0f;

	private ThalmicMyo _thalmicMyo;

	void Awake ()
	{
		_bulletPool = new List<GameObject> ();

		for (int i = 0; i < _poolSize; i++) 
		{
			GameObject bullet = (GameObject) Instantiate (bullets[Random.Range(0, bullets.Count)]);
			bullet.SetActive (false);
			_bulletPool.Add (bullet);
		}

		_shotSoundSource = GetComponent<AudioSource> ();
		_thalmicMyo = GameObject.FindGameObjectWithTag(C.TAG_MYO).GetComponent<ThalmicMyo> ();
	}

	void Update ()
	{
		if (Input.GetMouseButtonDown (1)) 
		{
			ShootBullets ();
//			Master.eventManager.CommandRoam ();
		}

		if (!_isOnCooldown) 
		{
			if (_thalmicMyo.pose == Thalmic.Myo.Pose.WaveOut) 
			{
				ShootBullets ();
				StartCoroutine ("ShotCooldown");
//				Master.eventManager.CommandRoam ();
			}
		}

//		if (Master.gestureController.IsHorizontalSwipePowerLeft) 
//		{
//			ShootBullets ();
//		}
	}

	void ShootBullets ()
	{
		if (!Master.pickupManager.isBulletsEmpty) 
		{
			for (int i = 0; i < bulletSpawnPoints.Count; i++) 
			{
				for (int j = 0; j < _bulletPool.Count; j++) 
				{
					if (!_bulletPool[j].activeInHierarchy) 
					{
						Master.pickupManager.RemoveBullet ();
						_bulletPool[j].transform.position = bulletSpawnPoints[i].transform.position;
						_bulletPool[j].transform.rotation = bulletSpawnPoints[i].transform.rotation;
						_bulletPool[j].SetActive (true);
						_bulletPool[j].GetComponent<Rigidbody> ().AddForce (bulletSpawnPoints[i].transform.forward * _force);
						break;
					}
				}
			}
		}

		PlayShotSound ();
	}

	void PlayShotSound ()
	{
		_shotSoundSource.Stop ();
		_shotSoundSource.clip = shotSound;
		_shotSoundSource.Play ();
	}

	IEnumerator ShotCooldown ()
	{
		_isOnCooldown = true;
		yield return new WaitForSeconds (_cooldownTime);
		_isOnCooldown = false;
	}
}