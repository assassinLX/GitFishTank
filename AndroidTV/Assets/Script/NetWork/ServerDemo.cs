using UnityEngine;
using System.Collections;
using System.IO;
using System;
using System.Threading;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Text;
/// <summary>
/// 服务器实现.
/// </summary>
public class ServerDemo : MonoBehaviour
{

    public Text IPtext;
	Vector2 scrollViewPosition;
    public List<NewModelData> myData;
    public delegate void SettingCurrentDisplayModel(string PeopleID);
    public SettingCurrentDisplayModel SetModel;

    public string currentIns;

	void Start ()
	{
        myData = new List<NewModelData>();
		scrollViewPosition = Vector2.zero;
		//消息委托
		NetUtility.Instance.SetDelegate ((string msg) => {
            UnityEngine.Debug.Log ("-"+msg);
		});
        NetUtility.Instance.SetGoodMessage((string msg) =>
        {
            getUniteData(msg);
        });
		//开启服务器
		NetUtility.Instance.ServerStart ();
        IPtext.text = "请链接IP："+GetLocalIP();
        currentIns = null;
	}


    private void getUniteData(string unitedate)
    {
        UnityEngine.Debug.Log(unitedate);
        UniteData currentData = FileManager.AnalyticData<UniteData>(unitedate);
        if(currentData.isInstruct == UniteData.currentState.colorData)
        {
            CreateModel(currentData.ColorData);
        }
        else
        {
            UnityEngine.Debug.Log(currentData.instructData);
            currentIns = currentData.instructData;
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
        UnityEngine.Debug.Log("###########______________############## 当前的数据有几个" + myData.Count);
        ClearDatas(myData);
        UnityEngine.Debug.Log("###########______________############## 当前的数据有几个" + myData.Count);
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





    /// <summary>  
    /// 获取当前使用的IP  
    /// </summary>  
    /// <returns></returns>  
    public static string GetLocalIP()
    {
        string result = RunApp("route", "print", true);
        Match m = Regex.Match(result, @"0.0.0.0\s+0.0.0.0\s+(\d+.\d+.\d+.\d+)\s+(\d+.\d+.\d+.\d+)");
        if (m.Success)
        {
            return m.Groups[2].Value;
        }
        else
        {
            try
            {
                System.Net.Sockets.TcpClient c = new System.Net.Sockets.TcpClient();
                c.Connect("www.baidu.com", 80);
                string ip = ((System.Net.IPEndPoint)c.Client.LocalEndPoint).Address.ToString();
                c.Close();
                return ip;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }

    /// <summary>  
    /// 获取本机主DNS  
    /// </summary>  
    /// <returns></returns>  
    public static string GetPrimaryDNS()
    {
        string result = RunApp("nslookup", "", true);
        Match m = Regex.Match(result, @"\d+\.\d+\.\d+\.\d+");
        if (m.Success)
        {
            return m.Value;
        }
        else
        {
            return null;
        }
    }

    /// <summary>  
    /// 运行一个控制台程序并返回其输出参数。  
    /// </summary>  
    /// <param name="filename">程序名</param>  
    /// <param name="arguments">输入参数</param>  
    /// <returns></returns>  
    public static string RunApp(string filename, string arguments, bool recordLog)
    {
        try
        {
            if (recordLog)
            {
                Trace.WriteLine(filename + " " + arguments);
            }
            Process proc = new Process();
            proc.StartInfo.FileName = filename;
            proc.StartInfo.CreateNoWindow = true;
            proc.StartInfo.Arguments = arguments;
            proc.StartInfo.RedirectStandardOutput = true;
            proc.StartInfo.UseShellExecute = false;
            proc.Start();

            using (System.IO.StreamReader sr = new System.IO.StreamReader(proc.StandardOutput.BaseStream, Encoding.Default))
            {
                //string txt = sr.ReadToEnd();  
                //sr.Close();  
                //if (recordLog)  
                //{  
                //    Trace.WriteLine(txt);  
                //}  
                //if (!proc.HasExited)  
                //{  
                //    proc.Kill();  
                //}  
                //上面标记的是原文，下面是我自己调试错误后自行修改的  
                Thread.Sleep(100);           //貌似调用系统的nslookup还未返回数据或者数据未编码完成，程序就已经跳过直接执行  
                                             //txt = sr.ReadToEnd()了，导致返回的数据为空，故睡眠令硬件反应  
                if (!proc.HasExited)         //在无参数调用nslookup后，可以继续输入命令继续操作，如果进程未停止就直接执行  
                {                            //txt = sr.ReadToEnd()程序就在等待输入，而且又无法输入，直接掐住无法继续运行  
                    proc.Kill();
                }
                string txt = sr.ReadToEnd();
                sr.Close();
                if (recordLog)
                    Trace.WriteLine(txt);
                return txt;
            }
        }
        catch (Exception ex)
        {
            Trace.WriteLine(ex);
            return ex.Message;
        }
    }


 
    
}
