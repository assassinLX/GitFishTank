using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkDatasManager : MonoBehaviour {

 

	public void GetNetworkDatas(string name){
		StartCoroutine(NetWorkGetDatas(name));
	}

	IEnumerator NetWorkGetDatas(string name){
		string NetworkString = "http://localhost:8888/" + name; 
		WWW www = new WWW (NetworkString);
		yield return www;
		string m_info = string.Empty;
		if(www.error != null){
			m_info = www.error;
			yield return null;
		}
		m_info = www.text;
		Debug.Log (m_info);
	}
}
