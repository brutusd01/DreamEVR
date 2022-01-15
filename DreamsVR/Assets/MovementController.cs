using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class MovementController : MonoBehaviour
{
    //TODO: Be able to set MoveType in game.
    public enum MoveType {Teleport, Continuous }
    private MoveType move;
    [Header("Teleport Movement")]
    public bool Teleport = true;
    public XRController leftTele;
    public XRController rightTele;
    public InputHelpers.Button teleportInput;
    public float deadZone = .1f;
    [Header("Continuous")]
    public float speed = 1.5f;
    public float vSpeed;
    public XRNode input;
    private Vector2 inputAxis;
    private CharacterController character;

    private void Start()
    {
        if(!character)character = GetComponent<CharacterController>();
    }
    void Update()
    {
        //if (move==MoveType.Teleport)
        if(Teleport)
        {
            if (leftTele)
            {
                leftTele.gameObject.SetActive(CheckActive(leftTele));
            }
            if (rightTele)
            {
                rightTele.gameObject.SetActive(CheckActive(rightTele));
            }
        }
        else
        {
            InputDevice device = InputDevices.GetDeviceAtXRNode(input);
            device.TryGetFeatureValue(CommonUsages.primary2DAxis, out inputAxis);
        }
    }

    private void FixedUpdate()
    {
        if (!Teleport)
        {
            Vector3 moveVector = new Vector3(inputAxis.x, vSpeed, inputAxis.y);
            character.Move(moveVector * Time.fixedDeltaTime * speed);
        }
    }

    public bool CheckActive(XRController controller)
    {
        InputHelpers.IsPressed(controller.inputDevice, teleportInput, out bool isPressed, deadZone);
        return isPressed;
    }
}
