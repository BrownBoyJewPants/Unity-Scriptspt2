using System;
using UnityEngine;


namespace PivecLabs.Tools
{ 
public class CommandIncreaseQuality : ConsoleInput
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

public static CommandIncreaseQuality CreateCommand()
{ 
return new CommandIncreaseQuality();
}

public CommandIncreaseQuality()
{ 
this.Command = "quality+";
this.Description = string.Format("{0}{1}", this.Command.PadRight(15), "Increase Quality Level with/without expensive changes (true/false) ");
base.AddCommandToConsole();
}

public override void RunCommand()
{ 
Debug.Log("Parameter Required - true or false");
}

public override void RunCommandwithPar(string name)
{ 
QualitySettings.IncreaseLevel(bool.Parse(name));
Debug.Log(string.Format("{0}{1}", "Increase Quality Level with expensive changes - ", name));
Debug.Log(string.Format("{0}{1}", "Quality Level now = ", QualitySettings.GetQualityLevel()));
}

public override void RunCommandwithPars(string name, string value)
{ 
Debug.Log("Only one Parameter Required  - true or false");
}
}
}
