using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelData{
    public string id;
    public List<NewVector> positions;
    public ModelData()
    {
        positions = new List<NewVector>();
    }
}

public class NewVector
{
    public double a;
    public double b;
    public double c;
}

