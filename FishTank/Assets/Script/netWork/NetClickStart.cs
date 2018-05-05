using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NetClickStart : MonoBehaviour {

    public GameObject Content;
    public GameObject option;

    public Button NetClick;
    public InputField currentinput;

	private void Awake()
	{
        NetClick.onClick.AddListener(() => currentClickIp());
        NetUtility.Instance.SetDelegate((string msg) => {
            //设置一下回调
            Debug.Log(msg + "\r\n");
        });
	}

    void currentClickIp(){
        
        if(string.IsNullOrEmpty(currentinput.text) == false){
            Debug.Log("test!");
            NetUtility.Instance.SetIpAddressAndPort(currentinput.text);
            NetUtility.Instance.ClientConnnect();
            settingCurrent();
            SettingManager.instance.isClick = true;
        }
    }

    void settingCurrent(){
        Content.SetActive(false);
        option.SetActive(true);
    }
}
