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
    [SerializeField] Transform eyeLeft;
    [SerializeField] Transform eyeRight;
    [Range(0.1f, 4f)]
    public float eyeDistance = 0.5f;
    public CameraMode cameraMode;
    [SerializeField] Vector3 followDiff;
    [SerializeField] Vector3 simpleFollowLookTarget;
    [SerializeField] float lerpFollowSpeed;
    [SerializeField] PlayerController player;
    Quaternion startRotation;
    Quaternion currentRotation;
    [SerializeField] Text uiLabel;
    [SerializeField] Gyroscope gyroscope;
    void Start()
    {
        UpdateCameraSettings();


        if (SystemInfo.supportsGyroscope)
        {
            gyroscope = Input.gyro;
            gyroscope.enabled = true;
        }
        else
        {
            Debug.LogError("gyroscope no sup");
        }
    }
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    // void Update()
    // {
    //     var a = Input.acceleration;
    //     var text = string.Format("ACC X : {0:.00}, Y : {1:.00} X :{2:.00} ¥n", a.x, a.y, a.z);
    //     //this.transform.rotation = new Quaternion(a.x, a.y, a.z, 0);
    //     uiLabel.text = text;
    // }
    void UpdateCameraSettings()
    {
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        currentRotation = Input.gyro.attitude;
        uiLabel.text = string.Format("Gyro x{0:0.000},y{1:0.000},z{2:0.000}", -Input.gyro.rotationRate.x, -Input.gyro.rotationRate.y, -Input.gyro.rotationRate.z);

        if (eyeLeft != null)
        {
            if (Input.GetKey(KeyCode.O))
            {
                eyeDistance += 0.01f;
            }
            else if (Input.GetKey(KeyCode.P))
            {
                eyeDistance -= 0.01f;
            }

            eyeDistance = Mathf.Min(0, eyeDistance);
            if ((-eyeLeft.localPosition.x != eyeDistance / 2f) || (eyeRight.localPosition.x != eyeDistance / 2f))
            {
                var localLF = eyeLeft.localPosition;
                localLF.x = -eyeDistance / 2f;
                eyeLeft.localPosition = localLF;

                var localRI = eyeRight.localPosition;
                localRI.x = eyeDistance / 2f;
                eyeRight.localPosition = localRI;
            }
        }

        switch (cameraMode)
        {
            case CameraMode.SimpleFollow:
                transform.position = player.transform.position + followDiff;
                //transform.Rotate(-Input.gyro.rotationRateUnbiased.x, -Input.gyro.rotationRateUnbiased.y, -Input.gyro.rotationRateUnbiased.z);
                //transform.Rotate(-Input.gyro.rotationRate.x, -Input.gyro.rotationRate.y, -Input.gyro.rotationRate.z);
                //transform.rotation = new Quaternion(-Input.gyro.userAcceleration.x, -Input.gyro.userAcceleration.y, -Input.gyro.userAcceleration.z, 1);
                // var lookAtTarget = player.transform.position + simpleFollowLookTarget;
                // transform.LookAt(lookAtTarget);
                Quaternion direction = Input.gyro.attitude;
                transform.rotation = Quaternion.Euler(90, 0, 0) * (new Quaternion(-direction.x, -direction.y, direction.z, direction.w));
                break;
            case CameraMode.LerpFollow:
                break;
            case CameraMode.VRModePC:
                break;
            case CameraMode.VRModeDev:
                break;
        }
    }
}
