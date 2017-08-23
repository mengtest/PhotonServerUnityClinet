using UnityEngine;
using System.Collections;
using Common;
using ExitGames.Client.Photon;

public abstract class BaseEvent : MonoBehaviour 
{
    public EventCode EvtCode;
    public abstract void OnEvent(EventData eventData);

    public virtual void Start()
    {
        PhotonEngine.Instance.AddEvent(this);
    }

    public void OnDestory()
    {
        PhotonEngine.Instance.RemoveEvent(this);
    }
}
