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



    private void Awake()
    {
        ColorBtn.onClick.AddListener(() => ControllerUI("Color"));
        InstructBtn.onClick.AddListener( () => ControllerUI("Instruct"));
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

