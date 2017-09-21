using UnityEngine;

public static class Selection
{
	public enum SelectionMethod
	{
		Mouse,
		Gaze
	}

	private static SelectionMethod _current = SelectionMethod.Mouse;
	public static SelectionMethod currentSelectionMethod
	{
		get
		{
			return _current;
		}
	}
	
	private static int waypointLayer = 1 << C.LAYER_WAYPOINT;

	public static Vector3 GetCursorPosition ()
	{
		switch (_current) 
		{
			case SelectionMethod.Mouse: 
				return Input.mousePosition;
            case SelectionMethod.Gaze:
                return Master.gazeController.avgGazePosition;
			default:
				return Input.mousePosition;
		}
	}

	public static Vector3 GetTargetHitPosition ()
	{
		Ray ray = Camera.main.ScreenPointToRay (GetCursorPosition ());
		
		RaycastHit hit;
		
		if (Physics.Raycast (ray, out hit, Mathf.Infinity, waypointLayer)) 
		{
			return hit.point;
		}

		return Vector3.zero;
	}

	public static void SetSelectionMethod (Selection.SelectionMethod method)
	{
		_current = method;
	}
}