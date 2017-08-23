using UnityEngine;
using System.Collections;
using Common;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginPanel : MonoBehaviour
{
    public GameObject registerPanel;
    public InputField usernameIF, passwordIF;
    public Text message;

    private LoginRequest loginRequest;


    void Start()
    {
        loginRequest = GetComponent<LoginRequest>();
    }

    public void OnLoginClick()
    {
        message.text = "";
        loginRequest.Username = usernameIF.text;
        loginRequest.Password = passwordIF.text;

        loginRequest.DefaultRequest();
    }

    public void OnRegisterClick()
    {
        registerPanel.SetActive(true);
        gameObject.SetActive(false);
    }

    public void OnLoginResponse(ReturenCode returenCode)
    {
        if (returenCode==ReturenCode.Success)
        {
            //调转到先一个场景
            SceneManager.LoadScene("Game");
        }
        else
        {
            //显示提示信息
            message.text = "用户名或密码错误";
        }

    }
}
