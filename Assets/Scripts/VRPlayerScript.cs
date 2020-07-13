using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.Serialization;
using Normal.Realtime;
using Valve.VR;

public class VRPlayerScript : MonoBehaviour
{
    [SerializeField] private Realtime _realtime;

    [SerializeField] private GameObject spherePrefab;

    public SteamVR_Action_Boolean TriggerAction = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!_realtime.connected)
            return;

        if (Input.GetKeyDown("space"))
        {
            // GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

            GameObject sphereObj = Realtime.Instantiate(spherePrefab.name, ownedByClient: true, useInstance: _realtime);

        }

        if (TriggerAction.stateDown)
        {
            GameObject sphereObj = Realtime.Instantiate(spherePrefab.name, ownedByClient: true, useInstance: _realtime);
        }
        //if (Input.GetAxisRaw("Left Trigger") > 0.1f)
        //{
        //    GameObject sphereObj = Realtime.Instantiate(spherePrefab.name, ownedByClient: true, useInstance: _realtime);
        //}

    }
}
