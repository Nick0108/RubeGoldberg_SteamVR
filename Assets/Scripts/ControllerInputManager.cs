using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ControllerInputManager: MonoBehaviour {

    //Traced Controller
    /*if for each hand
    public SteamVR_TrackedObject LeftTrackOBJ;
    public SteamVR_TrackedObject rightTrackOBJ;
    private SteamVR_Controller.Device leftController;
    private SteamVR_Controller.Device rightController;
    */
    public enum Hand
    {
        LeftHand,
        RightHand
    }

    public Hand forHand;
    private SteamVR_TrackedObject TrackObj;
    private SteamVR_Controller.Device ControllerDevice;

    //Teleporter
    public GameObject TeleporterTargetObject;
    public GameObject Player;
    public LayerMask TeleporterLayer;

    private LineRenderer TeleportLine;
    private Vector3 TeleporterLocation;

    //Grabing&Throwing
    [SerializeField,Range(1.0f,2.0f)]
    public float throwForce = 1.5f;


    // Use this for initialization
    void Start () {
        TrackObj = GetComponent<SteamVR_TrackedObject>();
        TeleportLine = GetComponentInChildren<LineRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
            ControllerDevice = SteamVR_Controller.Input((int)TrackObj.index);

        //(only Lefthand)--->for Teleporting
        if (forHand == Hand.LeftHand)
        {
            //To show the moving place
            if (ControllerDevice.GetTouch(SteamVR_Controller.ButtonMask.Touchpad))
            {
                TeleportLine.gameObject.SetActive(true);
                TeleporterTargetObject.SetActive(true);
                TeleportLine.SetPosition(0, transform.position);
                RaycastHit hitInfo;
                if (Physics.Raycast(transform.position, transform.forward, out hitInfo, 15.0f, TeleporterLayer))
                {
                    TeleporterLocation = hitInfo.transform.position;
                    TeleportLine.SetPosition(1, TeleporterLocation);
                }
                else
                {
                    TeleporterLocation = transform.position + transform.forward * 15;
                    TeleportLine.SetPosition(1, transform.position + transform.forward * 15);
                    RaycastHit groundRayhit;
                    if (Physics.Raycast(TeleporterLocation, -Vector3.up, out groundRayhit, 17.0f, TeleporterLayer))
                    {
                        TeleporterLocation = new Vector3(transform.position.x + transform.forward.x * 15, groundRayhit.transform.position.y, transform.position.z + transform.forward.z * 15);
                    }
                }
                TeleporterTargetObject.transform.position = TeleporterLocation + Vector3.up;
            }

            //To teleport the player
            if (ControllerDevice.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad))
            {
                if (TeleporterLocation != null)
                {
                    Player.transform.position = TeleporterLocation;
                    TeleportLine.gameObject.SetActive(false);
                    TeleporterTargetObject.SetActive(false);
                }
            }

            //Romove teleport
            if (ControllerDevice.GetTouchUp(SteamVR_Controller.ButtonMask.Touchpad))
            {
                TeleportLine.gameObject.SetActive(false);
                TeleporterTargetObject.SetActive(false);
            }
        }

        //(only RightHand) --->for menu
        if (forHand == Hand.RightHand)
        {

        }
	}

    //Grab and Throw the object
    private void OnTriggerStay(Collider other)
    {
        if (ControllerDevice.GetPressDown(SteamVR_Controller.ButtonMask.Grip))
        {
            GrabbingObject(other);
        }
        else if(ControllerDevice.GetPressUp(SteamVR_Controller.ButtonMask.Grip))
        {
            ThrowingObject(other);
        }
    }

    void GrabbingObject(Collider GrabbingOBJ)
    {
        if (GrabbingOBJ.CompareTag("Throwable"))
        {
            GrabbingOBJ.transform.SetParent(gameObject.transform);
            GrabbingOBJ.GetComponent<Rigidbody>().isKinematic = true;
            ControllerDevice.TriggerHapticPulse(2000);
        }
    }

    void ThrowingObject(Collider GrabbingOBJ)
    {
        if (GrabbingOBJ.CompareTag("Throwable"))
        {
            GrabbingOBJ.transform.SetParent(null);
            Rigidbody colliRid = GrabbingOBJ.GetComponent<Rigidbody>();
            colliRid.isKinematic = false;
            colliRid.velocity = ControllerDevice.velocity * throwForce;
            colliRid.angularVelocity = ControllerDevice.angularVelocity;
        }
    }
}
