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
    public const string ResourceModelPath = "Model/";
    public GameObject Display3DFish;
    private List<cellDatas> Datas_L_displayCell;
    public Slider slider_horizontal;
	public Slider slider_vertical;
    public GameObject BrushContainer;

    public float oldAngleNumber = 0;

    void Awake()
    {   
        slider_horizontal.maxValue = 360;
        slider_horizontal.minValue = 0;
        slider_horizontal.wholeNumbers = true;
        slider_vertical.maxValue = 360;
        slider_vertical.minValue = 0;
        slider_vertical.wholeNumbers = true;

        slider_horizontal.onValueChanged.AddListener(delegate {
			horizontal(slider_horizontal.value);
		});	
		slider_vertical.onValueChanged.AddListener(delegate {
			vertical(slider_vertical.value);
		}); 
        GetNetworkDatas("");
    }


    private void horizontal(float currentNumber){
        // Debug.Log("horizontal :" + currentNumber);
        GameObject  currentModel = GetModel();
        if(currentModel != null){
            var suitAngle = new Vector3(0,currentNumber,0) - 
            currentModel.transform.rotation.eulerAngles;
            currentModel.transform.Rotate(suitAngle);
       }
	}


	private void vertical(float currentNumber){
        // Debug.Log("vertical :  " + currentNumber);
        GameObject  currentModel = GetModel();
        if(currentModel != null){
            var angle = currentNumber - oldAngleNumber;
            currentModel.transform.Rotate(new Vector3(angle,0,0));
            oldAngleNumber = currentNumber;
       }
	}

    /// <summary>
    /// 得到展示模型
    /// </summary>
    /// <returns></returns>
    private GameObject GetModel(){
        var Display3DFishChildlens = GameObject.FindGameObjectsWithTag("Model");
        if (Display3DFishChildlens.Length > 0){
             var Display3DFishChildlen = Display3DFishChildlens[0];
             return Display3DFishChildlen.gameObject;
        }
        return null;
    }

    private void GetNetworkDatas(string url){
		StartCoroutine(NetWorkGetDatas(url));
	}

	private IEnumerator<object> NetWorkGetDatas(string name)
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
        yield return netDatas_info;
        // Debug.Log(netDatas_info);
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
            var btn = cell.GetComponent<Button>();
            var id = Datas_L_displayCell[i].id;
            if(btn != null){
                btn.onClick.AddListener(() => ListenerCell(id));
            }
          
        }
    }

    private void ListenerCell(int id){
        var Display3DFishChildlens = GameObject.FindGameObjectsWithTag("Model");
        for (var i = 0 ; i < Display3DFishChildlens.Length; i++){
            Destroy(Display3DFishChildlens[i].gameObject);
        }
        for (var i = 0; i<BrushContainer.transform.GetChildCount(); i++)
        {
            Destroy(BrushContainer.transform.GetChild(i).gameObject);
        }
        var model = Resources.Load(ResourceModelPath + "Cube"+id,typeof(GameObject));
        var displayModel = (GameObject)Instantiate(model,Display3DFish.transform);
        
    }
    
}
