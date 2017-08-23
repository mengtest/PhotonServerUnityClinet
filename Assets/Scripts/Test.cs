using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Test : MonoBehaviour 
{

	void Start () 
	{
	
	}
	
	void Update () 
	{
	    if (Input.GetMouseButtonDown(0))
	    {
	        SendRequest();
	    }
	}

    void SendRequest()
    {
        Dictionary<byte, object> data = new Dictionary<byte, object> {{1, 100}, {2, "qwetdf"}};
        PhotonEngine.Instance.Peer.OpCustom(1, data, true);
    }
}
