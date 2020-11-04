using System;


namespace PivecLabs.Tools
{ 
public class CommandHelp : ConsoleInput
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

public static CommandHelp CreateCommand()
{ 
return new CommandHelp();
}

public CommandHelp()
{ 
this.Command = "?";
this.Description = string.Format("{0}{1}", this.Command.PadRight(15), "Displays all available commands");
base.AddCommandToConsole();
}

public override void RunCommand()
{ 
base.ListCommands();
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
