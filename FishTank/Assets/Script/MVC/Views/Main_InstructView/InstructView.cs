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
        ColorBtn.onClick.AddListener(() => ControllerUI("Color"));
        InstructBtn.onClick.AddListener( () => ControllerUI("Instruct"));
        foreach (var btn in btn_Group)
        {
            btn.onClick.AddListener(() => SendInstruct(btn.name));
        }
    }
    
    void SendInstruct(string currentInstruct)
    {
        currentClient.Connect(currentInstruct);
    }

    void ControllerUI(string name)
    {
        if(name == "Color")
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

