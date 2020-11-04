using GameCreator.Core;
using System;
using UnityEngine;


namespace GameCreator.UIComponents
{ 
[AddComponentMenu("")]
public class ConditionInfoBool : ICondition
{ 
public SysInfo infoSwitch;

public GameObject infoPanel;

public bool satisfied = true;

public override bool Check()
{ 
this.infoSwitch = this.infoPanel.GetComponentInChildren<SysInfo>();
if (this.infoSwitch.fpsinfo)
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
