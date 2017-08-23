using UnityEngine;
using System.Collections.Generic;
using Common;
using Common.Tools;

public class PlayerManger : MonoBehaviour
{
    public GameObject Player;
    public GameObject PlayerPrefab;

    private SyncPositionRequest _syncPositionRequest;
    private SyncPlayerRequest _syncPlayerRequest;
    private Vector3 _lastPostion = Vector3.zero;
    private float moveOffset = 0.1f;

    private readonly Dictionary<string, GameObject> _playerDict = new Dictionary<string, GameObject>();


	void Start () 
	{
        _syncPositionRequest = GetComponent<SyncPositionRequest>();
        _syncPlayerRequest = GetComponent<SyncPlayerRequest>();
        _syncPlayerRequest.DefaultRequest();
        InvokeRepeating("SyncPositon", 3f, 0.05f);
	}
	
    //向服务端发送自身位置
    void SyncPositon()
    {
        if (Vector3.Distance(Player.transform.position, _lastPostion) > moveOffset)
        {
            _lastPostion = Player.transform.position;
            _syncPositionRequest.Pos = Player.transform.position;
            _syncPositionRequest.DefaultRequest();
        }
    }

    //实例其他Player
    public void OnSyncPlayerRequest(List<string> usernameList)
    {
        //创建其他客户端的Player角色
        foreach (string username in usernameList)
        {
            OnNewPlayerEvent(username);
        }
    }

    //增一个Player
    public void OnNewPlayerEvent(string username)
    {
        GameObject go = Instantiate(PlayerPrefab);
        go.GetComponent<Player>().Username = username;
        _playerDict.Add(username, go);
    }

    //同步位置信息
    public void OnSyncPositionEvent(List<PlayerData> playDataList)
    {
        foreach (PlayerData pd in playDataList)
        {
            GameObject go = DictTool.GetValue(_playerDict, pd.Username);
            Debug.Log(go);
            if (go!=null)
            {
                go.transform.position = new Vector3(pd.Pos.X, pd.Pos.Y, pd.Pos.Z);
            }
        }
    }
}
