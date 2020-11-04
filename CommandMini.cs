using GameCreator.Core;
using System;
using UnityEngine;


namespace PivecLabs.Tools
{ 
public class CommandMini : ConsoleInputMini
{ 
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

public static CommandMini CreateCommand()
{ 
return new CommandMini();
}

public CommandMini()
{ 
this.Command = "/";
this.Description = string.Format("{0}{1}", this.Command.PadRight(15), "Dispatches a Game Creator Event");
base.AddCommandToConsole();
}

public override void RunCommandwithPar(string name)
{ 
Singleton<EventDispatchManager>.Instance.Dispatch(name, InGameConsoleMini.Instance.consoleCanvas.gameObject);
Debug.Log(name + " Event Dispatched");
}

public override void RunCommand()
{ 
Debug.Log("Event Name Required");
}

public override void RunCommandwithPars(string name, string value)
{ 
Debug.Log("Event Name Required");
}
}
}
