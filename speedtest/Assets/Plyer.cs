using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plyer : MonoBehaviour
{
    [SerializeField] Vector3 diff;
    [SerializeField] Transform cameraT;
    [SerializeField] Vector3 moveDir;
    [SerializeField] float moveSpeed;

    // Use this for initialization
    void Start()
    {
        //Application.targetFrameRate = 120;
    }

    // Update is called once per frame
    void Update()
    {
        var pos = transform.position;
        var unscel = Time.fixedUnscaledDeltaTime;
        moveDir.z = Time.deltaTime * moveSpeed;
        Debug.Log(Time.deltaTime);
        pos += moveDir;
        transform.position = pos;
    }
    /// <summary>
    /// LateUpdate is called every frame, if the Behaviour is enabled.
    /// It is called after all Update functions have been called.
    /// </summary>
    void LateUpdate()
    {
        cameraT.position = transform.position + diff;
    }
}
