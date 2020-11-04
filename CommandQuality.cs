using System;
using UnityEngine;


namespace PivecLabs.Tools
{ 
public class CommandQuality : ConsoleInput
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

public static CommandQuality CreateCommand()
{ 
return new CommandQuality();
}

public CommandQuality()
{ 
this.Command = "quality";
this.Description = string.Format("{0}{1}", this.Command.PadRight(15), "Display Application Quality Level");
base.AddCommandToConsole();
}

public override void RunCommand()
{ 
Debug.Log(string.Format("{0}{1}", "Current Quality Level = ", QualitySettings.GetQualityLevel()));
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
