using UnityEngine;
using System.Collections;
using Common;
using UnityEngine.UI;

public class RegisterPanel : MonoBehaviour
{
    public GameObject loginPanel;
    public InputField usernameIF, passwordIF;
    public Text message;

    private RegisterRequest registerRequest;

    void Start()
    {
        registerRequest = GetComponent<RegisterRequest>();
    }

    public void OnRegisterClick()
    {
        message.text = "";

        registerRequest.Username = usernameIF.text;
        registerRequest.Password = passwordIF.text;

        registerRequest.DefaultRequest();

    }

    public void OnBackClick()
    {
        loginPanel.SetActive(true);
        gameObject.SetActive(false);
    }

    public void OnRegisterResponse(ReturenCode returenCode)
    {
        if (returenCode==ReturenCode.Success)
        {
            message.text = "注册成功，返回登录";
        }
        else
        {
            message.text = "用户名重复！";
        }
    }
}
