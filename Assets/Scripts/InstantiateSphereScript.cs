using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class InstantiateSphereScript : MonoBehaviour
{

    public Realtime realtime;
    public Transform spawnPosition;

    private void SpawnSphere(Realtime realtime)
    {
        Realtime.Instantiate("SpherePrefab",
            position: spawnPosition.position,
            rotation: Quaternion.identity,
            ownedByClient: false,
            preventOwnershipTakeover: false,
            useInstance: realtime);
    }
}
