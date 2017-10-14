using System.Collections.Generic;
using strange.extensions.mediation.impl;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System.Collections;

public class ClourView : View{

    public Transform L_displayCell_father;
	public GameObject L_displayCell;
    public const string ResourceImagePath = "Image/Fish/";

    public GameObject R_displayModel;
    private List<cellDatas> Datas_L_displayCell;

    void Awake()
    {   
        GetNetworkDatas("");
    }

    private void GetNetworkDatas(string name){
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
        for(int i = 0;i < Datas_L_displayCell.Count;i++){
            var cell = Instantiate(L_displayCell,L_displayCell_father);
            var cell_Component = cell.GetComponent<L_cell_display>();
            if(cell_Component != null){
                cell_Component.display(Datas_L_displayCell[i],ResourceImagePath);
            }
        }
    }


}
