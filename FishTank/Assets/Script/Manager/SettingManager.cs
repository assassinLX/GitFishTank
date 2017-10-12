using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingManager : MonoBehaviour {

	public static SettingManager instance;
 
	void Awake(){
		if(SettingManager.instance == null){
			SettingManager.instance = this;
			DontDestroyOnLoad(this.gameObject);
		}
	}

	 

}
