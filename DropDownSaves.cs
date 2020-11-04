using GameCreator.Core;
using GameCreator.Variables;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace GameCreator.UIComponents
{ 
public class DropDownSaves : MonoBehaviour
{ 
public Dropdown savesDropdown;

public Text dropdownLabel;

public string loadSlotVariable;

public string saveSlotVariable;

private int count;

private List<string> slots = new List<string>();

private void Start()
{ 
this.count = Singleton<SaveLoadManager>.Instance.GetSavesCount();
VariablesManager.SetGlobal(this.saveSlotVariable, this.count);
Debug.Log("get saves count returns" + this.count);
this.savesDropdown.ClearOptions();
foreach (KeyValuePair<int, SavesData.Profile> current in Singleton<SaveLoadManager>.Instance.savesData.profiles)
{ 
Debug.Log("Save profile ID: " + current.Key);
Debug.Log("Save profile date: " + current.Value.date);
this.slots.Add(current.Value.date.ToString());
}
this.slots.Reverse();
this.savesDropdown.AddOptions(this.slots);
VariablesManager.SetGlobal(this.loadSlotVariable, this.count);
enumerator
current
}

private void Update()
{ 
}

public void dropdown_indexchanged()
{ 
float num = (float)this.savesDropdown.value;
float num2 = (float)this.count - num;
VariablesManager.SetGlobal(this.loadSlotVariable, num2);
num
num2
}
}
}
