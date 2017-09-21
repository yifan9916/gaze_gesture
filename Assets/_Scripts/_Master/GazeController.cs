using UnityEngine;
using System.Collections;
using iView;

[DisallowMultipleComponent]
public class GazeController : MonoBehaviour
{
    private SampleData _gazeSample;

    private GameObject _focusedObject;

    private Vector3 _avgGazePosition;
    public Vector3 avgGazePosition 
    { 
        get
        {
            return _avgGazePosition;
        }
    }

    void Update ()
    {
        SetupGaze ();
    }

    void SetupGaze ()
    {
        _gazeSample = SMIGazeController.Instance.GetSample ();
        _avgGazePosition = _gazeSample.averagedEye.gazePosInUnityScreenCoords ();
    }
}