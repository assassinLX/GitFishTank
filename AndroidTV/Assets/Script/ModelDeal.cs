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
        Debug.Log("path : "+path);
        var model = (GameObject)Resources.Load(path);
        var CloneModel = Instantiate(model, MainScene.transform);
        CloneModel.transform.localPosition = new Vector3(0,0.9f,5.6f);
        CloneModel.transform.localScale = new Vector3(0.5f,0.5f,0.5f);
            
    }
	
    public void SetBrush(Vector3 position,Color color,Vector3 scale)
    {
        var entity = Instantiate(BrushEntity, BrushContainer.transform);
        var render = entity.GetComponent<SpriteRenderer>();
        entity.transform.localPosition = position;
        render.color = color;
        entity.transform.localScale = scale;
    }
}
