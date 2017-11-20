using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelDeal : MonoBehaviour {
    public GameObject MainScene;
    public GameObject BrushContainer;
    public GameObject BrushEntity;
    public string PeopleID;

    public void SetModel(string name)
    {
        var path = "Model/" + name;
        var model = (GameObject)Resources.Load(path);
        var CloneModel = Instantiate(model, MainScene.transform);
        
    }
	
    public void SetBrush(Vector3 position,Color color)
    {
        var entity = Instantiate(BrushEntity, BrushContainer.transform);
        var render = entity.GetComponent<SpriteRenderer>();
        entity.transform.localPosition = position;
        render.color = color;
    }
}
