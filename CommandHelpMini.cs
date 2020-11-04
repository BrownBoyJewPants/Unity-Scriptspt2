using GameCreator.Core;
using System;
using UnityEngine;


namespace PivecLabs.Tools
{ 
public class CommandHelpMini : ConsoleInputMini
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

public static CommandHelpMini CreateCommand()
{ 
return new CommandHelpMini();
}

public CommandHelpMini()
{ 
this.Command = "?";
this.Description = string.Format("{0}{1}", this.Command.PadRight(15), "Displays all available events");
base.AddCommandToConsole();
}

public override void RunCommand()
{ 
string[] subscribedKeys = Singleton<EventDispatchManager>.Instance.GetSubscribedKeys();
Debug.Log("Events in Total = " + subscribedKeys.Length);
string[] array = subscribedKeys;
for (int i = 0; i < array.Length; i++)
{ 
string str = array[i];
Debug.Log("Event Commands = \"/" + str + "\"");
}
subscribedKeys
array
i
str
}

public override void RunCommandwithPar(string name)
{ 
this.RunCommand();
}

public override void RunCommandwithPars(string name, string value)
{ 
this.RunCommand();
}
}
}
