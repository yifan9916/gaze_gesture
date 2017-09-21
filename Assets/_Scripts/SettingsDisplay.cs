using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SettingsDisplay : MonoBehaviour 
{
	private const float MIN_VALUE = 0.0f;
	private const float MAX_VALUE = 20.0f;

	private GameObject _canvas;

	public Slider _zSlider;
	public Slider _horizontalSlider;
	public Slider _verticalSlider;

	public Slider _minFwdSlider;
	public Slider _minHrzSlider;
	public Slider _minVrtSlider;

	public bool isSettings = false;

	void Awake ()
	{
		_canvas = GameObject.FindGameObjectWithTag (C.TAG_MENU);
	}

	void Start ()
	{
		_canvas.SetActive (false);
	}

	void Update ()
	{
		ToggleMenu ();
		UpdateSliders ();
	}

	public void ToggleMenu ()
	{
		if (Input.GetKeyDown (KeyCode.Z))
		{
			_canvas.SetActive (false);
			isSettings = false;
		}
		
		if (Input.GetKeyDown (KeyCode.X))
		{
			_canvas.SetActive (true);
			
			SetSliders ();
			isSettings = true;
		}

//		if (Master.gameController.isPaused) 
//		{
//			_canvas.SetActive (true);
//			SetSliders ();
//		}
//		else
//		{
//			_canvas.SetActive (false);
//		}
	}

	void SetSliders ()
	{
		_zSlider.value 			= Master.gestureController.forwardTrigger;
		_horizontalSlider.value = Master.gestureController.horizontalTrigger;
		_verticalSlider.value 	= Master.gestureController.verticalTrigger;

		_minFwdSlider.value = Master.gestureController.minZDistance;
		_minHrzSlider.value = Master.gestureController.minHorizontalDistance;
		_minVrtSlider.value = Master.gestureController.minVerticalDistance;
	}

	void UpdateSliders ()
	{
		Master.gestureController.forwardTrigger 	= _zSlider.value;
		Master.gestureController.horizontalTrigger 	= _horizontalSlider.value;
		Master.gestureController.verticalTrigger 	= _verticalSlider.value;

		Master.gestureController.minZDistance 			= _minFwdSlider.value;
		Master.gestureController.minHorizontalDistance 	= _minHrzSlider.value;
		Master.gestureController.minVerticalDistance 	= _minVrtSlider.value;
	}
}