using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameSceneManager : MonoBehaviour {

	public Transform canvas;
	public GameObject _slider;
	private Text text_dispaly;
	private Slider currentSlider;
	
	
	public void changeScene(string name){
		creatSlider_displayLoad();
		StartCoroutine(StartLoadingScene(name));
	}
 
	private void creatSlider_displayLoad(){
		int count = canvas.GetChildCount();
		for(var i = 0; i < count;i++){
			if(canvas.GetChild(i).name == "background"){
				continue;
			}
			canvas.GetChild(i).gameObject.SetActive(false);
		}
		var a = (GameObject)Instantiate(_slider,canvas);
		text_dispaly = a.transform.GetChild(1).transform.GetComponent<Text>();
		currentSlider = a.GetComponent<Slider>();
	}

 	private IEnumerator StartLoadingScene(string scene) {
    	int displayProgress = 0;
    	int toProgress = 0;

    	AsyncOperation op = SceneManager.LoadSceneAsync(scene);
    	op.allowSceneActivation = false;

		while(op.progress < 0.9f) {
			toProgress = (int)op.progress * 100;
			while(displayProgress < toProgress) {
				++displayProgress;
				SetLoadingPercentage(displayProgress);
				yield return new WaitForEndOfFrame();
			}
		}

		toProgress = 96;
		while(displayProgress < toProgress){
			++displayProgress;
			SetLoadingPercentage(displayProgress);
			yield return new WaitForEndOfFrame();
		}
		op.allowSceneActivation = true;
	}

    private void SetLoadingPercentage(int displayProgress)
    {
        currentSlider.value =  (float)displayProgress / 100;
		text_dispaly.text = displayProgress.ToString() + "%";
    }
}
