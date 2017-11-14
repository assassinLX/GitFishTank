using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewModelData  {

    public string id;
    public List<Vector3> positions;
    public bool currentRender;
    public NewModelData()
    {
        positions = new List<Vector3>();
    }
    
}
