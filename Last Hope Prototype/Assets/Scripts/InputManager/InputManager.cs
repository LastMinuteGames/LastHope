﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InputManager {
    public static float joystickDeadZone = 0.2f;
    public static float triggerDeadZone = 0.2f;
    // Based on the XBOX 360 controller
    // ---- Axis
    // -- Left
    private static float LeftHorizontal()
    {
        float r = 0.0f;
        r += Input.GetAxis("JLeftHorizontal");
        r += Input.GetAxis("KLeftHorizontal");
        return Mathf.Clamp(r, -1.0f, 1.0f);
    }
    private static float LeftVertical()
    {
        float r = 0.0f;
        r += Input.GetAxis("JLeftVertical");
        r += Input.GetAxis("KLeftVertical");
        return Mathf.Clamp(r, -1.0f, 1.0f);
    }
    public static Vector3 LeftJoystick()
    {
        return new Vector3(LeftHorizontal(), 0, LeftVertical());
    }

    private static bool upInUse = false;
    public static bool LeftJoystickUp()
    {
        if (LeftVertical() > joystickDeadZone)
        {
            if (!upInUse)
            {
                upInUse = true;
            }
        }
        else
        {
            upInUse = false;
        }
        return upInUse;
    }
    private static bool downInUse = false;
    public static bool LeftJoystickDown()
    {
        if (LeftVertical() < -joystickDeadZone)
        {
            if (!downInUse)
            {
                downInUse = true;
            }
        }
        else
        {
            downInUse = false;
        }
        return downInUse;
    }
    private static bool leftInUse = false;
    public static bool LeftJoystickLeft()
    {
        if (LeftHorizontal() < -joystickDeadZone)
        {
            if (!leftInUse)
            {
                leftInUse = true;
            }
        }
        else
        {
            leftInUse = false;
        }
        return leftInUse;
    }
    private static bool rightInUse = false;
    public static bool LeftJoystickRight()
    {
        if (LeftHorizontal() > joystickDeadZone)
        {
            if (!rightInUse)
            {
                rightInUse = true;
            }
        }
        else
        {
            rightInUse = false;
        }
        return rightInUse;
    }
    // -- Right
    private static float RightHoirzontal()
    {
        float r = 0.0f;
        r += Input.GetAxis("JRightHorizontal");
        r += Input.GetAxis("MRightHorizontal");
        return Mathf.Clamp(r, -1.0f, 1.0f);
    }
    private static float RightVertical()
    {
        float r = 0.0f;
        r += Input.GetAxis("JRightVertical");
        r += Input.GetAxis("MRightVertical");
        return Mathf.Clamp(r, -1.0f, 1.0f);
    }
    public static Vector3 RightJoystick()
    {
        return new Vector3(RightHoirzontal(), 0, RightVertical());
    }

    public static bool Stance1()
    {
        bool ret = false;
        if (Input.GetButtonDown("KStance1"))
        {
            ret = true;
        }
        else if (Input.GetAxis("JStance1") > triggerDeadZone)
        {
            ret = true;
        }
        return ret;
    }
    public static bool Stance2()
    {
        bool ret = false;
        if (Input.GetButtonDown("KStance2"))
        {
            ret = true;
        }
        else if (Input.GetAxis("JStance2") < -triggerDeadZone)
        {
            ret = true;
        }
        return ret;
    }
    // ---- Buttons
    public static bool Pause()
    {
        return Input.GetButtonDown("Pause");
    }
    public static bool Dodge()
    {
        return Input.GetButtonDown("Dodge");
    }
    public static bool Interact()
    {
        return Input.GetButtonDown("Interact");
    }
    public static bool LightAttack()
    {
        return Input.GetButtonDown("LightAttack");
    }
    public static bool HeavyAttack()
    {
        return Input.GetButtonDown("HeavyAttack");
    }
    public static bool SpecialAttack()
    {
        return Input.GetButtonDown("SpecialAttack");
    }
    public static bool Block()
    {
        return Input.GetButtonDown("Block");
    }
    public static bool DebugMode()
    {
        return Input.GetButtonDown("DebugMode");
    }
}
