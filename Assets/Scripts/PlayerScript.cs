using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using InputTracking = UnityEngine.XR.InputTracking;
using Node = UnityEngine.XR.XRNode;

public class PlayerScript : NetworkBehaviour
{
    public GameObject CubePrefab;

    public GameObject headsetPrefab;
    public GameObject rightContPrefab;
    public GameObject leftContPrefab;

    private GameObject cube;

    private GameObject headsetSource;
    private GameObject rightContSoure;
    private GameObject leftContSource;

    private GameObject headsetObj;
    private GameObject rightContObj;
    private GameObject leftContObj;


    [Command]
    public void CmdMakeBox()
    {
        GameObject m_InstantiatedCube = Instantiate(CubePrefab);
        Vector3 position = new Vector3(Random.Range(-10.0f, 10.0f), 0, Random.Range(-10.0f, 10.0f));
        m_InstantiatedCube.transform.position = position;
        Debug.Log("Space pressed!!");

        NetworkServer.Spawn(m_InstantiatedCube);
    }

    [Command]
    void CmdInstantiteHeadAndController()
    {
        headsetObj = Instantiate(headsetPrefab);
        rightContObj = Instantiate(rightContPrefab);
        leftContObj = Instantiate(leftContPrefab);

        headsetSource = GameObject.Find("Camera");
        rightContSoure = GameObject.Find("Controller (left)");
        leftContSource = GameObject.Find("Controller (right)");

        //headsetObj = gameObject.transform.Find("headsetPrefab").gameObject;
        //rightContObj = gameObject.transform.Find("rightContPrefab").gameObject;
        //leftContObj = gameObject.transform.Find("leftContPrefab").gameObject;



        //// spawn the bullet on the clients
        NetworkServer.Spawn(headsetObj);
        NetworkServer.Spawn(rightContObj);
        NetworkServer.Spawn(leftContObj);

        CmdControllerPositionSync();
    }

    [Command]
    public void CmdControllerPositionSync()
    {

        headsetObj.transform.localRotation = headsetSource.transform.rotation;
        rightContObj.transform.localRotation = headsetSource.transform.rotation;
        leftContObj.transform.localRotation = headsetSource.transform.rotation;

        headsetObj.transform.localPosition = headsetSource.transform.position;
        rightContObj.transform.localPosition = headsetSource.transform.position;
        leftContObj.transform.localPosition = headsetSource.transform.position;
    }

    void Start()
    {
        if (isLocalPlayer)
        {
            CmdInstantiteHeadAndController();
            //headsetSource = GameObject.Find("Camera");
            //rightContSource = GameObject.Find("Controller (right)");
            //leftContSource = GameObject.Find("Controller (left)");


        }

        //headset = Camera.main.gameObject;
    }
    //public override void OnStartLocalPlayer()
    //{
    //    base.OnStartLocalPlayer();
    //    CmdInstantiteHeadAndController();
    //}

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        
        CmdControllerPositionSync();

        if (Input.GetKeyDown("space"))
        {
            // GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

            CmdMakeBox();


        }
    }

}
