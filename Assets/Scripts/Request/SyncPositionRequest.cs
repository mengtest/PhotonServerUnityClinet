using UnityEngine;
using System.Collections.Generic;
using Common;
using ExitGames.Client.Photon;

public class SyncPositionRequest :Request
{
    [HideInInspector]
    public Vector3 Pos;

    public override void DefaultRequest()
    {
        Dictionary<byte, object> data = new Dictionary<byte, object>
        {
            {(byte) ParamererCode.X, Pos.x},
            {(byte) ParamererCode.Y, Pos.y},
            {(byte) ParamererCode.Z, Pos.z}
        };     
        PhotonEngine.Instance.Peer.OpCustom((byte)OpCode, data, true);
    }

    public override void OnOperationResonse(OperationResponse response)
    {
        
    }
}
