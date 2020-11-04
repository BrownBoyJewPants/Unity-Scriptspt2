using GameCreator.Core;
using System;
using UnityEngine;


namespace GameCreator.UIComponents
{ 
[AddComponentMenu("")]
public class ConditionMapBool : ICondition
{ 
public MapManager fullscreen;

public GameObject mapManager;

public bool satisfied = true;

public override bool Check()
{ 
this.fullscreen = this.mapManager.GetComponent<MapManager>();
if (this.fullscreen.miniMapshowing)
{ 
this.satisfied = true;
}
else
{ 
this.satisfied = false;
}
return this.satisfied;
}
}
}
