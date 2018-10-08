using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PlayerController
{
    [SerializeField] ButtonController buttonUp;
    [SerializeField] ButtonController buttonDown;
	[SerializeField] ButtonController buttonLeft;
	[SerializeField] ButtonController buttonRight;
	bool jumped;
    void SetInput()
    {
        buttonUp.onPress = OnPressFront;
        buttonDown.onPress = OnPressBack;
		buttonLeft.onPress = OnPressLeft;
		buttonRight.onPress = OnPressRight;
    }
    public void OnPressFront()
    {
        moveDirection.z += Time.deltaTime * runSpeed;
    }
    public void OnPressBack()
    {
        moveDirection.z -= Time.deltaTime * runSpeed;
    }
    public void OnPressLeft()
    {
        groundRoot.transform.Rotate(0, 0, Time.deltaTime * groundRotateSpeed);
    }
    public void OnPressRight()
    {
        groundRoot.transform.Rotate(0, 0, -Time.deltaTime * groundRotateSpeed);
    }
    public void OnClickJump()
    {
        if (isTouchedGround)
            jumped = true;
    }

}
