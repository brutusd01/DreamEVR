using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

namespace Player
{
    

public class VRHands : MonoBehaviour
{
    [Header("Variables")]
    public bool showController = false;
    public InputDeviceCharacteristics deviceCharacteristics;
    public List<GameObject> controllerTypes;
    public GameObject handModel;
    public GameObject teleportRay;
    private Animator anim;
    private InputDevice targetDevice;
    private GameObject currentDevice;
    private GameObject currentHands;

    [Header("Inputs")]
    [SerializeField]private float triggerValue;
    [SerializeField]private float gripValue;
    void Start()
    {
        GetControllers();
    }

    private void GetControllers()
    {
        List<InputDevice> devices = new List<InputDevice>();
        //InputDeviceCharacteristics rightDeviceCharacteristics = InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;
        //InputDeviceCharacteristics leftDeviceCharacteristics = InputDeviceCharacteristics.Left | InputDeviceCharacteristics.Controller;

        InputDevices.GetDevicesWithCharacteristics(deviceCharacteristics, devices);

        foreach (var item in devices)
        {
            Debug.Log(item.name + item.characteristics);
        }

        if (devices.Count > 0)
        {
            targetDevice = devices[0];
            GameObject handType = controllerTypes.Find(controller => controller.name == targetDevice.name);
            if (handType)
            {
                currentDevice = Instantiate(handType, transform);
            }

            else
            {
                Debug.Log("Can't find identify controller.");
                currentDevice = Instantiate(controllerTypes[0], transform);
            }

            currentHands = Instantiate(handModel, transform);
            anim = handModel.GetComponent<Animator>();
        }
    }

    void Update()
    {
        ReadInputs();

        if(!targetDevice.isValid) 
        { 
            GetControllers();
        }
        else
        {
            if (showController)
            {
            currentDevice.SetActive(true);
            currentHands.SetActive(false);
       
            }
        else
        
            {
            currentDevice.SetActive(false);
            currentHands.SetActive(true);
            UpdateAnims();
            }
 
        }
        
    }

    void UpdateAnims()
    {
        if(!anim.isActiveAndEnabled)
        {
            anim = GetComponentInChildren<Animator>();
        }

        if (triggerValue>0)
        {
            anim.SetFloat("Trigger", triggerValue);
        }
        else
            anim.SetFloat("Trigger", 0);
        //Grip Inputs
        if (gripValue>0)
        {
            anim.SetFloat("Grip", gripValue);
        }
        else
            anim.SetFloat("Grip", 0);
    }
    private void ReadInputs()
    {
        //TODO: Setup Action Based rather than Device Based
        //TODO: Create an input manager and a player spawner.
        if (targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float trigger) && trigger > .1f)
        {
            triggerValue = trigger;
        }

        if (targetDevice.TryGetFeatureValue(CommonUsages.grip, out float grip) && grip > .1f)
        {
            gripValue = grip;
        }

    }
}
}