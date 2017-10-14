using System.Collections.Generic;
using strange.extensions.mediation.impl;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System.Collections;

public class ClourView : View{

	public GameObject L_displayCell;
    public GameObject R_displayModel;

    private List<cellDatas> Datas_L_displayCell;


    public Text _text;


    void Awake()
    {   
        GetNetworkDatas("");
    }

    public void GetNetworkDatas(string name){
		StartCoroutine(NetWorkGetDatas(name));
	}

	private IEnumerator NetWorkGetDatas(string name)
    {
        string NetworkString = "http://localhost:8888/" + name;
        WWW www = new WWW(NetworkString);
        yield return www;
        string netDatas_info = string.Empty;
        if (www.error != null)
        {
            netDatas_info = www.error;
            yield return null;
        }
        netDatas_info = www.text;
        Debug.Log(netDatas_info);
        AnalyticData(netDatas_info);
    }

    private  void AnalyticData(string info)
    {
        Datas_L_displayCell = FileManager.AnalyticData<List<cellDatas>>(info);
        _text.text = Datas_L_displayCell[0].id.ToString();
    }








}
