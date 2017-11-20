using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IDENTIFICATION : MonoBehaviour {
    public const string name = "IDENTIFICATION";
    public string CurrentIdentification;

    public void Awake()
    {
        var currentFile = FileManager.LoadGameData<SelfID>(name);

        if (currentFile.id == null)
        {
            Debug.Log("没有数据");
            CurrentIdentification = (Random.Range(1000,9999) * Random.Range(1000, 9999) + Random.Range(0,1000)).ToString();
            Debug.Log("current range number :" + CurrentIdentification);
            SelfID myID = new SelfID();
            myID.id = CurrentIdentification;
            FileManager.writeData<SelfID>(myID, name);

        }else
        {
            Debug.Log("有数据");
            CurrentIdentification = currentFile.id;
            Debug.Log(CurrentIdentification);
        }
    }
}

public class SelfID {
    public string id;
}