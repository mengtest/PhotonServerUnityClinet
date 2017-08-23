using Common;
using ExitGames.Client.Photon;
using UnityEngine;

public abstract class Request:MonoBehaviour
{
    public OperationCode OpCode;
    public abstract void DefaultRequest();
    public abstract void OnOperationResonse(OperationResponse response);

    public virtual void Start()
    {
        PhotonEngine.Instance.AddRequest(this);
    }

    public void OnDestory()
    {
        PhotonEngine.Instance.RemoveRequest(this);
    }
}
