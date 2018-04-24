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
    public delegate void SettingCurrentDisplayModel(string PeopleID);
    public SettingCurrentDisplayModel SetModel;

	void Start ()
	{
        myData = new List<NewModelData>();
		scrollViewPosition = Vector2.zero;
		//消息委托
		NetUtility.Instance.SetDelegate ((string msg) => {
			Debug.Log ("-"+msg);
		});
        NetUtility.Instance.SetGoodMessage((string msg) =>
        {
            getUniteData(msg);
        });
		//开启服务器
		NetUtility.Instance.ServerStart ();
	}


    private void getUniteData(string unitedate)
    {
        Debug.Log(unitedate);
        UniteData currentData = FileManager.AnalyticData<UniteData>(unitedate);
        if(currentData.isInstruct == UniteData.currentState.colorData)
        {
            CreateModel(currentData.ColorData);
        }
        else
        {
            Debug.Log(currentData.instructData);
        }
    }
    


    void CreateModel(ModelData _data)
    {
        var NewData = new NewModelData();
        NewData.id = _data.id;
        NewData.currentRender = false;
        NewData.PeopleID = _data.IDENTIFICATION;

        //Debug.Log("NewData.PeopleID : -----"+ NewData.PeopleID);
        //Debug.Log("-----------------data id:" + _data.id);

        //Debug.Log("-----------------data positions count :" + _data.positions.Count);
        //Debug.Log("-----------------data color count :" + _data.currentColors.Count);

        for(int i = 0; i < _data.positions.Count ; i++)
        {
            Vector3 position = new Vector3();
            position.x = (float)_data.positions[i].a;
            position.y = (float)_data.positions[i].b;
            position.z = (float)_data.positions[i].c;

            //Debug.Log("--------------- position :" + position);
            NewData.positions.Add(position);

            Color color = new Color();
            color.r = (float)_data.currentColors[i].r;
            color.g = (float)_data.currentColors[i].g;
            color.b = (float)_data.currentColors[i].b;
            color.a = (float)_data.currentColors[i].a;
            NewData.colors.Add(color);

            Vector3 currentScale = new Vector3();
            currentScale.x = (float)_data.scale[i].a;
            currentScale.y = (float)_data.scale[i].b;
            currentScale.z = (float)_data.scale[i].c;
            NewData.scale.Add(currentScale);
        }
        myData.Add(NewData);
        Debug.Log("###########______________############## 当前的数据有几个" + myData.Count);
        ClearDatas(myData);
        Debug.Log("###########______________############## 当前的数据有几个" + myData.Count);
    }

    //清理当前重复的数组
    private void ClearDatas(List<NewModelData> datas)
    {
        for (int i = datas.Count - 1; i >= 0; i--)
        {
            for (int t = i - 1; t >= 0; t--)
            {
                if (datas[i].PeopleID == datas[t].PeopleID)
                {
                    datas.RemoveAt(t);
                    SetModel(datas[t].PeopleID);
                }
            }

        }
    }

 
    
}
