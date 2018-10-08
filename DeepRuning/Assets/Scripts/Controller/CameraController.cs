using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    public enum CameraMode
    {
        SimpleFollow,
        LerpFollow,
        VRModePC,
        VRModeDev,
    };
    public CameraMode cameraMode;
    [SerializeField] Vector3 followDiff;
    [SerializeField] Vector3 simpleFollowLookTarget;
    [SerializeField] float lerpFollowSpeed;
    [SerializeField] PlayerController player;
    Quaternion startRotation;
    Quaternion currentRotation;
    [SerializeField] Text uiLabel;
    void Start()
    {
        UpdateCameraSettings();
    }
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        var a = Input.acceleration;
        this.transform.rotation = new Quaternion(a.x, a.y, a.z, 0);
    }
    void UpdateCameraSettings()
    {
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    // void Update()
    // {
    //     currentRotation = Input.gyro.attitude;
    //     uiLabel.text = string.Format("Gyro x{0:0.000},y{1:0.000},z{2:0.000}", currentRotation.x, currentRotation.y, currentRotation.z);

    //     switch (cameraMode)
    //     {
    //         case CameraMode.SimpleFollow:
    //             transform.position = player.transform.position + followDiff;
    //             var lookAtTarget = player.transform.position + simpleFollowLookTarget;
    //             transform.LookAt(lookAtTarget);
    //             break;
    //         case CameraMode.LerpFollow:
    //             break;
    //         case CameraMode.VRModePC:
    //             break;
    //         case CameraMode.VRModeDev:
    //             break;
    //     }
    // }
}
