using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Common;
using Common.Tools;
using ExitGames.Client.Photon;

public class PhotonEngine : MonoBehaviour,IPhotonPeerListener
{
    public static PhotonEngine Instance;
    public PhotonPeer Peer;

    private readonly Dictionary<OperationCode,Request> _requestDict=new Dictionary<OperationCode, Request>();
    private readonly Dictionary<EventCode, BaseEvent> _eventDict = new Dictionary<EventCode, BaseEvent>();

    public static string Username;

    void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance!=this)
        {
            Destroy(gameObject);
        }        
    }

	void Start ()
	{
	    string adress = "127.0.0.1:5055";
	    string appName = "MyGame1";

	    Peer=new PhotonPeer(this,ConnectionProtocol.Udp);
        Peer.Connect("127.0.0.1:5055", "MyGame1");
//        Peer.Connect("220.203.63.237:5055", "MyGame1");
	}
	
	void Update () 
	{
        Peer.Service();

//        if (peer.PeerState == PeerStateValue.Connected)
//        {
//            peer.Connect("127.0.0.1:5055", "MyGame1");
//	      }	    
	}

    void OnDestroy()
    {
        Peer.Disconnect();
//        if (peer!=null&&peer.PeerState==PeerStateValue.Connected)
//        {
//            peer.Disconnect();
//        }
    }

    public void DebugReturn(DebugLevel level, string message)
    {
        
    }

    //请求的响应
    public void OnOperationResponse(OperationResponse operationResponse)
    {
        OperationCode opCode =  (OperationCode)operationResponse.OperationCode;
        Request request;
        bool temp= _requestDict.TryGetValue(opCode, out request);

        if (temp)
        {
            request.OnOperationResonse(operationResponse);
        }
        else
        {
            Debug.Log("没找到对应的响应处理对象");           
        }
    }

    public void OnStatusChanged(StatusCode statusCode)
    {
        Debug.Log(statusCode);
    }

    //只响应
    public void OnEvent(EventData eventData)
    {
        EventCode evtCode = (EventCode) eventData.Code;
        BaseEvent evt= DictTool.GetValue(_eventDict, evtCode);
        if (evt!=null)
        {
            Debug.Log(evtCode);
            evt.OnEvent(eventData); 
        }
        else
        {
            Debug.Log(evtCode+"没有找到");
        }
    }

    public void AddRequest(Request request)
    {
        if (!_requestDict.ContainsKey(request.OpCode))
        {
            _requestDict.Add(request.OpCode, request);
        }
    }    
    
    public void RemoveRequest(Request request)
    {
        if (_requestDict.ContainsKey(request.OpCode))
        {
            _requestDict.Remove(request.OpCode);
        }      
    }    
    
    public void AddEvent(BaseEvent evt)
    {
        if (!_eventDict.ContainsKey(evt.EvtCode))
        {
            _eventDict.Add(evt.EvtCode, evt);
        }
    }

    public void RemoveEvent(BaseEvent evt)
    {
        if (_eventDict.ContainsKey(evt.EvtCode))
        {
            _eventDict.Remove(evt.EvtCode);
        }      
    }
}
