using UnityEngine;
using strange.extensions.mediation.impl;
using UnityEngine.UI;

public class InstructView : View
{
    public Sprite normal;
    public Sprite HeightLight;

    public Button ColorBtn;
    public Button InstructBtn;

    public GameObject[] AboutColorFish;
    public GameObject InstructMode;

    public Button[] btn_Group;
    public ClientDemo currentClient;

     override protected void Awake()
    {
        base.Awake();
        ColorBtn.onClick.AddListener(() => ControllerUI("Color"));
        InstructBtn.onClick.AddListener( () => ControllerUI("Instruct"));
        foreach (var btn in btn_Group)
        {
            btn.onClick.AddListener(() => SendInstruct(btn.name));
        }
    }
    
    void SendInstruct(string currentInstruct)
    {
        if(currentClient != null){
            currentClient.Connect(currentInstruct);
        }else{
            Debug.Log("当前还没有发送颜色数据");
        }
       
    }

    void ControllerUI(string _name)
    {
        Debug.Log("name : "+_name);
        if(_name == "Color")
        {
            ColorBtn.GetComponent<Image>().sprite = HeightLight;
            InstructBtn.GetComponent<Image>().sprite = normal;
            displayColorModel(true);
        }
        else
        {
            ColorBtn.GetComponent<Image>().sprite = normal;
            InstructBtn.GetComponent<Image>().sprite = HeightLight;
            displayColorModel(false);
        }
    }

    void displayColorModel(bool isDisplay)
    {
        for(int i = 0; i < AboutColorFish.Length; i++)
        {
            AboutColorFish[i].SetActive(isDisplay);
        }
        InstructMode.SetActive(!isDisplay);
    }
}

