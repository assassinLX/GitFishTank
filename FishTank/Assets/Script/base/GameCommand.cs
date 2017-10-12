using strange.extensions.command.impl;  
using strange.extensions.context.api;  
using UnityEngine;  
  
public class GameCommand : EventCommand {  
  
    public override void Execute()  
    {  
        Debug.Log("Start DGame");  
    }  
}  