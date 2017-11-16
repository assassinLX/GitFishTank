using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewModelData  {

    public string id;
    public bool currentRender;

    public List<Vector3> positions;
    public List<Color> colors;
    

    public NewModelData()
    {
        positions = new List<Vector3>();
        colors = new List<Color>();
    }
    
}
