using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InputManager {
    // Based on the XBOX 360 controller
    // ---- Axis
    // -- Left
    private static float LeftHoirzontal()
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
        return new Vector3(LeftHoirzontal(), 0, LeftVertical());
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

    // ---- Buttons
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
}
