using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using strange.extensions.context.impl;  
using strange.extensions.command.impl;  
using strange.extensions.context.api;  
public class GameContext : MVCSContext {

	public GameContext(MonoBehaviour view) : base(view){

	}

	protected override void mapBindings(){
		base.mapBindings();
		commandBinder.Bind(ContextEvent.START).To<GameCommand>().Once();
		mediationBinder.Bind<ClourView>().To<ClourMediator>();
        mediationBinder.Bind<InstructView>().To<InstructMediator>();
	}
}
