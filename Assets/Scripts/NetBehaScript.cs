using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetBehaScript : NetworkBehaviour
{
    public GameObject CubePrefab;
    GameObject m_InstantiatedCube;

    [Command]
    void CmdMakeBox()
    {
        GameObject m_InstantiatedCube = Instantiate(CubePrefab);
        Vector3 position = new Vector3(Random.Range(-10.0f, 10.0f), 0, Random.Range(-10.0f, 10.0f));
        m_InstantiatedCube.transform.position = position;
        Debug.Log("Space pressed!!");

        NetworkServer.Spawn(m_InstantiatedCube);
    }
}
