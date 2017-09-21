using UnityEngine;
using System.Collections;
using Leap;

[DisallowMultipleComponent]
public class GestureController : MonoBehaviour
{
	private float _minZDistance = 2.0f;
	public float minZDistance 
	{
		get
		{
			return _minZDistance;
		}
		set
		{
			_minZDistance = value;
		}
	}
	private float _forwardTrigger = 10.0f;
	public float forwardTrigger 
	{
		get
		{
			return _forwardTrigger;
		}
		set
		{
			_forwardTrigger = value;
		}
	}

	private float _minHorizontalDistance = 2.0f;
	public float minHorizontalDistance 
	{
		get
		{
			return _minHorizontalDistance;
		}
		set
		{
			_minHorizontalDistance = value;
		}
	}
	private float _horizontalTrigger = 10.0f;
	public float horizontalTrigger 
	{
		get
		{
			return _horizontalTrigger;
		}
		set
		{
			_horizontalTrigger = value;
		}
	}

	private float _minVerticalDistance = 3.0f;
	public float minVerticalDistance 
	{
		get
		{
			return _minVerticalDistance;
		}
		set
		{
			_minVerticalDistance = value;
		}
	}
	private float _verticalTrigger = 10.0f;
	public float verticalTrigger 
	{
		get
		{
			return _verticalTrigger;
		}
		set
		{
			_verticalTrigger = value;
		}
	}

	//SCREEN TAP
	private int _leftHandMotionForward 		= 0;
	private int _rightHandMotionForward 	= 0;
	//SWIPE HORIZONTAL
	private int _leftHandMotionLeft		= 0;
	private int _leftHandMotionRight	= 0;
	private int _rightHandMotionLeft	= 0;
	private int _rightHandMotionRight	= 0;
	//SWIPE VERTICAL
	private int _leftHandMotionUp 		= 0;
	private int _leftHandMotionDown 	= 0;
	private int _rightHandMotionUp		= 0;
	private int _rightHandMotionDown 	= 0;
	
	private float _gestureCooldownTime = 1.5f;
	private bool _isGestureOnCooldown = false;

	private Controller _leapController;

	private Frame _currentFrame;
	private Frame _previousFrame;

	private HandList _handsInCurrentFrame;
	private HandList _handsInPreviousFrame;
	
	private Hand _leftHand;
	public Hand leftHand 
	{
		get
		{
			if (_leftHand != null)
			{
				return _leftHand;
			}
			else
			{
				return Hand.Invalid;
			}
		}
	}
	private Hand _rightHand;
	public Hand rightHand 
	{
		get
		{
			if (_rightHand != null) 
			{
				return _rightHand;				
			}
			else
			{
				return Hand.Invalid;
			}
		}
	}

	private bool _isLeftHandClenched = false;
	public bool isLeftHandClenched
	{
		get
		{
			return _isLeftHandClenched;
		}
	}

	private bool _isRightHandClenched = false;
	public bool isRightHandClenched
	{
		get
		{
			return _isRightHandClenched;
		}
	}

	private bool _isLeftHandScreenTap = false;
	public bool isLeftHandScreenTap
	{
		get
		{
			return _isLeftHandScreenTap;
		}
	}

	private bool _isRightHandScreenTap = false;
	public bool isRightHandScreenTap
	{
		get
		{
			return _isRightHandScreenTap;
		}
	}

	private bool _isScreenTap;
	public bool isScreenTap
	{
		get
		{
			return _isScreenTap;
		}
	}

	private bool _isLeftHandSwipeLeft = false;
	public bool isLeftHandSwipeLeft
	{
		get
		{
			return _isLeftHandSwipeLeft;
		}
	}

	private bool _isLeftHandSwipeHorizontal = false;
	public bool isLeftHandSwipeHorizontal
	{
		get
		{
			return _isLeftHandSwipeHorizontal;
		}
	}

	private bool _isRightHandSwipeRight = false;
	public bool isRightHandSwipeRight
	{
		get
		{
			return _isRightHandSwipeRight;
		}
	}

	private bool _isRightHandSwipeHorizontal = false;
	public bool isRightHandSwipeHorizontal
	{
		get
		{
			return _isRightHandSwipeHorizontal;
		}
	}

	private bool _isSwipeHorizontal;
	public bool isSwipeHorizontal
	{
		get
		{
			return _isSwipeHorizontal;
		}
	}

	private bool _isLeftHandSwipeDown = false;
	public bool isLeftHandSwipeDown
	{
		get
		{
			return _isLeftHandSwipeDown;
		}
	}

	private bool _isLeftHandSwipeVertical = false;
	public bool isLeftHandSwipeVertical
	{
		get
		{
			return _isLeftHandSwipeVertical;
		}
	}

	private bool _isRightHandSwipeDown = false;
	public bool isRightHandSwipeDown
	{
		get
		{
			return _isRightHandSwipeDown;
		}
	}

	private bool _isRightHandSwipeVertical = false;
	public bool isRightHandSwipeVertical
	{
		get
		{
			return _isRightHandSwipeVertical;
		}
	}

	private bool _isSwipeVertical;
	public bool isSwipeVertical
	{
		get
		{
			return _isSwipeVertical;
		}
	}
	
	public bool IsAnyHandClenched
	{
		get
		{
			return _isLeftHandClenched || _isRightHandClenched;
		}
	}
	
	public bool BothHandsUp
	{
		get
		{
			return _handsInCurrentFrame.Count == 2;
		}
	}
	
	public bool IsLeftFistPower
	{
		get
		{
			if (BothHandsUp && (_leftHand.GrabStrength == 1))
			{
				return true;
			}
			else
			{
				return false;
			}
		}
	}
	
	public bool IsRightFistPower
	{
		get
		{
			if (BothHandsUp && (_rightHand.GrabStrength == 1))
			{
				return true;
			}
			else
			{
				return false;
			}
		}
	}
	
	public bool IsScreenTapPowerLeft
	{
		get
		{
			if (IsLeftFistPower && isRightHandScreenTap) 
			{
				return true;
			}
			else
			{
				return false;
			}
		}
	}

	public bool IsScreenTapPowerRight
	{
		get
		{
			if (IsRightFistPower && isLeftHandScreenTap)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
	}

	public bool IsHorizontalSwipePowerLeft
	{
		get
		{
			if (IsLeftFistPower && isRightHandSwipeHorizontal)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
	}

	public bool IsHorizontalSwipePowerRight
	{
		get
		{
			if (IsRightFistPower && isLeftHandSwipeHorizontal) 
			{
				return true;
			}
			else
			{
				return false;
			}
		}
	}

	public bool IsVerticalSwipePowerLeft
	{
		get
		{
			if (IsLeftFistPower && isRightHandSwipeDown) 
			{
				return true;
			}
			else
			{
				return false;
			}
		}
	}

	public bool IsVerticalSwipePowerRight
	{
		get
		{
			if (IsRightFistPower && isLeftHandSwipeDown) 
			{
				return true;
			}
			else
			{
				return false;
			}
		}
	}

	public bool IsGestureRecognized
	{
		get
		{
			if (_isScreenTap || _isSwipeHorizontal || _isSwipeVertical) 
			{
				return true;
			}
			else
			{
				return false;
			}
		}
	}

	void Awake ()
	{
		_leapController = new Controller ();
		_currentFrame = new Frame ();
		//TODO: nullreference???
		_handsInCurrentFrame = new HandList ();
		_handsInPreviousFrame = new HandList ();
	}

	void Update ()
	{
		SetupLeapFrame ();
		SetupHands ();
		CheckHandsClenched ();
		CheckForGestures ();
		
		Debugging ();
	}

	#region setup
	void SetupLeapFrame ()
	{	
		_previousFrame = _currentFrame;
		_currentFrame = _leapController.Frame ();

		_handsInCurrentFrame = new HandList ();
		_handsInPreviousFrame = new HandList ();

		if (!_currentFrame.Hands.IsEmpty)
		{
			_handsInCurrentFrame = _currentFrame.Hands;
			_handsInPreviousFrame = _previousFrame.Hands;
		}
	}

	void SetupHands ()
	{
		ResetHands ();

		if (!_handsInCurrentFrame.IsEmpty)
		{
			for (int i = 0; i < _handsInCurrentFrame.Count; i++) 
			{
				if (_handsInCurrentFrame[i].IsLeft)
				{
					_leftHand = _handsInCurrentFrame[i];
				} 
				else if (_handsInCurrentFrame[i].IsRight)
				{
					_rightHand = _handsInCurrentFrame[i];
				}
			}
		}
	}
	#endregion

	void ResetHands ()
	{
		_leftHand = null;
		_rightHand = null;
	}

	void CheckHandsClenched ()
	{
		if (!_handsInCurrentFrame.IsEmpty) 
		{
			for (int i = 0; i < _handsInCurrentFrame.Count; i++) 
			{
				if (_handsInCurrentFrame[i].IsLeft) 
				{
					if (_handsInCurrentFrame[i].GrabStrength == 1)
					{
						_isLeftHandClenched = true;
					}
					else
					{
						_isLeftHandClenched = false;
					}
				}

				if (_handsInCurrentFrame[i].IsRight) 
				{
					if (_handsInCurrentFrame[i].GrabStrength == 1) 
					{
						_isRightHandClenched = true;
					}
					else
					{
						_isRightHandClenched = false;
					}
				}
			}
		}
		else
		{
			_isLeftHandClenched = false;
			_isRightHandClenched = false;
		}
	}

	void CheckForGestures ()
	{
		ResetGestures ();

		if (!_isGestureOnCooldown) 
		{
			CheckForScreenTap ();
			CheckForSwipe ();
		}

		StartGestureCooldown ();
	}

	void StartGestureCooldown ()
	{
		if (IsGestureRecognized) 
		{
			StartCoroutine ("GestureCooldown");
		}
	}

	void CheckForScreenTap ()
	{
		CheckHandForScreenTap (_rightHand);
		CheckHandForScreenTap (_leftHand);
		_isScreenTap = _isRightHandScreenTap || _isLeftHandScreenTap;
	}
	
	void CheckForSwipe ()
	{
		CheckHandForSwipeHorizontal (_leftHand);
		CheckHandForSwipeHorizontal (_rightHand);
		_isSwipeHorizontal = _isLeftHandSwipeHorizontal || _isRightHandSwipeHorizontal;
		
		CheckHandForSwipeVertical (_leftHand);
		CheckHandForSwipeVertical (_rightHand);
		_isSwipeVertical = _isLeftHandSwipeVertical || _isRightHandSwipeVertical;
	}
	
	void ResetGestures ()
	{
		_isScreenTap = false;
		_isSwipeHorizontal = false;
		_isSwipeVertical = false;

		_isLeftHandScreenTap = false;
		_isRightHandScreenTap = false;

		_isLeftHandSwipeLeft = false;
		_isRightHandSwipeRight = false;
		_isLeftHandSwipeHorizontal = false;
		_isRightHandSwipeHorizontal = false;

		_isLeftHandSwipeDown = false;
		_isRightHandSwipeDown = false;
		_isLeftHandSwipeVertical = false;
		_isRightHandSwipeVertical = false;
	}
	
	public bool IsNewHand ()
	{
		for (int i = 0; i < _handsInCurrentFrame.Count; i++) 
		{
			if (_handsInPreviousFrame[i].Id != _handsInCurrentFrame[i].Id)
			{
				return true;
			}
		}
		return false;
	}

	#region gestures
	void CheckHandForScreenTap (Hand hand)
	{
		if (hand != null)
		{
			Vector motionSincePreviousFrame = hand.Translation (_previousFrame);

			bool motionForward = motionSincePreviousFrame.z < -_minZDistance;

			if (motionForward)
			{
				if (hand.IsLeft) 
				{
					_leftHandMotionForward++;
				}
				else if (hand.IsRight)
				{
					_rightHandMotionForward++;
				}
				else
				{
					Debug.LogError ("Wtf is this hand.");
				}
			}
			else
			{
				if (hand.IsLeft) 
				{
					_leftHandMotionForward = 0;
				}

				if (hand.IsRight)
				{
					_rightHandMotionForward = 0;
				}
			}

			if (hand.IsLeft)
			{
				if (_leftHandMotionForward > _forwardTrigger)
				{
					_isLeftHandScreenTap = true;
					_leftHandMotionForward = 0;
				}
			}
			
			if (hand.IsRight)
			{
				if (_rightHandMotionForward > _forwardTrigger) 
				{
					_isRightHandScreenTap = true;
					_rightHandMotionForward = 0;
				}
			}
		}
	}

	void CheckHandForSwipeHorizontal (Hand hand)
	{
		if (hand != null) 
		{
			Vector motionSincePreviousFrame = hand.Translation (_previousFrame);

			bool motionRight = motionSincePreviousFrame.x > _minHorizontalDistance;
			bool motionLeft = motionSincePreviousFrame.x < -_minHorizontalDistance;

			if (motionRight) //SWIPE RIGHT
			{
				if (hand.IsLeft)
				{
					_leftHandMotionRight++;
				}
				else if (hand.IsRight)
				{
					_rightHandMotionRight++;
				}
				else
				{
					Debug.LogError ("Wtf is this hand.");
				}
			}
			else if (motionLeft) //SWIPE LEFT
			{
				if (hand.IsLeft)
				{
					_leftHandMotionLeft++;
				}
				else if (hand.IsRight)
				{
					_rightHandMotionLeft++;
				}
				else
				{
					Debug.LogError ("Wtf is this hand.");
				}
			}
			else
			{
				if (hand.IsLeft) 
				{
					_leftHandMotionLeft = 0;
					_leftHandMotionRight = 0;
				}

				if (hand.IsRight)
				{
					_rightHandMotionLeft = 0;
					_rightHandMotionRight = 0;
				}
			}

			if (hand.IsLeft)
			{
				if (_leftHandMotionLeft > _horizontalTrigger) 
				{
					_isLeftHandSwipeLeft = true;
					_leftHandMotionLeft = 0;
					_leftHandMotionRight = 0;
				}
				//left hand any horizontal swipe direction
				if (_leftHandMotionLeft > _horizontalTrigger || _leftHandMotionRight > _horizontalTrigger)
				{
					_isLeftHandSwipeHorizontal = true;
					_leftHandMotionLeft = 0;
					_leftHandMotionRight = 0;
				}
			}

			if (hand.IsRight)
			{
				if (_rightHandMotionRight > _horizontalTrigger) 
				{
					_isRightHandSwipeRight = true;
					_rightHandMotionLeft = 0;
					_rightHandMotionRight = 0;
				}
				//right hand any horizontal swipe direction
				if (_rightHandMotionLeft > _horizontalTrigger || _rightHandMotionRight > _horizontalTrigger) 
				{
					_isRightHandSwipeHorizontal = true;
					_rightHandMotionLeft = 0;
					_rightHandMotionRight = 0;
				}
			}
		}
	}

	void CheckHandForSwipeVertical (Hand hand)
	{
		if (hand != null) 
		{
			Vector motionSincePreviousFrame = hand.Translation (_previousFrame);
			
			bool motionUp = motionSincePreviousFrame.y > _minHorizontalDistance;
			bool motionDown = motionSincePreviousFrame.y < -_minHorizontalDistance;
			
			if (motionUp) //SWIPE UP
			{
				if (hand.IsLeft)
				{
					_leftHandMotionUp++;
				}
				else if (hand.IsRight)
				{
					_rightHandMotionUp++;
				}
				else
				{
					Debug.LogError ("Wtf is this hand.");
				}
			}
			else if (motionDown) //SWIPE DOWN
			{
				if (hand.IsLeft)
				{
					_leftHandMotionDown++;
				}
				else if (hand.IsRight)
				{
					_rightHandMotionDown++;
				}
				else
				{
					Debug.LogError ("Wtf is this hand.");
				}
			}
			else
			{
				if (hand.IsLeft) 
				{
					_leftHandMotionUp = 0;
					_leftHandMotionDown = 0;
				}
				
				if (hand.IsRight)
				{
					_rightHandMotionUp = 0;
					_rightHandMotionDown = 0;
				}
			}
			
			if (hand.IsLeft)
			{
				if (_leftHandMotionDown > _verticalTrigger) 
				{
					_isLeftHandSwipeDown = true;
					_leftHandMotionUp = 0;
					_leftHandMotionDown = 0;
				}
				//left hand any vertical swipe direction
				if (_leftHandMotionUp > _verticalTrigger || _leftHandMotionDown > _verticalTrigger)
				{
					_isLeftHandSwipeVertical = true;
					_leftHandMotionUp = 0;
					_leftHandMotionDown = 0;
				}
			}
			
			if (hand.IsRight)
			{
				if (_rightHandMotionDown > _verticalTrigger) 
				{
					_isRightHandSwipeDown = true;
					_rightHandMotionUp = 0;
					_rightHandMotionDown = 0;

				}
				//right hand any vertical swipe direction
				if (_rightHandMotionUp > _verticalTrigger || _rightHandMotionDown > _verticalTrigger) 
				{
					_isRightHandSwipeVertical = true;
					_rightHandMotionUp = 0;
					_rightHandMotionDown = 0;
				}
			}
		}
	}
	#endregion
	

	void Debugging ()
	{
//		Debug.Log ("forward: " + _forwardTrigger);
//		Debug.Log ("horizontal: " + _horizontalTrigger);
//		Debug.Log ("vertical: " + _verticalTrigger);
//
//		Debug.Log ("min fwd: " + _minZDistance);
//		Debug.Log ("min hrz: " + _minHorizontalDistance);
//		Debug.Log ("min vrt: " + _minVerticalDistance);

//		if (IsLeftFistPower) 				Debug.Log ("left hand clenched");
//
//		if (IsRightFistPower) 				Debug.Log ("right hand clenched");
//
//		if (_isScreenTap) 					Debug.Log ("screen tap");
//
//		if (_isSwipeHorizontal) 			Debug.Log ("swipe horizontal");
//		
//		if (_isSwipeVertical) 				Debug.Log ("swipe vertical");
//		
//		if (IsGestureRecognized)			Debug.Log ("used gesture");
//		
//		if (IsScreenTapPowerLeft) 			Debug.Log ("left power tap");
//
//		if (IsScreenTapPowerRight) 			Debug.Log ("right power tap");
//
//		if (IsHorizontalSwipePowerLeft) 	Debug.Log ("left power swipe horizontal");
//
//		if (IsHorizontalSwipePowerRight) 	Debug.Log ("right power swipe horizontal");
//
//		if (IsVerticalSwipePowerLeft) 		Debug.Log ("left power swipe vertical");
//
//		if (IsVerticalSwipePowerRight) 		Debug.Log ("right power swipe vertical");
	}

	IEnumerator GestureCooldown ()
	{
		if (!_isGestureOnCooldown)
		{
			_isGestureOnCooldown = true;
			yield return new WaitForSeconds (_gestureCooldownTime);
			_isGestureOnCooldown = false;
		}
	}
}