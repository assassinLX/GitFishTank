using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataDisplay : MonoBehaviour {

    public ServerDemo Sever;
    public GameObject Test;

    public bool isClear;
    public string PeopleID = null;


    private void Awake()
    {
        Sever.SetModel = SettingShouldDisplayModel;
        isClear = false;
    }

    void Update()
    {
        var data = Sever.myData;
        if(data == null || data.Count == 0)
        {
            return;
        }

        if(data != null && data.Count > 0)
        {
            if (isClear == true) {
                for (int i = transform.GetChildCount() - 1; i >= 0 ;i--)
                {
                    if(transform.GetChild(i).GetComponent<ModelDeal>().PeopleID == PeopleID)
                    {
                        Destroy(transform.GetChild(i).gameObject);
                        isClear = false;
                    }
                }
            }

            for (int i = 0;i < data.Count; i++)
            {
                if(data[i].currentRender == false)
                {
                    Debug.Log(data[i].id);
                    DisplayCurrentModel(data[i].id ,data[i].PeopleID, data[i].positions,data[i].colors,data[i].scale);
                    data[i].currentRender = true;
                }
            }
        }
     
    }

    private void SettingShouldDisplayModel(string PeopleID)
    {
        isClear = true;
        this.PeopleID = PeopleID;
    }

    private void DisplayCurrentModel(string id,string peopleID, List<Vector3> positions,List<Color> colors,List<Vector3> scales)
    {
        var testModel = Instantiate(Test,this.transform);
        var name = id.Substring(0,8);
        ModelDeal deal = testModel.GetComponent<ModelDeal>();
        deal.SetModel(name);
        deal.PeopleID = peopleID;
        for(int i = 0; i < positions.Count; i++)
        {
            deal.SetBrush(positions[i], colors[i],scales[i]);
        }

    }
}
