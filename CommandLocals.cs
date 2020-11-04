using GameCreator.Variables;
using System;
using UnityEngine;


namespace PivecLabs.Tools
{ 
public class CommandLocals : ConsoleInput
{ 
private string varType;

private string varName;

private string varValue;

public override string Command
{ 
get;
protected set;
}

public override string Description
{ 
get;
protected set;
}

public static CommandLocals CreateCommand()
{ 
return new CommandLocals();
}

public CommandLocals()
{ 
this.Command = "locals";
this.Description = string.Format("{0}{1}", this.Command.PadRight(15), "Displays all local variables");
base.AddCommandToConsole();
}

public override void RunCommandwithPar(string name)
{ 
this.RunCommand();
}

public override void RunCommandwithPars(string name, string value)
{ 
this.RunCommand();
}

public override void RunCommand()
{ 
GameObject[] array = UnityEngine.Object.FindObjectsOfType<GameObject>();
for (int i = 0; i < array.Length; i++)
{ 
LocalVariables component = array[i].GetComponent<LocalVariables>();
if (!(component is ListVariables) && component != null)
{ 
for (int j = 0; j < component.references.Length; j++)
{ 
this.varName = component.references[j].variable.name;
int expr_5E = component.references[j].variable.type;
if (expr_5E == 1)
{ 
this.varType = "String";
}
if (expr_5E == 3)
{ 
this.varType = "Bool";
}
if (expr_5E == 2)
{ 
this.varType = "String";
}
if (expr_5E == 3)
{ 
this.varType = "Number";
}
if (expr_5E == 4)
{ 
this.varType = "Color";
}
if (expr_5E == 9)
{ 
this.varType = "GameObject";
}
if (expr_5E == 8)
{ 
this.varType = "Sprite";
}
if (expr_5E == 7)
{ 
this.varType = "Texture2D";
}
if (expr_5E == 5)
{ 
this.varType = "Vector2";
}
if (expr_5E == 6)
{ 
this.varType = "Vector3";
}
this.varValue = VariablesManager.GetLocal(component.references[j].gameObject, this.varName, false).ToString();
Debug.Log(string.Format("Name: {0}Type: {1}Value: {2}", this.varName.PadRight(25), this.varType.PadRight(10), this.varValue.PadRight(20)));
}
}
}
array
i
component
j
expr_5E
}
}
}
