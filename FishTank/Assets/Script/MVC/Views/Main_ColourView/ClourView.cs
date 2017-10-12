using System.Collections.Generic;
using strange.extensions.mediation.impl;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class ClourView : View {

	public GameObject L_displayCell;
    public GameObject R_displayModel;

    public Text _text;
    public Button Test_writeButton;
    public Button Test_readBtn;

    void Awake()
    {
        //WriteCellDatas();
        //ReadCellDatas();
    }

    public void WriteCellDatas(){
        cellDatas [] _datas = new cellDatas[3];
        _datas[0].id = 1001;
        _datas[0].imageName = "shark";
        _datas[0].name = "fish001";
        _datas[0].describe = "this is shark";

        _datas[1].id = 1002;
        _datas[1].imageName = "someFish";
        _datas[1].name = "fish002";
        _datas[1].describe = "this is someFish";

        _datas[2].id = 1001;
        _datas[2].imageName = "shark";
        _datas[2].name = "fish001";
        _datas[2].describe = "this is shark";

        FileManager.writeData(_datas,"FishSomething");

    }

    public void ReadCellDatas(){
        List<cellDatas> cell_display_datas = FileManager.LoadGameData<List<cellDatas>>("FishSomething");
        
        if(cell_display_datas == null && cell_display_datas.Count > 0){
            Debug.Log("cell display data is null ");
        }else{
            Debug.Log("cell have datas");
            var celldatas = cell_display_datas.ToArray() ;
            Debug.Log(celldatas[0].describe);
            _text.text = celldatas[0].describe.ToString();
        }
    }

    void Start()
    {
        
    }
	

 


}
