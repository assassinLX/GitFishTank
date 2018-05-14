using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;   

public class SkinView : MonoBehaviour {

    public GameObject ControllBtn;
    public Button SkinBtn;

    [SerializeField]
    protected bool isDisplay;
    [SerializeField]
    protected bool isPlaying;

    public void Awake()
	{
        isDisplay = true;
        isPlaying = false;

        SkinBtn.onClick.AddListener(() => ControllerUI());
        for (int i = 0; i < ControllBtn.transform.childCount;i++){
            var btn = ControllBtn.transform.GetChild(i).GetComponent<Button>();
            btn.onClick.AddListener(() => ChooseColor(btn.name));
        }
	}

    public void ControllerUI(){
        if(isDisplay && isPlaying == false){
            //扩展出去
            StartCoroutine(ControllBtnMove());
        }else if(isPlaying == false){
            //回收回来
            StartCoroutine(ControllBtnCome());
        }
    }

	public void Update()
	{
        SkinBtn.enabled = !isPlaying;
	}

	public IEnumerator ControllBtnMove(){
        isPlaying = true;
        ControllBtn.SetActive(isDisplay);
        float result = 0.0f;
        for (int i = 0; i < ControllBtn.transform.childCount; i++)
        {
            var btn = ControllBtn.transform.GetChild(i);
            var btn_rectTransform = btn.GetComponent<RectTransform>();
            result += btn_rectTransform.sizeDelta.x + 20;
            btn_rectTransform.DOLocalMoveX(btn_rectTransform.anchoredPosition3D.x+result,0.3f);
            yield return new WaitForSeconds(0.3f);
        }
        yield return new WaitForSeconds(0.2f);
        isPlaying = false;
        isDisplay = !isDisplay;

    }

    public IEnumerator ControllBtnCome()
    {
        isPlaying = true;
        for (int i = 0; i < ControllBtn.transform.childCount; i++)
        {
            var btn = ControllBtn.transform.GetChild(i);
            var btn_rectTransform = btn.GetComponent<RectTransform>();
            btn_rectTransform.DOLocalMoveX(-230, 0.3f);
            yield return new WaitForSeconds(0.3f);
        }
        yield return new WaitForSeconds(0.2f);
        ControllBtn.SetActive(isDisplay);
        isPlaying = false;
        isDisplay = !isDisplay;

    }




    protected void ChooseColor(string _name){
        Debug.Log(_name);
       
        if(GameObject.FindWithTag("Model") != null){
            GameObject parent = GameObject.Find("BrushContainer");
            GameObject brushObj = (GameObject)Resources.Load("TexturePainter-Instances/BrushEntity");
            for (int i = 0; i < parent.transform.childCount;i++){
                Destroy(parent.transform.GetChild(i));
            }
            if (_name == "Button (1)")
            {
                brushObj.GetComponent<SpriteRenderer>().color = new Color(150.0f / 255.0f, 89.0f / 255.0f, 33.0f / 255.0f);
            }
            else if (_name == "Button (2)")
            {
                brushObj.GetComponent<SpriteRenderer>().color = new Color(242.0f / 255.0f, 192.0f / 255.0f, 49.0f / 255.0f);
            }
            else if (_name == "Button (3)")
            {
                brushObj.GetComponent<SpriteRenderer>().color = new Color(95.0f / 255.0f, 196.0f / 255.0f, 170.0f / 255.0f);
            }
            else
            {
                brushObj.GetComponent<SpriteRenderer>().color = new Color(240.0f / 255.0f, 141.0f / 255.0f, 48.0f / 255.0f);
            }

            for (float x = -0.45f; x < 0.46f; x += 0.01f)
            {
                for (float y = -0.45f; y < 0.46f; y += 0.01f)
                {
                    var brushClone = GameObject.Instantiate(brushObj, parent.transform);
                    brushClone.transform.localPosition = new Vector3(x, y, 0);
                }
            }
        }
       

    }
}
