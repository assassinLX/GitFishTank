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
    public bool isClick = false;
    public void Connect()
    {
        if (isClick == false)
        {
            //消息处理
            NetUtility.Instance.SetDelegate((string msg) => {
                Debug.Log(msg + "\r\n");
            });
            //连接服务器
            NetUtility.Instance.ClientConnnect();
            //开启协程
            StartCoroutine(ServerStart());
            isClick = true;
        }else
        {
            StartCoroutine(ServerStart());
        }
       
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

        if(transform.GetComponent<IDENTIFICATION>().CurrentIdentification != null)
        {
            _data.IDENTIFICATION = transform.GetComponent<IDENTIFICATION>().CurrentIdentification;
        }

        for (int i = 0; i < BrushContainer.transform.GetChildCount(); i++)
        {
            var c = BrushContainer.transform.GetChild(i);
            NewVector _vector = new NewVector();
            _vector.a = (double)c.transform.localPosition.x;
            _vector.b = (double)c.transform.localPosition.y;
            _vector.c = (double)c.transform.localPosition.z;
            _data.positions.Add(_vector);
            NewColor _color = new NewColor();
            var render = c.transform.GetComponent<SpriteRenderer>();
            _color.r = (double)render.color.r;
            _color.g = (double)render.color.g;
            _color.b = (double)render.color.b;
            _color.a = (double)render.color.a;
            _data.currentColors.Add(_color);
        }
        Debug.Log("current id :"+_data.id);
        foreach (var i in _data.positions)
        {
            Debug.Log("current Data - position :" +i);
        }

        foreach (var i in _data.currentColors)
        {
            Debug.Log("current Data color :" + i);
        }
        string content = JsonMapper.ToJson(_data);
        Debug.Log(content);
        return content;
    }

}
