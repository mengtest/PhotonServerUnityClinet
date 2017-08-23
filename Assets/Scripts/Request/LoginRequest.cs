using System;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using Common;
using UnityEngine;

public class LoginRequest : Request
{
    [HideInInspector]
    public string Username,Password;

    private LoginPanel loginPanel;

    public override void Start()
    {
        base.Start();
        loginPanel=GetComponent<LoginPanel>();
    }

    public override void DefaultRequest()
    {
        Dictionary<byte,object> data=new Dictionary<byte, object>();
        data.Add((byte) ParamererCode.Username,Username);
        data.Add((byte)ParamererCode.Password,Password);

        PhotonEngine.Instance.Peer.OpCustom((byte)OpCode, data, true);
    }

    public override void OnOperationResonse(OperationResponse response)
    {
        ReturenCode returenCode = (ReturenCode)response.ReturnCode;
        if (returenCode==ReturenCode.Success)
        {
            PhotonEngine.Username = Username;
        }

        loginPanel.OnLoginResponse(returenCode);
    }
}
