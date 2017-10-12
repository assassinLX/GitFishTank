using System.Collections;
using System.Collections.Generic;
using strange.extensions.mediation.impl;
using UnityEngine;

public class ClourMediator : EventMediator {

	 [Inject]
	 public ClourView View_clour{get ; set ;}


	public override void OnRegister(){
		 Debug.Log("ClourMediator is OnRegister");
	 }

	public override void OnRemove(){
		 Debug.Log("ClourMediator is OnRemove");
	}
	
	

}
