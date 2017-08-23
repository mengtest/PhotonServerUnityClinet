using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Common;
using Common.Tools;
using ExitGames.Client.Photon;
using UnityEngine;

public class SyncPositionEvent : BaseEvent
{
    private PlayerManger _playerManger;

    public override void Start()
    {
        base.Start();
        _playerManger = GetComponent<PlayerManger>();
    }


    public override void OnEvent(EventData eventData)
    {        
        string playerDataListString=(string) DictTool.GetValue(eventData.Parameters, (byte)ParamererCode.PlayerDataList);
        Debug.Log(playerDataListString);

        if (string.IsNullOrEmpty(playerDataListString))
        {
            return;
        }
   
        using (StringReader sr=new StringReader(playerDataListString))
        {
            XmlSerializer serializer=new XmlSerializer(typeof(List<PlayerData>));
            List<PlayerData> playerDataList =(List<PlayerData>) serializer.Deserialize(sr);
            _playerManger.OnSyncPositionEvent(playerDataList);
        }
    }
}
