using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class L_cell_display : MonoBehaviour {
	public void display(cellDatas obj,string ResourceImagePath){
		var cell_image = Resources.Load(ResourceImagePath + obj.imageName,typeof(Sprite));
		Debug.Log(ResourceImagePath);
        transform.FindChild("Image").GetComponent<Image>().sprite = (Sprite)cell_image;
        transform.FindChild("Name").GetComponent<Text>().text = obj.name;
		transform.FindChild("Describe").GetComponent<Text>().text = obj.describe;
	}
}
