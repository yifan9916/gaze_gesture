//using UnityEngine;
//using System.Collections;
//
//public class PlayerSight : MonoBehaviour
//{
//	private GameObject _player;
//	private PlayerAwareness _awareness;
//
//	void Awake ()
//	{
//		_player = GameObject.FindGameObjectWithTag (C.TAG_PLAYER);
//		_awareness = _player.transform.parent.GetComponentInChildren<PlayerAwareness> ();
//	}
//
//	void OnTriggerEnter (Collider other)
//	{
//		if (other.CompareTag (C.TAG_FOLLOW_POINT)) 
//		{
//			Master.playerController.SetPlayerToFollow ();
//		}
//	}
//
//	void OnTriggerStay (Collider other)
//	{
//		//won't work??
//	}
//	//distance between transform pos and dot pos
//	void OnTriggerExit (Collider other)
//	{
//		if (other.CompareTag (C.TAG_FOLLOW_POINT))
//		{
//			if (Master.eventManager.IsFollowCommand && !_awareness.isAware)
//			{
//				Master.eventManager.CommandRoam ();
//			}
//		}
//	}
//}