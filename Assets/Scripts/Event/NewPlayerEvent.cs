using UnityEngine;
using System.Collections;
using Common;
using Common.Tools;
using ExitGames.Client.Photon;

public class NewPlayerEvent : BaseEvent
{
    private PlayerManger _playerManger;

    public override void Start()
    {
        base.Start();
        _playerManger = GetComponent<PlayerManger>();
    }

    public override void OnEvent(EventData eventData)
    {
        string username=(string) DictTool.GetValue(eventData.Parameters,(byte)EventCode.NewPlayer);
        _playerManger.OnNewPlayerEvent(username);
    }
}
