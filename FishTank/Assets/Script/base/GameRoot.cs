using System.Collections;
using System.Collections.Generic;
using strange.extensions.context.impl;
using UnityEngine;

public class GameRoot : ContextView {
	void Awake(){
		context = new GameContext(this);
	}
}
