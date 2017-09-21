using UnityEngine;

static class Master
{
	public static GameController gameController;
	public static LevelLoader levelLoader;
	public static PlayerController playerController;
	public static CameraController cameraController;
	public static EventManager eventManager;
	public static PickupManager pickupManager;
	public static GestureController gestureController;
    public static GazeController gazeController;
	public static ColorManager colorManager;
	public static AudioManager audioManager;
	public static MyoGesture myoGesture;

//	static Master ()
//	{
//		GameObject g;
//
//		g = GetObjectByTag (C.TAG_GAME_CONTROLLER);
//		gameController = (GameController)GetObjectComponent (g, "GameController");
//
//		g = GetObjectByTag (C.TAG_BAD_MOTHERFUCKER);
//		playerController = (PlayerController)GetObjectComponent (g, "PlayerController");
//
//		g = GetObjectByTag (C.TAG_MAIN_CAMERA);
//		cameraController = (CameraController)GetObjectComponent (g, "CameraController");
//
//		g = GetObjectByTag (C.TAG_WAYPOINT_MANAGER);
//		waypointManager = (WaypointManager)GetObjectComponent (g, "WaypointManager");
//
//		g = GetObjectByTag (C.TAG_EVENT_MANAGER);
//		eventManager = (EventManager)GetObjectComponent (g, "EventManager");
//
//		g = GetObjectByTag (C.TAG_PICKUP_MANAGER);
//		pickupManager = (PickupManager)GetObjectComponent (g, "PickupManager");
//
//		g = GetObjectByTag (C.TAG_GESTURE_CONTROLLER);
//		gestureController = (GestureController)GetObjectComponent (g, "GestureController");
//
//        g = GetObjectByTag (C.TAG_GAZE_CONTROLLER);
//        gazeController = (GazeController)GetObjectComponent (g, "GazeController");
//
//		g = GetObjectByTag (C.TAG_COLOR_MANAGER);
//		colorManager = (ColorManager)GetObjectComponent (g, "ColorManager");
//		
//		g = GetObjectByTag (C.TAG_AUDIO_MANAGER);
//		audioManager = (AudioManager)GetObjectComponent (g, "AudioManager");
//
//		g = GetObjectByTag (C.TAG_DATA_DISPLAY);
//		dataDisplay = (DataDisplay)GetObjectComponent (g, "DataDisplay");
//	}

	public static void SetupComponents ()
	{
		GameObject g;
		
		g = GetObjectByTag (C.TAG_GAME_CONTROLLER);
		gameController = (GameController)GetObjectComponent (g, "GameController");

		g = GetObjectByTag (C.TAG_LEVEL_LOADER);
		levelLoader = (LevelLoader)GetObjectComponent (g, "LevelLoader");
		
		g = GetObjectByTag (C.TAG_PLAYER_CONTROLLER);
		playerController = (PlayerController)GetObjectComponent (g, "PlayerController");
		
		g = GetObjectByTag (C.TAG_MAIN_CAMERA);
		cameraController = (CameraController)GetObjectComponent (g, "CameraController");

		g = GetObjectByTag (C.TAG_EVENT_MANAGER);
		eventManager = (EventManager)GetObjectComponent (g, "EventManager");
		
		g = GetObjectByTag (C.TAG_PICKUP_MANAGER);
		pickupManager = (PickupManager)GetObjectComponent (g, "PickupManager");
		
		g = GetObjectByTag (C.TAG_GESTURE_CONTROLLER);
		gestureController = (GestureController)GetObjectComponent (g, "GestureController");
		
		g = GetObjectByTag (C.TAG_GAZE_CONTROLLER);
		gazeController = (GazeController)GetObjectComponent (g, "GazeController");
		
		g = GetObjectByTag (C.TAG_COLOR_MANAGER);
		colorManager = (ColorManager)GetObjectComponent (g, "ColorManager");
		
		g = GetObjectByTag (C.TAG_AUDIO_MANAGER);
		audioManager = (AudioManager)GetObjectComponent (g, "AudioManager");

		g = GetObjectByTag (C.TAG_MYO_GESTURE);
		myoGesture = (MyoGesture)GetObjectComponent (g, "MyoGesture");
	}

    #region get stuff
	private static GameObject GetObjectByTag (string objTag)
	{
		GameObject obj = GameObject.FindGameObjectWithTag (objTag);

		if (obj == null) 
		{
			Debug.Log (objTag + "GameObject cannot be found.");
		}

		return obj;
	}

	private static Component GetObjectComponent (GameObject obj, string component)
	{
		Component c = obj.GetComponent (component);

		if (c == null) 
		{
			Debug.Log (component + " Component cannot be found.");
		}

		return c;
	}
    #endregion
}