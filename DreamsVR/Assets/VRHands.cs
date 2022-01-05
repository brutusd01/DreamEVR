using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class VRHands : MonoBehaviour
{
    private InputDevice targetDevice;
    void Start()
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDeviceCharacteristics rightDeviceCharacteristics = InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;
        //InputDeviceCharacteristics leftDeviceCharacteristics = InputDeviceCharacteristics.Left | InputDeviceCharacteristics.Controller;

        InputDevices.GetDevicesWithCharacteristics(rightDeviceCharacteristics, devices);

        foreach (var item in devices)
        {
            Debug.Log(item.name + item.characteristics);
        }

        if(devices.Count > 0)
        {
            targetDevice = devices[0];
        }
    }

    // Update is called once per frame
    void Update()
    {
        //TODO: Factor code to shorten these to variables
        //TODO: Create an input manager and a player spawner.
       // targetDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool primButtonValue);
        if(targetDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool primButtonValue) && primButtonValue)
        {
            Debug.Log("Pressed primary!");
        }

       // targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue);
        if (targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue) && triggerValue > .1f)
        {
            Debug.Log("Trigger Value: " + triggerValue);
        }

        //targetDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 inputAxis);
        if (targetDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 inputAxis) && inputAxis != Vector2.zero)
        {
            //TODO: Movement here!
            Debug.Log("Primary Touchpad " + inputAxis);
        }
    }
}
