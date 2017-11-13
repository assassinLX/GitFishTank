using UnityEngine;
using System.Collections;
using System.Text;
using System;
using LitJson;

/// <summary>
/// 客户端实现
/// </summary>
public class ClientDemo : MonoBehaviour
{
    public GameObject BrushContainer;
    public string CurrentID = "123456";
    

   
    public void Connect()
    {
        //消息处理
        NetUtility.Instance.SetDelegate((string msg) => {
            Debug.Log(msg + "\r\n");
        });
        //连接服务器
        NetUtility.Instance.ClientConnnect();
        //开启协程
        StartCoroutine(ServerStart());
    }

	IEnumerator ServerStart ()
	{
        yield return new WaitForSeconds(0.1f);
        //编码获取内容
        string content = getContent();
        //内容测试
		Debug.Log (content);
		//待发送对象
		NetModel nm = new NetModel ();
		//消息体
		nm.senderIp = "127.0.0.1";
		nm.content = content;
		nm.time = DateTime.Now.ToString ();
		//发送数据对象
		NetUtility.Instance.SendMsg (nm);
	}

     string getContent()
    {
        ModelData _data = new ModelData();
        var currentModel = GameObject.FindGameObjectWithTag("Model");
        if (currentModel != null)
        {
            CurrentID = currentModel.name;
        }
        _data.id = CurrentID;
        for (int i = 0; i < BrushContainer.transform.GetChildCount(); i++)
        {
            var c = BrushContainer.transform.GetChild(i);
            NewVector _vector = new NewVector();
            _vector.a = (double)c.transform.position.x;
            _vector.b = (double)c.transform.position.y;
            _vector.c = (double)c.transform.position.z;
            _data.positions.Add(_vector);
             
        }

        Debug.Log("current id :"+_data.id);
        foreach (var i in _data.positions)
        {
            Debug.Log("current Data :" +i);
        }
        
        string content = JsonMapper.ToJson(_data); ;
        Debug.Log(content);
        return content;
    }

}
