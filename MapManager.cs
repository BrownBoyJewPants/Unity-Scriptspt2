using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;


namespace GameCreator.UIComponents
{ 
public class MapManager : MonoBehaviour
{ 
[CompilerGenerated]
[Serializable]
private sealed class <>c
{ 
public static readonly MapManager.<>c <>9 = new MapManager.<>c();

public static Func<GameObject, bool> <>9__7_0;

internal bool <Update>b__7_0(GameObject obj)
{ 
return obj.name == "MapMarkerLabel";
}
}

public bool miniMapshowing;

public bool fullMapshowing;

public bool miniMapscrollWheel;

public float miniMapscrollWheelSpeed = 5f;

public bool miniMapmouseDrag;

public int miniMapDragSpeed = 1;

public int miniMapDragButton;

private void Update()
{ 
if (this.fullMapshowing || this.miniMapshowing)
{ 
GameObject gameObject = GameObject.Find("MiniMapCamera");
IEnumerable<GameObject> arg_42_0 = Resources.FindObjectsOfTypeAll<GameObject>();
Func<GameObject, bool> arg_42_1;
if ((arg_42_1 = MapManager.<>c.<>9__7_0) == null)
{ 
arg_42_1 = (MapManager.<>c.<>9__7_0 = new Func<GameObject, bool>(MapManager.<>c.<>9.<Update>b__7_0));
}
IEnumerable<GameObject> enumerable = arg_42_0.Where(arg_42_1);
if (enumerable != null)
{ 
foreach (GameObject current in enumerable)
{ 
current.GetComponent<Transform>().eulerAngles = new Vector3(current.transform.eulerAngles.x, gameObject.transform.eulerAngles.y, current.transform.eulerAngles.z);
}
}
}
if (this.fullMapshowing && this.miniMapscrollWheel)
{ 
GameObject.Find("MiniMapCamera").GetComponent<Camera>().orthographicSize += Input.GetAxis("Mouse ScrollWheel") * this.miniMapscrollWheelSpeed;
}
if (this.fullMapshowing && this.miniMapmouseDrag)
{ 
GameObject gameObject2 = GameObject.Find("MiniMapCamera");
if (Input.GetMouseButton(this.miniMapDragButton))
{ 
gameObject2.GetComponent<Camera>().transform.position -= new Vector3(Input.GetAxis("Mouse X") * (float)this.miniMapDragSpeed, 0f, Input.GetAxis("Mouse Y") * (float)this.miniMapDragSpeed);
}
}
gameObject
arg_42_0
arg_42_1
enumerable
enumerator
current
gameObject2
}
}
}
