using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Camera playerCamera;
    [SerializeField] Vector3 playerMoveCameraDiff = new Vector3(0, 2f, -0.5f);
    [SerializeField] Vector3 playerTargetCameraDiff = new Vector3(0, 0, 5f);
    [Range(0.1f, 10f)] public float playerCameraFollowSpeed = 5f;
    [SerializeField] Rigidbody playerRigidbody;
    [Range(0.1f, 5f)] public float baseSpeed = 2f;
    [Range(0.1f, 2000f)] public float jumpPower = 100f;
    [SerializeField] GameObject groundRoot;
    Vector3 runDirection = Vector3.forward;
    void Update()
    {
        if (IsPlayerFallDown())
        {
            GameObject.Destroy(this.gameObject);
        }

        var move = runDirection * baseSpeed * Time.deltaTime;
        var currentPos = transform.position;
        transform.rotation = Quaternion.Euler(0, 0, 0);
        LocalMove(currentPos + move);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerRigidbody.AddForce(new Vector3(0, jumpPower, 0));
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            groundRoot.transform.Rotate(0, 0, Time.deltaTime * 10f);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            groundRoot.transform.Rotate(0, 0, -Time.deltaTime * 10f);
        }
    }
    void FixedUpdate()
    {
        UpdateCamera();
    }
    void LocalMove(Vector3 pos)
    {
        pos.x = 0;
        transform.position = pos;
    }
    void UpdateCamera()
    {
        if (playerCamera == null)
        {
            return;
        }

        var cameraMovePos = transform.position + playerMoveCameraDiff;
        //playerCamera.transform.position = cameraMovePos;
        playerCamera.transform.position = Vector3.Lerp(playerCamera.transform.position, cameraMovePos, 0.124f);
        var targetPos = transform.position + playerTargetCameraDiff;
        playerCamera.transform.LookAt(targetPos);
    }

    bool IsPlayerFallDown()
    {
        return transform.position.y < -100f;
    }
}
