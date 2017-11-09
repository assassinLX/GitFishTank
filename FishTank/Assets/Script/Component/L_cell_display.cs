using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class L_cell_display : MonoBehaviour {
	public void display(cellDatas obj,string ResourceImagePath){
		var cell_image = Resources.Load(ResourceImagePath + obj.imageName,typeof(Sprite));
		Debug.Log(ResourceImagePath);
        transform.Find("Image").GetComponent<Image>().sprite = (Sprite)cell_image;
        transform.Find("Name").GetComponent<Text>().text = obj.name;
		transform.Find("Describe").GetComponent<Text>().text = obj.describe;
	}
}
