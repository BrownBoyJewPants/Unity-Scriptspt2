using System;
using UnityEngine;


namespace PivecLabs.Tools
{ 
public class CommandTargetFrameRate : ConsoleInput
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

public static CommandTargetFrameRate CreateCommand()
{ 
return new CommandTargetFrameRate();
}

public CommandTargetFrameRate()
{ 
this.Command = "frames";
this.Description = string.Format("{0}{1}", this.Command.PadRight(15), "Set the Application target frame rate");
base.AddCommandToConsole();
}

public override void RunCommand()
{ 
Debug.Log("Parameter Required");
}

public override void RunCommandwithPar(string name)
{ 
Application.targetFrameRate = int.Parse(name);
Debug.Log("Application Target Frame Rate set to " + name);
}

public override void RunCommandwithPars(string name, string value)
{ 
Debug.Log("Only one Parameter Required");
}
}
}
