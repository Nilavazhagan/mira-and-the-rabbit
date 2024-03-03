using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScaler : MonoBehaviour
{

    //public float referenceHeight;
    //public float referenceWidth;
    //public float referenceSize;

    float screenHeightKnown, screenWidthKnown;

    public SpriteRenderer background;

    [SerializeField]
    private ScalingMode scalingMode;
    // Start is called before the first frame update
    void Start()
    {
        AdjustCameraSize();
    }

    private void LateUpdate()
    {
        if(!Mathf.Approximately(screenHeightKnown, Screen.height) || !Mathf.Approximately(screenWidthKnown, Screen.width))
        {
            AdjustCameraSize();
        }
    }

    void AdjustCameraSize() {
        screenHeightKnown = Screen.height;
        screenWidthKnown = Screen.width;
        float cameraSize = background.bounds.size.x * screenHeightKnown / screenWidthKnown * 0.5f;

        if(cameraSize < background.bounds.size.y / 2f)
        {
            cameraSize = background.bounds.size.y / 2;
        }

        Camera.main.orthographicSize = cameraSize;
    }


    enum ScalingMode {
        Width,
        Height
    }
}
