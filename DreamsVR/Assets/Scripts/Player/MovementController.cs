using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

namespace Player
{
    public class MovementController : MonoBehaviour
{
    //TODO: Be able to set MoveType in game via settings.
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
    public float terminalVelocity = -200f;
    public float damp = .25f;
    public XRNode input;
    [Header("Variables")]
    public float extraHeight;
    private Vector2 inputAxis;
    private CharacterController character;
    private XROrigin rig;

    private void Start()
    {
        if(!character)character = GetComponent<CharacterController>();
        if (!rig) rig = GetComponent<XROrigin>();
    }
    void Update()
    {
        //TODO: Update this to be a switch(move) function
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
    float Gravity()
    {
        if (character.isGrounded)
        {
            vSpeed = Physics.gravity.y;
        }
        else
        {
            vSpeed += Physics.gravity.y * Time.fixedDeltaTime;
            Mathf.Clamp(vSpeed, 0, terminalVelocity); //This is to prevent the player from going past a certain fall speed.
        }
        //TODO: Add jump functionality here! 
            return (vSpeed * damp);
    }
    private void FixedUpdate() //May have to change the physics timestep do work better for a VR project.
    {
        heightControl();
        if (!Teleport)
        {           
            Quaternion headYaw = Quaternion.Euler(0, rig.Camera.transform.eulerAngles.y, 0);
            Vector3 moveVector = headYaw * new Vector3(inputAxis.x, Gravity(), inputAxis.y);
            character.Move(moveVector * Time.fixedDeltaTime * speed);
        }
    }

    public bool CheckActive(XRController controller)
    {
        InputHelpers.IsPressed(controller.inputDevice, teleportInput, out bool isPressed, deadZone);
        return isPressed;
    
    
    }
    void heightControl()
    {
        //This updates the player height to match how high the camera is, allows for prone height changes.
        character.height = rig.CameraInOriginSpaceHeight + extraHeight;
        Vector3 center = transform.InverseTransformPoint(rig.Camera.transform.position);
        character.center = new Vector3(center.x,character.height/2 + character.skinWidth, center.z);
    }
}
}