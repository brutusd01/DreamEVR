using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MovementController : MonoBehaviour
{
    public XRController leftTele;
    public XRController rightTele;
    public InputHelpers.Button teleportInput;
    public float deadZone = .1f;

    void Update()
    {
        if(leftTele)
        {
            leftTele.gameObject.SetActive(CheckActive(leftTele));
        }
        if (rightTele)
        {
            rightTele.gameObject.SetActive(CheckActive(rightTele));
        }
    }

    public bool CheckActive(XRController controller)
    {
        InputHelpers.IsPressed(controller.inputDevice, teleportInput, out bool isPressed, deadZone);
        return isPressed;
    }
}
