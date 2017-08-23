using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;
using Common;
using Common.Tools;
using ExitGames.Client.Photon;

public class SyncPlayerRequest : Request
{
    private PlayerManger _playerManger;

    public override void Start()
    {
        base.Start();
        _playerManger = GetComponent<PlayerManger>();
    }

    public SyncPlayerRequest()
    {
        OpCode=OperationCode.SyncPlayer;
    }

    public override void DefaultRequest()
    {
        PhotonEngine.Instance.Peer.OpCustom((byte) OpCode, null, true);
    }

    public override void OnOperationResonse(OperationResponse response)
    {
        string usernameListString = (string) DictTool.GetValue(response.Parameters, (byte) ParamererCode.UsernameList);
        using (StringReader sr = new StringReader(usernameListString))
        {
            XmlSerializer serializer=new XmlSerializer(typeof(List<string>));
            List<string> usernameList = (List<string>)serializer.Deserialize(sr);
            _playerManger.OnSyncPlayerRequest(usernameList);
        }       
    }
}
