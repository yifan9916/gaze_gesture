using UnityEngine;
using System.Collections;

public class LevelLoader : MonoBehaviour
{
	private const int FIRST_LEVEL = 0;

	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.N)) 
		{
			Debug.Log ("Starting new game");
			LoadNewGame ();
		}

		if (Input.GetKeyDown (KeyCode.S)) 
		{
			Debug.Log ("Reloading level");
			ReloadLevel ();
		}
	}

	public void LoadNewGame ()
	{
		Application.LoadLevel (FIRST_LEVEL);
	}

	public void LoadNextLevel ()
	{
		Application.LoadLevel (Application.loadedLevel + 1);
	}

	public void ReloadLevel ()
	{
		Application.LoadLevel (Application.loadedLevel);
	}
}