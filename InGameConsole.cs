using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;


namespace PivecLabs.Tools
{ 
[AddComponentMenu("")]
public class InGameConsole : MonoBehaviour
{ 
public bool adminMode;

public bool disableCommands;

public bool disableUpdates;

public bool disableLogs;

public Canvas consoleCanvas;

public GameObject sysinfoPanel;

public Text consoleText;

public Text inputText;

public InputField consoleInput;

[SerializeField]
public KeyCode selectedKey;

[HideInInspector]
public bool showSysinfo;

public bool displayCanvas;

[HideInInspector]
public bool showConsole;

public static InGameConsole Instance
{ 
get;
private set;
}

public static Dictionary<string, ConsoleInput> Commands
{ 
get;
private set;
}

public static Dictionary<string, ConsoleInput> CommandDesc
{ 
get;
private set;
}

private void Awake()
{ 
if (InGameConsole.Instance != null)
{ 
return;
}
this.sysinfoPanel.SetActive(false);
InGameConsole.Instance = this;
InGameConsole.Commands = new Dictionary<string, ConsoleInput>();
InGameConsole.CommandDesc = new Dictionary<string, ConsoleInput>();
}

private void Start()
{ 
this.consoleCanvas.gameObject.SetActive(false);
this.CreateCommands();
}

private void Update()
{ 
if (Input.GetKeyDown(this.selectedKey))
{ 
if (!this.showConsole)
{ 
this.showConsole = true;
}
else
{ 
this.showConsole = false;
}
}
if (this.showConsole)
{ 
this.displayCanvas = true;
this.ShowConsole();
return;
}
if (!this.showConsole)
{ 
this.displayCanvas = false;
this.HideConsole();
}
}

private void OnEnable()
{ 
Application.logMessageReceived += new Application.LogCallback(this.HandleLog);
}

private void OnDisable()
{ 
Application.logMessageReceived -= new Application.LogCallback(this.HandleLog);
}

private void HandleLog(string logMessage, string stackTrace, LogType type)
{ 
if (!this.disableLogs)
{ 
string text = string.Format("{0}:{1}:{2}", DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
string msg = string.Concat(new string[]
{ 
"[",
text,
"] [",
type.ToString(),
"] ",
logMessage
});
base.StartCoroutine(this.AddMessageToConsole(msg));
return;
}
if (type == LogType.Log)
{ 
string text2 = string.Format("{0}:{1}:{2}", DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
string msg2 = string.Concat(new string[]
{ 
"[",
text2,
"] [",
type.ToString(),
"] ",
logMessage
});
base.StartCoroutine(this.AddMessageToConsole(msg2));
}
text
msg
text2
msg2
}

private void CreateCommands()
{ 
CommandHelp.CreateCommand();
CommandGlobals.CreateCommand();
CommandGlobalsUpdate.CreateCommand();
CommandLocals.CreateCommand();
CommandLists.CreateCommand();
CommandEvent.CreateCommand();
CommandEvents.CreateCommand();
CommandTargetFrameRate.CreateCommand();
CommandVSync.CreateCommand();
CommandQuality.CreateCommand();
CommandIncreaseQuality.CreateCommand();
CommandDecreaseQuality.CreateCommand();
CommandTimeScale.CreateCommand();
CommandQuit.CreateCommand();
}

public static void AddCommandsToConsole(string _name, ConsoleInput _command)
{ 
if (!InGameConsole.Commands.ContainsKey(_name))
{ 
InGameConsole.Commands.Add(_name, _command);
}
}

public static void AddDescToConsole(string _name, ConsoleInput _help)
{ 
if (!InGameConsole.CommandDesc.ContainsKey(_name))
{ 
InGameConsole.CommandDesc.Add(_name, _help);
}
}

[IteratorStateMachine(typeof(InGameConsole.<AddMessageToConsole>d__34))]
private IEnumerator AddMessageToConsole(string msg)
{ 
int num;
while (num == 0)
{ 
yield return new WaitForEndOfFrame();
}
if (num != 1)
{ 
yield break;
}
Text expr_3F = this.consoleText;
expr_3F.text = expr_3F.text + msg + "\n";
this.ClearInput();
yield break;
num
expr_3F
}

public void ClearMessages()
{ 
this.consoleText.text = "";
}

public void EndEdit()
{ 
if (this.inputText.text != "")
{ 
base.StartCoroutine(this.AddMessageToConsole(this.inputText.text));
this.ParseInput(this.inputText.text);
}
}

public void ClearInput()
{ 
this.consoleInput.text = "";
}

public void ShowSysInfo()
{ 
if (this.consoleText.enabled)
{ 
this.consoleText.enabled = false;
this.sysinfoPanel.SetActive(true);
SysInfoExt.sysinfo = true;
return;
}
this.consoleText.enabled = true;
this.sysinfoPanel.SetActive(false);
SysInfoExt.sysinfo = false;
}

public void ShowConsole()
{ 
this.consoleCanvas.gameObject.SetActive(true);
}

public void HideConsole()
{ 
this.consoleCanvas.gameObject.SetActive(false);
}

private void ParseInput(string input)
{ 
string[] array = input.Split(null);
if (array.Length == 0 || array == null)
{ 
Debug.LogWarning("Input not Valid");
return;
}
if (!InGameConsole.Commands.ContainsKey(array[0]))
{ 
Debug.LogWarning("Input not Valid");
Debug.LogWarning("Type ? for Valid commands");
return;
}
if (array.Length > 1)
{ 
if (this.disableUpdates)
{ 
Debug.LogWarning("Updates Disabled");
return;
}
if (array.Length < 3)
{ 
InGameConsole.Commands[array[0]].RunCommandwithPar(array[1]);
}
if (array.Length > 2)
{ 
InGameConsole.Commands[array[0]].RunCommandwithPars(array[1], array[2]);
return;
}
}
else
{ 
if (!this.disableCommands)
{ 
InGameConsole.Commands[array[0]].RunCommand();
return;
}
Debug.LogWarning("Commands Disabled");
}
array
}
}
}
