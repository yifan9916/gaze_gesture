using UnityEngine;
using System.Collections;

public abstract class Enemy : MonoBehaviour
{
	private float _moveSpeed;
	public float moveSpeed
	{
		get
		{
			return _moveSpeed;
		}

		set
		{
			_moveSpeed = value;
		}
	}

	private int _hitPoints;
	public int hitPoints
	{
		get
		{
			return _hitPoints;
		}

		set
		{
			_hitPoints = value;
		}
	}
}
