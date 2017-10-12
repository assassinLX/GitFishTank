using System;
using System.IO;
using UnityEngine;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using LitJson;
public static class FileManager{
 
    private const string gameDataFileName = "/"; 
    
/// <summary>
/// 写入json格式的数据
/// </summary>
/// <param name="jsonData"></param>
/// <param name="name"></param>
    public static void writeData<T>(T jsonData,string name){
        string json = JsonMapper.ToJson (jsonData);
		string filePath = Application.persistentDataPath + gameDataFileName + name + ".json";
		Debug.Log (filePath);
		File.WriteAllText (filePath,json);
	}


    
/// <summary>
/// 读取数据返回该数据
/// </summary>
/// <param name="name"></param>
/// <returns></returns>
    public static T LoadGameData<T>(string name) where T : new()
    {
        string filePath = Application.persistentDataPath + gameDataFileName + name + ".json";
        Debug.Log (filePath);
        T loadData;
        if (File.Exists(filePath)){  ///表示文件是否存在
			string dataAsJson = File.ReadAllText(filePath);
            loadData = JsonMapper.ToObject<T>(dataAsJson);
        }else{
            loadData = new T();
        }
        return loadData;
    }

  
}  