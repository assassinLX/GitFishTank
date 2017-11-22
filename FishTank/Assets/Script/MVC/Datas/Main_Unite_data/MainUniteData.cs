using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUniteData{

    public enum currentState
    {
        colorData,
        instructData
    }

    public currentState isInstruct;

    public ModelData ColorData;
    public string instructData;

}
