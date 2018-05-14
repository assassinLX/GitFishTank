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
    }
}
