using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewModelData  {

    public string PeopleID;
    public string id;
    public bool currentRender;

    public List<Vector3> positions;
    public List<Color> colors;
    public List<Vector3> scale;

    public NewModelData()
    {
        positions = new List<Vector3>();
        colors = new List<Color>();
        scale = new List<Vector3>();
    }
    
}
