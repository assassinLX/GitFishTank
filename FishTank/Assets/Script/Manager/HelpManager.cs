using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class HelpManager : MonoBehaviour {

    public Button back;

    public Button introduceBtn;
    public Button proclaimBtn;

    public GameObject introduceGameObject;
    public GameObject proclaimGameObject;

	private void Awake()
	{
        back.onClick.AddListener(() => backStart());
        introduceBtn.onClick.AddListener(() => introduceBtnOnClick());
        proclaimBtn.onClick.AddListener(() => proclaimBtnOnClick());

	}

    private void backStart(){
        SceneManager.LoadScene("star");
    }

    private void introduceBtnOnClick(){
        introduceGameObject.SetActive(true);
        proclaimGameObject.SetActive(false);
    }
    private void proclaimBtnOnClick()
    {
        introduceGameObject.SetActive(false);
        proclaimGameObject.SetActive(true);
    }
}
