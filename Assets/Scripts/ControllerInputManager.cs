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

    //Menu
    public ObjectMenuManager objectMenuManager;
    //public List<GameObject> InstanBasicObject;
    //public List<GameObject> InstanComplexObject;
    //public int BasicIndex;
    //public int ComplexIndex;
    //public bool isBasic = true;

    //Swipe_x
    private float Touchcurrent_x;
    private float TouchLast_x;
    private float SwipeSum_x;
    private float distance_x;
    private bool hasSwipeLeft_x;
    private bool hasSwipeRight_x;

    //Swipe_y
    private float Touchcurrent_y;
    private float TouchLast_y;
    private float SwipeSum_y;
    private float distance_y;
    private bool hasSwipeLeft_y;
    private bool hasSwipeRight_y;

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
            //memu apearing
            if (ControllerDevice.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
            {
                MenuAppear();

                if (ControllerDevice.GetTouchDown(SteamVR_Controller.ButtonMask.Touchpad))
                {
                    //Record the Axis of x and y on firstTouch
                    TouchLast_x = ControllerDevice.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad).x;
                    TouchLast_y = ControllerDevice.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad).y;
                }
                if (ControllerDevice.GetTouch(SteamVR_Controller.ButtonMask.Touchpad))
                {
                    //Record the Axis x on its moving distance
                    Touchcurrent_x = ControllerDevice.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad).x;
                    distance_x = Touchcurrent_x - TouchLast_x;
                    TouchLast_x = Touchcurrent_x;
                    SwipeSum_x += distance_x;

                    //Record the Axis y on its moving distance
                    Touchcurrent_y = ControllerDevice.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad).y;
                    distance_y = Touchcurrent_y - TouchLast_y;
                    TouchLast_y = Touchcurrent_y;
                    SwipeSum_y += distance_y;

                    //if Axis x's swipe distance >0.5, then change selected Object
                    if (!hasSwipeRight_x)
                    {
                        if (SwipeSum_x > 0.5f)
                        {
                            SwipeSum_x = 0;
                            SwipeRight();
                            hasSwipeRight_x = true;
                            hasSwipeLeft_x = false;
                        }
                    }
                    if (!hasSwipeLeft_x)
                    {
                        if (SwipeSum_x < -0.5f)
                        {
                            SwipeSum_x = 0;
                            SwipeLeft();
                            hasSwipeRight_x = false;
                            hasSwipeLeft_x = true;
                        }
                    }

                    //if Axis y's swipe distance >0.5, then change selected Munu
                    if (!hasSwipeRight_y)
                    {
                        if (SwipeSum_y > 0.5f)
                        {
                            SwipeSum_y = 0;
                            SwipeUP();
                            hasSwipeRight_y = true;
                            hasSwipeLeft_y = false;
                        }
                    }
                    if (!hasSwipeLeft_y)
                    {
                        if (SwipeSum_y < -0.5f)
                        {
                            SwipeSum_y = 0;
                            SwipeDown();
                            hasSwipeRight_y = false;
                            hasSwipeLeft_y = true;
                        }
                    }
                }
                //if leave the touchpad ,reSet the touch number
                if (ControllerDevice.GetTouchUp(SteamVR_Controller.ButtonMask.Touchpad))
                {
                    Touchcurrent_x = 0;
                    TouchLast_x = 0;
                    distance_x = 0;
                    SwipeSum_x = 0;
                    hasSwipeLeft_x = false;
                    hasSwipeRight_x = false;
                }
                if (ControllerDevice.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad))
                {
                    SpawnObject();
                }
            }
            //Menu disappear
            if (ControllerDevice.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
            {
                MenuDisappear();
            }
        }
	}

    private void SwipeRight()
    {
        objectMenuManager.SwipeRight();
    }

    private void SwipeLeft()
    {
        objectMenuManager.SwipeLeft();
    }

    private void SwipeUP()
    {
        objectMenuManager.SwipeUp();
    }

    private void SwipeDown()
    {
        objectMenuManager.SwipeDown();
    }

    private void MenuAppear()
    {
        objectMenuManager.Appear();
    }

    private void MenuDisappear()
    {
        objectMenuManager.Disappear();
    }
    
    private void SpawnObject()
    {
        objectMenuManager.SpawnObject();
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
