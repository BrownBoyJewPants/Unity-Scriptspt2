using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace GameCreator.UIComponents
{ 
public class MouseOver : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{ 
public bool overObject;

public bool outlineObject;

public void OnPointerEnter(PointerEventData eventData)
{ 
this.overObject = true;
if (this.outlineObject)
{ 
base.GetComponent<Outline>().enabled = true;
}
}

public void OnPointerExit(PointerEventData eventData)
{ 
this.overObject = false;
base.GetComponent<Outline>().enabled = false;
}
}
}
