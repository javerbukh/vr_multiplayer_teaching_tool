using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.Serialization;
using Normal.Realtime;
using Valve.VR;

public class VRPlayerScript : RealtimeComponent
{
    [SerializeField] private Realtime _realtime;

    [SerializeField] private GameObject spherePrefab;

    private SphereModel _sphereModel = new SphereModel();

    public SteamVR_Action_Single squeeze = null;

    public SteamVR_Action_Boolean Bpressed = null;

    public SteamVR_Action_Boolean Apressed = null;

    public SteamVR_Action_Vector2 joystickPosition = null;

    public Transform leftCont;

    public Transform rightCont;

    public SteamVR_Input_Sources LeftInputSource = SteamVR_Input_Sources.LeftHand;

    private Color[] colors_list = { Color.red, Color.yellow, Color.green, Color.blue, Color.cyan, Color.magenta, Color.white, Color.grey, Color.black };

    public int current_index = 0;

    private GameObject floatingSphere = null;

    private void Start()
    {
        floatingSphere = GameObject.Find("SpherePrefab").gameObject;
        AdjustPreviewSphere();
        _sphereModel.sphereColor = colors_list[current_index];
        // AdjustPlayer();

    }
    // Update is called once per frame
    void Update()
    {
        if (!_realtime.connected)
            return;

        floatingSphere.transform.position = leftCont.position;

        if (Input.GetKeyDown("space"))
        {
            // GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

            GameObject sphereObj = Realtime.Instantiate(spherePrefab.name, ownedByClient: true, useInstance: _realtime);

        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Bpressed.stateDown)
        {
            if(current_index == 0)
            {
                current_index = colors_list.Length - 1;
            }
            else
            {
                current_index -= 1;
            }
            AdjustPreviewSphere();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) || Apressed.stateDown)
        {
            if (current_index == colors_list.Length - 1)
            {
                current_index = 0;
            }
            else
            {
                current_index += 1;
            }
            AdjustPreviewSphere();
        }
        if (squeeze.axis > 0.8f)
        {
            Debug.Log("Squeezing");
            GameObject sphereObj = Realtime.Instantiate(spherePrefab.name, position: leftCont.position, rotation: leftCont.rotation, ownedByClient: true, useInstance: _realtime);
            sphereObj.GetComponent<Renderer>().material.color = _sphereModel.sphereColor;
        }

    }

    private void AdjustPlayer()
    {
        if (joystickPosition.axis.x > 0.5f)
        {
            
            transform.Translate(Vector3.left * Time.deltaTime);
            // Define a target position above and behind the target transform
            //Vector3 targetPosition = transform.Translate(Vector3.left);

            //// Smoothly move the camera towards that target position
            //transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }
        else if (joystickPosition.axis.x < -0.5f)
        {
            transform.Translate(Vector3.right * Time.deltaTime);
        }
        if (joystickPosition.axis.y > 0.5f)
        {

            transform.Translate(Vector3.forward * Time.deltaTime);

        }
        else if (joystickPosition.axis.y < -0.5f)
        {
            transform.Translate(Vector3.back * Time.deltaTime);
        }

    }
    private void AdjustPreviewSphere()
    {
        floatingSphere.transform.position = leftCont.position;
        floatingSphere.GetComponent<Renderer>().material.color = colors_list[current_index];
        _sphereModel.sphereColor = colors_list[current_index];
    }
}
