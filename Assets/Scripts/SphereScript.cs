using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class SphereScript : MonoBehaviour
{
    private RealtimeTransform rtTransform;
    private RealtimeView rtView;
    private int ownership = -1;

    // Start is called before the first frame update
    void Start()
    {
        rtTransform = gameObject.GetComponent<RealtimeTransform>();
        rtView = gameObject.GetComponent<RealtimeView>();
    }

    // Update is called once per frame
    public void Grabbed()
    {
        rtTransform.RequestOwnership();
        rtView.RequestOwnership();
        ownership = rtTransform.ownerID;
    }
}
