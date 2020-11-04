using System;
using UnityEngine;


namespace PivecLabs.Tools
{ 
public class CommandQuit : ConsoleInput
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

public static CommandQuit CreateCommand()
{ 
return new CommandQuit();
}

public CommandQuit()
{ 
this.Command = "quit";
this.Description = string.Format("{0}{1}", this.Command.PadRight(15), "Quits the application");
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
if (!Application.isEditor)
{ 
Application.Quit();
}
}
}
}
