using GameCreator.Core;
using GameCreator.Variables;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


namespace GameCreator.UIComponents
{ 
public class DropDownSavesTMP : MonoBehaviour
{ 
public TMP_Dropdown savesDropdown;

public TextMeshProUGUI dropdownLabel;

public string targetVariable;

private List<string> slots = new List<string>();

private void Start()
{ 
int savesCount = Singleton<SaveLoadManager>.Instance.GetSavesCount();
Debug.Log("get saves count returns" + savesCount);
this.savesDropdown.ClearOptions();
foreach (KeyValuePair<int, SavesData.Profile> current in Singleton<SaveLoadManager>.Instance.savesData.profiles)
{ 
Debug.Log("Save profile ID: " + current.Key);
Debug.Log("Save profile date: " + current.Value.date);
this.slots.Add(current.Value.date.ToString());
}
this.slots.Reverse();
this.savesDropdown.AddOptions(this.slots);
savesCount
enumerator
current
}

private void Update()
{ 
}

public void dropdown_indexchanged()
{ 
float num = (float)this.savesDropdown.value;
VariablesManager.SetGlobal(this.targetVariable, num);
num
}
}
}
