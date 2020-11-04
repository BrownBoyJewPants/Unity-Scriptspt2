using GameCreator.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;


namespace PivecLabs.Tools
{ 
[AddComponentMenu("")]
public class InGameConsoleMini : MonoBehaviour
{ 
public bool adminMode;

public bool disableCommands;

public bool disableUpdates;

public bool disableLogs;

public Canvas consoleCanvas;

public Text consoleText;

public Text inputText;

public InputField consoleInput;

[SerializeField]
public KeyCode selectedKey;

[HideInInspector]
public bool displayCanvas;

[HideInInspector]
public bool showConsole;

public static InGameConsoleMini Instance
{ 
get;
private set;
}

public static Dictionary<string, ConsoleInputMini> Commands
{ 
get;
private set;
}

public static Dictionary<string, ConsoleInputMini> CommandDesc
{ 
get;
private set;
}

private void Awake()
{ 
if (InGameConsoleMini.Instance != null)
{ 
return;
}
InGameConsoleMini.Instance = this;
InGameConsoleMini.Commands = new Dictionary<string, ConsoleInputMini>();
InGameConsoleMini.CommandDesc = new Dictionary<string, ConsoleInputMini>();
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
string str = string.Format("{0}:{1}:{2}", DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
string msg = "[" + str + "]" + logMessage;
base.StartCoroutine(this.AddMessageToConsole(msg));
return;
}
if (type == LogType.Log)
{ 
string str2 = string.Format("{0}:{1}:{2}", DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
string msg2 = "[" + str2 + "]" + logMessage;
base.StartCoroutine(this.AddMessageToConsole(msg2));
}
str
msg
str2
msg2
}

private void CreateCommands()
{ 
CommandMini.CreateCommand();
CommandHelpMini.CreateCommand();
}

public static void AddCommandsToConsole(string _name, ConsoleInputMini _command)
{ 
if (!InGameConsoleMini.Commands.ContainsKey(_name))
{ 
InGameConsoleMini.Commands.Add(_name, _command);
}
}

public static void AddDescToConsole(string _name, ConsoleInputMini _help)
{ 
if (!InGameConsoleMini.CommandDesc.ContainsKey(_name))
{ 
InGameConsoleMini.CommandDesc.Add(_name, _help);
}
}

[IteratorStateMachine(typeof(InGameConsoleMini.<AddMessageToConsole>d__32))]
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
bool flag = false;
string text = input.ToString();
string text2 = text.Remove(0, 1);
if (text.Equals("?"))
{ 
InGameConsoleMini.Commands[text[0].ToString()].RunCommand();
return;
}
if (text2.Length < 1)
{ 
Debug.Log("Event Name Required");
return;
}
string[] subscribedKeys = Singleton<EventDispatchManager>.Instance.GetSubscribedKeys();
for (int i = 0; i < subscribedKeys.Length; i++)
{ 
if (subscribedKeys[i].Equals(text2))
{ 
InGameConsoleMini.Commands[text[0].ToString()].RunCommandwithPar(text2);
flag = false;
break;
}
flag = true;
}
if (flag)
{ 
Debug.Log("Event Name not found");
}
flag
text
text2
subscribedKeys
i
}
}
}
