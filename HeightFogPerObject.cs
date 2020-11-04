using Boxophobic.StyledGUI;
using System;
using UnityEngine;


[DisallowMultipleComponent, ExecuteInEditMode, HelpURL("https://docs.google.com/document/d/1pIzIHIZ-cSh2ykODSZCbAPtScJ4Jpuu7lS3rNEHCLbc/edit#heading=h.pzat2b29j9a0")]
public class HeightFogPerObject : StyledMonoBehaviour
{ 
[StyledBanner(0.474f, 0.709f, 0.901f, "Height Fog Per Object", "", "https://docs.google.com/document/d/1pIzIHIZ-cSh2ykODSZCbAPtScJ4Jpuu7lS3rNEHCLbc/edit#heading=h.pzat2b29j9a0")]
public bool styledBanner;

[StyledMessage("Info", "The Object does not have a Mesh Renderer!", 5f, 5f)]
public bool messageNoRenderer;

[StyledMessage("Info", "Objects using multiple materials are not supported!", 5f, 5f)]
public bool messageMultiMaterials;

[StyledMessage("Info", "The Object does not have a Material assigned!", 5f, 5f)]
public bool messageNoMaterial;

[StyledMessage("Info", "Please note that the Height Fog Per Object option will not work for all transparent objects. Available in Play mode only. Please read the documentation for more!", 0f, 0f)]
public bool messageTransparencySupport = true;

[StyledCategory("Settings")]
public bool categoryMaterial;

public Material customFogMaterial;

[StyledMessage("Info", "The is not a valid Height Fog material! Please assign the correct shader first!", 5f, 0f)]
public bool messageInvalidFogMaterial;

[StyledSpace(5)]
public bool styledSpace0;

private int transparencyRenderQueue = 3002;

private Material originalMaterial;

private Material instanceMaterial;

private Material transparencyMaterial;

private GameObject transparencyGO;

private void Awake()
{ 
if (this.GameObjectIsInvalid())
{ 
return;
}
this.transparencyGO = new GameObject(base.gameObject.name + " (Height Fog Object)");
this.transparencyGO.transform.parent = base.gameObject.transform;
this.transparencyGO.transform.localPosition = Vector3.zero;
this.transparencyGO.transform.localRotation = Quaternion.identity;
this.transparencyGO.transform.localScale = Vector3.one;
this.transparencyGO.AddComponent<MeshFilter>();
this.transparencyGO.AddComponent<MeshRenderer>();
this.transparencyGO.GetComponent<MeshFilter>().sharedMesh = base.gameObject.GetComponent<MeshFilter>().sharedMesh;
Material sharedMaterial = base.gameObject.GetComponent<MeshRenderer>().sharedMaterial;
this.instanceMaterial = new Material(sharedMaterial);
this.instanceMaterial.name = sharedMaterial.name + " (Instance)";
if (this.customFogMaterial == null)
{ 
this.transparencyMaterial = new Material(this.instanceMaterial);
this.transparencyMaterial.shader = Shader.Find("BOXOPHOBIC/Atmospherics/Height Fog Per Object");
this.transparencyMaterial.name = sharedMaterial.name + " (Generic Fog)";
}
else if (this.customFogMaterial != null)
{ 
if (this.customFogMaterial.HasProperty("_IsHeightFogShader"))
{ 
this.transparencyMaterial = this.customFogMaterial;
this.transparencyMaterial.name = sharedMaterial.name + " (Custom Fog)";
}
else
{ 
this.transparencyMaterial = new Material(this.instanceMaterial);
this.transparencyMaterial.shader = Shader.Find("BOXOPHOBIC/Atmospherics/Height Fog Per Object");
this.transparencyMaterial.name = sharedMaterial.name + " (Generic Fog)";
}
}
if (this.transparencyMaterial.HasProperty("_IsStandardPipeline"))
{ 
this.transparencyRenderQueue = 3002;
}
else
{ 
this.transparencyRenderQueue = 3102;
}
this.instanceMaterial.renderQueue = this.transparencyRenderQueue;
this.transparencyMaterial.renderQueue = this.transparencyRenderQueue + 1;
base.gameObject.GetComponent<MeshRenderer>().material = this.instanceMaterial;
this.transparencyGO.GetComponent<MeshRenderer>().material = this.transparencyMaterial;
sharedMaterial
}

private bool GameObjectIsInvalid()
{ 
bool result = false;
if (base.gameObject.GetComponent<MeshRenderer>() == null)
{ 
this.messageNoRenderer = true;
result = true;
}
else if (base.gameObject.GetComponent<MeshRenderer>().sharedMaterials.Length > 1)
{ 
this.messageMultiMaterials = true;
result = true;
}
else if (base.gameObject.GetComponent<MeshRenderer>().sharedMaterial == null)
{ 
this.messageNoMaterial = true;
result = true;
}
return result;
result
}
}
