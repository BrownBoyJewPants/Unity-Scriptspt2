using System;
using UnityEngine;


namespace PivecLabs.Tools
{ 
public class CommandTimeScale : ConsoleInput
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

public static CommandTimeScale CreateCommand()
{ 
return new CommandTimeScale();
}

public CommandTimeScale()
{ 
this.Command = "time";
this.Description = string.Format("{0}{1}", this.Command.PadRight(15), "Change the scale at which time passes ( 1 is realtime)");
base.AddCommandToConsole();
}

public override void RunCommand()
{ 
Debug.Log("Parameter Required - from 0.1 to 1 to slowdown , 1 to 100 to speedup");
}

public override void RunCommandwithPar(string name)
{ 
Time.timeScale = float.Parse(name);
Debug.Log(string.Format("{0}{1}", "Time Scale set to = ", Time.timeScale.ToString()));
}

public override void RunCommandwithPars(string name, string value)
{ 
Debug.Log("Only one Parameter Required  - from 0.1 to 1 to slowdown , 1 to 100 to speedup");
}
}
}
