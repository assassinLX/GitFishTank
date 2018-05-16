using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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


        if(Sever.currentIns != null){
            if(Sever.currentIns == "btn001"){
                DisplayFood("FoodAmmonite");
            }else if(Sever.currentIns == "btn002"){
                DisplayFood("laternfish");
            }else{
                DisplayFood("breed_fish");
            }
            Sever.currentIns = null;
        }
     
    }

    private void SettingShouldDisplayModel(string PeopleID)
    {
        isClear = true;
        this.PeopleID = PeopleID;
    }

    

    private void DisplayFood(string _name){
        var path = "Model/Food/" + _name;
        Debug.Log("path : " + path);
        var model = (GameObject)Resources.Load(path);
        var CloneModel = Instantiate(model);
        CloneModel.transform.position = new Vector3(-2.47f, 5.0f, 12.73f);
        StartCoroutine(foodMove(CloneModel));
    }

    IEnumerator foodMove(GameObject cloneModel){
        cloneModel.transform.DOMove(new Vector3(-2.47f, -3.0f, 12.73f), 3.0f);
        yield return new WaitForSeconds(3.0f);
        cloneModel.tag = "Food";
    }

    private void DisplayCurrentModel(string id,string peopleID, List<Vector3> positions,List<Color> colors,List<Vector3> scales)
    {
        var testModel = Instantiate(Test,this.transform);
        var name = id.Substring(0,id.Length - 7);
        ModelDeal deal = testModel.GetComponent<ModelDeal>();
        deal.SetModel(name);
        deal.PeopleID = peopleID;
        for(int i = 0; i < positions.Count; i++)
        {
            deal.SetBrush(positions[i], colors[i],scales[i]);
        }

    }
}
