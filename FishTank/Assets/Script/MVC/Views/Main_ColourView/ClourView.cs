using System.Collections.Generic;
using strange.extensions.mediation.impl;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class ClourView : View {

	public GameObject L_displayCell;
    public GameObject R_displayModel;

    public NetworkDatasManager networkDatasManager;

    public Text _text;
    public Button Test_writeButton;
    public Button Test_readBtn;

    void Awake()
    {
         networkDatasManager.GetNetworkDatas("");
    }


}
