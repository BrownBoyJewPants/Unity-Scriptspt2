using System;
using System.Collections.Generic;
using UnityEngine;


namespace PivecLabs.Tools
{ 
public abstract class ConsoleInputMini
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
InGameConsoleMini.AddCommandsToConsole(this.Command, this);
InGameConsoleMini.AddDescToConsole(this.Description, this);
}

public void ListCommands()
{ 
foreach (KeyValuePair<string, ConsoleInputMini> current in InGameConsoleMini.CommandDesc)
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
