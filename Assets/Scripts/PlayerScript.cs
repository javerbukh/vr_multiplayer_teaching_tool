using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerScript : MonoBehaviour
{
    public GameObject CubePrefab;
    GameObject m_InstantiatedCube;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            // GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            GameObject m_InstantiatedCube = Instantiate(CubePrefab);
            Vector3 position = new Vector3(Random.Range(-10.0f, 10.0f), 0, Random.Range(-10.0f, 10.0f));
            m_InstantiatedCube.transform.position = position;
            Debug.Log("Space pressed!!");

            NetworkServer.Spawn(m_InstantiatedCube);

        }
    }
}
