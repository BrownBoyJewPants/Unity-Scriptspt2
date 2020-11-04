using GameCreator.Variables;
using System;
using UnityEngine;


namespace PivecLabs.Tools
{ 
public class CommandLists : ConsoleInput
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

public static CommandLists CreateCommand()
{ 
return new CommandLists();
}

public CommandLists()
{ 
this.Command = "lists";
this.Description = string.Format("{0}{1}", this.Command.PadRight(15), "Displays all List variables");
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
GameObject gameObject = array[i];
ListVariables component = gameObject.GetComponent<ListVariables>();
if (component != null)
{ 
for (int j = 0; j < component.references.Length; j++)
{ 
this.varName = gameObject.transform.parent.name;
int expr_55 = component.references[j].variable.type;
if (expr_55 == 1)
{ 
this.varType = "String";
}
if (expr_55 == 3)
{ 
this.varType = "Bool";
}
if (expr_55 == 2)
{ 
this.varType = "String";
}
if (expr_55 == 3)
{ 
this.varType = "Number";
}
if (expr_55 == 4)
{ 
this.varType = "Color";
}
if (expr_55 == 9)
{ 
this.varType = "GameObject";
}
if (expr_55 == 8)
{ 
this.varType = "Sprite";
}
if (expr_55 == 7)
{ 
this.varType = "Texture2D";
}
if (expr_55 == 5)
{ 
this.varType = "Vector2";
}
if (expr_55 == 6)
{ 
this.varType = "Vector3";
}
this.varValue = component.references[j].variable.Get().ToString();
Debug.Log(string.Format("GameObject: {0}Type: {1}Value: {2}", this.varName.PadRight(20), this.varType.PadRight(10), this.varValue.PadRight(20)));
}
}
}
array
i
gameObject
component
j
expr_55
}
}
}
