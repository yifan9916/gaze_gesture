using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class ProjectileAreaOfEffect : MonoBehaviour
{
	void OnTriggerEnter (Collider other)
	{
		if (other.CompareTag (C.TAG_ENEMY)) 
		{
			other.GetComponentInParent<EnemyController> ().SetToDead ();
		}
	}
}