using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Common;
using ExitGames.Client.Photon;

public class RegisterRequest : Request
{
    [HideInInspector]
    public string Username, Password;

    private RegisterPanel registerPanel;

    public override void Start()
    {
        base.Start();
        registerPanel = GetComponent<RegisterPanel>();
    }


    public override void DefaultRequest()
    {
        Dictionary<byte, object> data = new Dictionary<byte, object>();
        data.Add((byte)ParamererCode.Username, Username);
        data.Add((byte)ParamererCode.Password, Password);

        PhotonEngine.Instance.Peer.OpCustom((byte)OpCode, data, true);
    }

    public override void OnOperationResonse(OperationResponse response)
    {
        ReturenCode returenCode = (ReturenCode)response.ReturnCode;
        registerPanel.OnRegisterResponse(returenCode);

    }
}
