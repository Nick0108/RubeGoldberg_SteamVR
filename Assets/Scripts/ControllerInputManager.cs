using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerInputManager: MonoBehaviour {

    //Traced Controller
    public SteamVR_TrackedObject LeftTrackOBJ;
    public SteamVR_TrackedObject rightTrackOBJ;

    private SteamVR_Controller.Device leftController;
    private SteamVR_Controller.Device rightController;

    //Teleporter
    public GameObject TeleporterTargetObject;
    public GameObject Player;
    public LayerMask TeleporterLayer;

    private LineRenderer TeleportLine;
    private Vector3 TeleporterLocation;
    
    

    // Use this for initialization
    void Start () {
        TeleportLine = GetComponentInChildren<LineRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
            leftController = SteamVR_Controller.Input((int)LeftTrackOBJ.index);
            rightController = SteamVR_Controller.Input((int)rightTrackOBJ.index);
        
        //To show the moving place
        if (leftController.GetTouch(SteamVR_Controller.ButtonMask.Touchpad))
        {
            TeleportLine.gameObject.SetActive(true);
            TeleporterTargetObject.SetActive(true);
            TeleportLine.SetPosition(0, transform.position);
            RaycastHit hitInfo;
            if(Physics.Raycast(transform.position,transform.forward,out hitInfo, 15.0f, TeleporterLayer))
            {
                TeleporterLocation = hitInfo.transform.position;
                TeleportLine.SetPosition(1, TeleporterLocation);
            }
            else
            {
                TeleporterLocation = transform.position + transform.forward * 15;
                TeleportLine.SetPosition(1, transform.position + transform.forward * 15);
                RaycastHit groundRayhit;
                if (Physics.Raycast(TeleporterLocation, -Vector3.up,out groundRayhit, 17.0f, TeleporterLayer))
                {
                    TeleporterLocation = new Vector3(transform.position.x + transform.forward.x * 15, groundRayhit.transform.position.y, transform.position.z + transform.forward.z * 15);
                }
            }
            TeleporterTargetObject.transform.position = TeleporterLocation + Vector3.up;
        }
        //To teleport the player
        if (leftController.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad))
        {
            if (TeleporterLocation != null)
            {
                Player.transform.position = TeleporterLocation;
                TeleportLine.gameObject.SetActive(false);
                TeleporterTargetObject.SetActive(false);
            }
        }

        //Romove teleport
        if (leftController.GetTouchUp(SteamVR_Controller.ButtonMask.Touchpad))
        {
            TeleportLine.gameObject.SetActive(false);
            TeleporterTargetObject.SetActive(false);
        }
	}
}
