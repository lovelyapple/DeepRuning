﻿using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PlayerController : MonoBehaviour
{
    [SerializeField] Transform groundRoot;
    [Range(1f, 30f)] [SerializeField] float groundRotateSpeed;
    [SerializeField] Vector3 moveDirection;
    [SerializeField] float runSpeed;
    [Range(1f, 30f)] [SerializeField] float maxSpeed;
    [SerializeField] Vector3 playerSize;
    [Range(1f, 10f)] [SerializeField] float gravity;
    [Range(1.0f, 2.0f)] [SerializeField] float groundCheckRange;
    [SerializeField] float JumpPower;
    [SerializeField] bool moveManual;
    bool isTouchedGround;

    [SerializeField] LineRenderer[] lines;

    void Start()
    {
        SetInput();
    }
    void Update()
    {
        var currentPos = transform.position;
        currentPos.x = 0;

        if (moveManual)
        {
            UpdateMoveManual();
        }
        else
        {
            moveDirection.z = Time.deltaTime * runSpeed;
        }

        //debug
        if (currentPos.y <= -50f)
        {
            moveDirection.y = 0;
            currentPos.y = 0;
        }

        CheckIsTouchGround((v) => //touched
        {
            moveDirection.y = v.y - transform.position.y + (playerSize.y / 2f);//player size + blocksize
            UpdateJump();
            isTouchedGround = true;
        }, () => //missed
        {
            moveDirection.y -= Time.deltaTime * gravity;
            isTouchedGround = false;
        });

        if (jumped == true)
        {
            jumped = false;
            moveDirection.y = JumpPower;
        }


        currentPos += moveDirection;
        currentPos.x = 0;
        transform.position = currentPos;
        UpdateGround();
    }
    void UpdateMoveManual()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            moveDirection.z += Time.deltaTime * runSpeed;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            moveDirection.z -= Time.deltaTime * runSpeed;
        }
        else
        {
            if (Mathf.Abs(moveDirection.z) < 0.001f)
            {
                moveDirection.z = 0;
            }
            else
            {
                moveDirection.z *= 0.9f;
            }
        }

        if (moveDirection.z != 0)
        {
            moveDirection.z = Mathf.Clamp(moveDirection.z, -maxSpeed, maxSpeed);
        }

    }
    void UpdateJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumped = true;
        }
    }
    void UpdateGround()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            groundRoot.transform.Rotate(0, 0, Time.deltaTime * groundRotateSpeed);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            groundRoot.transform.Rotate(0, 0, -Time.deltaTime * groundRotateSpeed);
        }
    }
    class RayHitClass
    {
        public RaycastHit hit;
        public RayHitClass(RaycastHit hit)
        {
            this.hit = hit;
        }
    }
    RayHitClass CheckGroundSingle(Vector3 diff, float checkRange, int? lineIndex = null)
    {
        var pos = transform.position + diff;
        var lineEndPos = pos + Vector3.down * 5;

        Vector3[] drawPos = new Vector3[2] { pos, lineEndPos };

        if (lineIndex.HasValue)
        {
            try
            {
                lines[lineIndex.Value].SetPositions(drawPos);
            }
            catch
            {

            }
        }

        var hits = Physics.RaycastAll(pos, Vector3.down, checkRange);

        if (hits.Length > 0)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].transform.gameObject.tag == "ground")
                {
                    return new RayHitClass(hits[i]);
                }
            }
        }

        return null;
    }
    void CheckIsTouchGround(Action<Vector3> onHitObj, Action onMissed)
    {
        var posCenter = Vector3.zero;
        var checkRange = -moveDirection.y * groundCheckRange;
        checkRange = Mathf.Clamp(checkRange, playerSize.y / 2f * 1.1f, 100);

        //中心
        var pos = posCenter;
        var hit = CheckGroundSingle(pos, checkRange);

        if (hit != null && onHitObj != null)
        {
            onHitObj(hit.hit.point);
            return;
        }

        //左前
        pos.z = +playerSize.z / 2f;
        pos.x = +playerSize.x / 2f;
        var hitLF = CheckGroundSingle(pos, checkRange, 0);

        if (hitLF != null && onHitObj != null)
        {
            onHitObj(hitLF.hit.point);
            return;
        }

        //右前
        pos.z = +playerSize.z / 2f;
        pos.x = -playerSize.x / 2f;
        var hitRF = CheckGroundSingle(pos, checkRange, 1);

        if (hitRF != null && onHitObj != null)
        {
            onHitObj(hitRF.hit.point);
            return;
        }

        //左後
        pos.z = -playerSize.z / 2f;
        pos.x = -playerSize.x / 2f;
        var hitLB = CheckGroundSingle(pos, checkRange, 2);

        if (hitLB != null && onHitObj != null)
        {
            onHitObj(hitLB.hit.point);
            return;
        }

        //右後
        pos.z = -playerSize.z / 2f;
        pos.x = +playerSize.x / 2f;
        var hitRB = CheckGroundSingle(pos, checkRange, 3);

        if (hitRB != null && onHitObj != null)
        {
            onHitObj(hitRB.hit.point);
            return;
        }


        if (onMissed != null)
        {
            onMissed();
        }
    }
}
