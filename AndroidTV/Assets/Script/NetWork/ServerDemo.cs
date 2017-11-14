using UnityEngine;
using System.Collections;
using System.IO;
using System;
using System.Threading;
using System.Collections.Generic;
/// <summary>
/// 服务器实现.
/// </summary>
public class ServerDemo : MonoBehaviour
{
 
	Vector2 scrollViewPosition;
    public List<NewModelData> myData;


	void Start ()
	{
        myData = new List<NewModelData>();
		scrollViewPosition = Vector2.zero;
		//消息委托
		NetUtility.Instance.SetDelegate ((string msg) => {
			//Debug.Log ("-"+msg);
		});
        NetUtility.Instance.SetGoodMessage((string msg) =>
        {
            CreateModel(msg);
        });
		//开启服务器
		NetUtility.Instance.ServerStart ();
	}

    void CreateModel(string msg)
    {
        Debug.Log("-----------#########----------------"+msg);
        var _data = FileManager.AnalyticData<ModelData>(msg);

        var NewData = new NewModelData();
        NewData.id = _data.id;
        NewData.currentRender = false;
        Debug.Log("-----------------data id:" + _data.id);
        Debug.Log("-----------------data positions count :" + _data.positions.Count);
        for(int i = 0; i < _data.positions.Count ; i++)
        {
            Vector3 position = new Vector3();
            position.x = (float)_data.positions[i].a;
            position.y = (float)_data.positions[i].b;
            position.z = (float)_data.positions[i].c;
            Debug.Log("--------------- position :" + position);
            NewData.positions.Add(position);
        }

        myData.Add(NewData);
    }


}
