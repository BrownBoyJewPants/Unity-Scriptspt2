using System;
using UnityEngine;


namespace GameCreator.UIComponents
{ 
public class FrameRotate : MonoBehaviour
{ 
private Transform target;

public bool rotating;

private void Start()
{ 
this.target = GameObject.FindGameObjectWithTag("Player").transform;
}

private void Update()
{ 
if (this.rotating)
{ 
Vector3 eulerAngles = default(Vector3);
eulerAngles.z = this.target.transform.eulerAngles.y;
base.transform.eulerAngles = eulerAngles;
}
eulerAngles
}
}
}
