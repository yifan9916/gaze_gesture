//using UnityEngine;
//using System.Collections;
//
//public class PlayerAwareness : MonoBehaviour
//{
//	private bool _isAware = false;
//	public bool isAware
//	{
//		get
//		{
//			return _isAware;
//		}
//	}
//
//	void OnTriggerEnter (Collider other)
//	{
//		if (Master.eventManager.IsFollowCommand && other.CompareTag (C.TAG_FOLLOW_POINT)) 
//		{
//			_isAware = true;
//			Master.playerController.SetPlayerToWaiting ();
//		}
//
//		if (!Master.eventManager.IsFollowCommand && other.CompareTag (C.TAG_FOLLOW_POINT)) 
//		{
//			Master.playerController.SetPlayerToIdle ();
//		}
//	}
//
//	void OnTriggerStay (Collider other)
//	{
//		//won't work??
//	}
//
//	void OnTriggerExit (Collider other)
//	{
//		if (other.CompareTag (C.TAG_FOLLOW_POINT)) 
//		{
//			_isAware = false;
//		}
//	}
//}