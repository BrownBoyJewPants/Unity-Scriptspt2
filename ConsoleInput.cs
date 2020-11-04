using System;
using System.Collections.Generic;
using UnityEngine;


namespace PivecLabs.Tools
{ 
public abstract class ConsoleInput
{ 
public abstract string Command
{ 
get;
protected set;
}

public abstract string Description
{ 
get;
protected set;
}

public void AddCommandToConsole()
{ 
InGameConsole.AddCommandsToConsole(this.Command, this);
InGameConsole.AddDescToConsole(this.Description, this);
}

public void ListCommands()
{ 
foreach (KeyValuePair<string, ConsoleInput> current in InGameConsole.CommandDesc)
{ 
Debug.Log(current.Key);
}
enumerator
current
}

public abstract void RunCommand();

public abstract void RunCommandwithPar(string name);

public abstract void RunCommandwithPars(string name, string value);
}
}
