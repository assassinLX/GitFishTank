﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelData{
    public string id;
    public List<NewVector> positions;
    public List<NewColor> currentColors;
    public List<NewVector> scale;
    public string IDENTIFICATION;

    public ModelData()
    {
        positions = new List<NewVector>();
        currentColors = new List<NewColor>();
        scale = new List<NewVector>();
    }
}

public class NewVector
{
    public double a;
    public double b;
    public double c;
}

public class NewColor
{
    public double r;
    public double g;
    public double b;
    public double a;
}
