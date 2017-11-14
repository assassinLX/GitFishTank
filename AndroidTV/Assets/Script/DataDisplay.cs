using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataDisplay : MonoBehaviour {
    public ServerDemo Sever;
    public GameObject Test;
    void Update()
    {
        var data = Sever.myData;
        if(data != null && data.Count > 0)
        {
            for(int i = 0;i < data.Count; i++)
            {
                
                if(data[i].currentRender == false)
                {
                    Debug.Log(data[i].id);
                    DisplayCurrentModel(data[i].id , data[i].positions);
                    data[i].currentRender = true;
                }
            }
        }
     
    }

    private void DisplayCurrentModel(string id, List<Vector3> positions)
    {
        
    }
}
