using UnityEngine;
using System.Collections;

public class MyoGesture : MonoBehaviour 
{
	private ThalmicMyo _thalmicMyo;

	private bool _isFistPose = false;
	public bool isFistPose
	{
		get
		{
			return _isFistPose;
		}
	}
	private bool _isSpreadPose = false;
	public bool isSpreadPose
	{
		get
		{
			return _isSpreadPose;
		}
	}
	private bool _isWaveInPose = false;
	public bool isWaveInPose
	{
		get
		{
			return _isWaveInPose;
		}
	}
	private bool _isWaveOutPose = false;
	public bool isWaveOutPose
	{
		get
		{
			return _isWaveOutPose;
		}
	}

	void Awake ()
	{
		_thalmicMyo = GameObject.FindGameObjectWithTag (C.TAG_MYO).GetComponent<ThalmicMyo> ();
	}

	void Update ()
	{
		ResetPose ();
		GetMyoPose ();
	}

	void GetMyoPose ()
	{
		if (_thalmicMyo.pose == Thalmic.Myo.Pose.Fist) 
		{
			_isFistPose = true;
		}

		if (_thalmicMyo.pose == Thalmic.Myo.Pose.FingersSpread)
		{
			_isSpreadPose = true;
		}

		if (_thalmicMyo.pose == Thalmic.Myo.Pose.WaveIn) 
		{
			_isWaveInPose = true;
		}

		if (_thalmicMyo.pose == Thalmic.Myo.Pose.WaveOut) 
		{
			_isWaveOutPose = true;
		}
	}

	void ResetPose ()
	{
		_isFistPose = false;
		_isSpreadPose = false;
		_isWaveInPose = false;
		_isWaveOutPose = false;
	}
}
