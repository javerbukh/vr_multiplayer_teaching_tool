using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerScript : NetworkBehaviour
{
    public GameObject CubePrefab;
    private GameObject cube;

    private GameObject rightContSource;
    private GameObject leftContSource;
    private GameObject headsetSource;

    public GameObject rightContPrefab;
    public GameObject leftContPrefab;
    public GameObject headsetPrefab;

    private GameObject rightContObj;
    private GameObject leftContObj;
    private GameObject headsetObj;


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
        headsetObj = (GameObject)Instantiate(headsetPrefab);
        rightContObj = (GameObject)Instantiate(rightContPrefab);
        leftContObj = (GameObject)Instantiate(leftContPrefab);

        // spawn the bullet on the clients
        NetworkServer.Spawn(headsetObj);
        NetworkServer.Spawn(rightContObj);
        NetworkServer.Spawn(leftContObj);
    }

    [Command]
    public void CmdControllerPositionSync()
    {
        headsetObj.transform.localRotation = headsetSource.transform.parent.localRotation;
        headsetObj.transform.position = headsetSource.transform.position;

        rightContObj.transform.localPosition = rightContSource.transform.position;
        rightContObj.transform.localRotation = rightContSource.transform.localRotation;

        leftContObj.transform.localPosition = leftContSource.transform.position;
        leftContObj.transform.localRotation = leftContSource.transform.localRotation;
    }

    void Start()
    {
        if (!isLocalPlayer)
        {
            headsetSource = GameObject.Find("Camera");
            rightContSource = GameObject.Find("Controller (right)");
            leftContSource = GameObject.Find("Controller (left)");

            CmdInstantiteHeadAndController();
        }

        //headset = Camera.main.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        else
        {
            CmdControllerPositionSync();
        }
        if (Input.GetKeyDown("space"))
        {
            // GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

            CmdMakeBox();


        }
    }

}
