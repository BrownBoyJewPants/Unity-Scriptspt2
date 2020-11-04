using Boxophobic.StyledGUI;
using System;
using UnityEngine;


[ExecuteInEditMode, HelpURL("https://docs.google.com/document/d/1pIzIHIZ-cSh2ykODSZCbAPtScJ4Jpuu7lS3rNEHCLbc/edit#heading=h.hd5jt8lucuqq"), RequireComponent(typeof(BoxCollider))]
public class HeightFogOverride : StyledMonoBehaviour
{ 
[StyledBanner(0.474f, 0.709f, 0.901f, "Height Fog Override", "", "https://docs.google.com/document/d/1pIzIHIZ-cSh2ykODSZCbAPtScJ4Jpuu7lS3rNEHCLbc/edit#heading=h.hd5jt8lucuqq")]
public bool styledBanner;

[StyledMessage("Info", "The Height Fog Global object is missing from your scene! Please add it before using the Height Fog Override component!", 5f, 0f)]
public bool messageNoHeightFogGlobal;

[StyledCategory("Settings")]
public bool categorySettings;

[Tooltip("Choose if the fog settings are set on game start or updated in realtime for animation purposes.")]
public FogUpdateMode updateMode;

[StyledCategory("Volume")]
public bool categoryVolume;

public float volumeDistanceFade = 3f;

[Range(0f, 1f)]
public float volumeVisibility = 0.2f;

[StyledCategory("Fog")]
public bool categoryFog;

[Tooltip("Shareable fog preset material.")]
public Material fogPreset;

[HideInInspector]
public Material fogPresetOld;

[StyledMessage("Info", "The is not a valid Fog Preset material! Please assign the correct shader first!", 10f, 0f)]
public bool messageInvalidPreset;

[Range(0f, 1f), Space(10f)]
public float fogIntensity = 1f;

[Space(10f)]
public FogAxisMode fogAxisMode = FogAxisMode.YAxis;

[ColorUsage(false, true)]
public Color fogColor = new Color(0f, 1f, 0f, 1f);

public float fogDistanceStart;

public float fogDistanceEnd = 30f;

public float fogHeightStart;

public float fogHeightEnd = 5f;

[StyledCategory("Skybox")]
public bool categotySkybox;

[Range(0f, 1f)]
public float skyboxFogHeight = 0.5f;

[Range(0f, 1f)]
public float skyboxFogFill;

[StyledCategory("Directional")]
public bool categoryDirectional;

public FogDirectionalMode directionalMode;

[Range(0f, 1f)]
public float directionalIntensity = 1f;

[ColorUsage(false, true)]
public Color directionalColor = new Color(1f, 0.75f, 0.5f, 1f);

[StyledCategory("Noise")]
public bool categoryNoise;

public FogNoiseMode noiseMode;

[Range(0f, 1f)]
public float noiseIntensity = 1f;

public float noiseDistanceEnd = 60f;

public float noiseScale = 1f;

public Vector3 noiseSpeed = new Vector3(0f, 0f, 0f);

[StyledSpace(5)]
public bool styledSpace0;

[HideInInspector]
public bool firstTime = true;

[HideInInspector]
public bool upgradedTo100;

private Material localMaterial;

private Collider volumeCollider;

private HeightFogGlobal globalFog;

private Camera cam;

private bool distanceSent;

private void Start()
{ 
this.volumeCollider = base.GetComponent<Collider>();
this.volumeCollider.isTrigger = true;
if (GameObject.Find("Height Fog Global") != null)
{ 
GameObject gameObject = GameObject.Find("Height Fog Global");
this.globalFog = gameObject.GetComponent<HeightFogGlobal>();
if (!this.upgradedTo100)
{ 
this.directionalMode = this.globalFog.directionalMode;
this.noiseMode = this.globalFog.noiseMode;
this.upgradedTo100 = true;
}
this.messageNoHeightFogGlobal = false;
}
else
{ 
this.messageNoHeightFogGlobal = true;
}
this.localMaterial = new Material(Shader.Find("BOXOPHOBIC/Atmospherics/Height Fog Preset"));
this.localMaterial.name = "Local";
this.SetLocalMaterial();
gameObject
}

private void Update()
{ 
this.GetCamera();
if (this.cam == null || this.globalFog == null)
{ 
return;
}
if (!Application.isPlaying || this.updateMode == FogUpdateMode.Realtime)
{ 
this.SetLocalMaterial();
}
Vector3 position = this.cam.transform.position;
Vector3 b = this.volumeCollider.ClosestPoint(position);
float num = Vector3.Distance(position, b);
if (num > this.volumeDistanceFade && !this.distanceSent)
{ 
this.globalFog.overrideCamToVolumeDistance = float.PositiveInfinity;
this.distanceSent = true;
return;
}
if (num < this.volumeDistanceFade)
{ 
this.globalFog.overrideMaterial = this.localMaterial;
this.globalFog.overrideCamToVolumeDistance = num;
this.globalFog.overrideVolumeDistanceFade = this.volumeDistanceFade;
this.distanceSent = false;
}
position
b
num
}

private void OnDrawGizmos()
{ 
Gizmos.color = new Color(this.fogColor.r, this.fogColor.g, this.fogColor.b, this.volumeVisibility);
Gizmos.DrawCube(base.transform.position, new Vector3(base.transform.lossyScale.x, base.transform.lossyScale.y, base.transform.lossyScale.z));
Gizmos.DrawCube(base.transform.position, new Vector3(base.transform.lossyScale.x + this.volumeDistanceFade * 2f, base.transform.lossyScale.y + this.volumeDistanceFade * 2f, base.transform.lossyScale.z + this.volumeDistanceFade * 2f));
}

private void GetCamera()
{ 
this.cam = null;
if (Camera.current != null)
{ 
this.cam = Camera.current;
}
if (Camera.main != null)
{ 
this.cam = Camera.main;
}
}

private void SetPresetToScript()
{ 
this.fogIntensity = this.fogPreset.GetFloat("_FogIntensity");
if (this.fogPreset.GetInt("_FogAxisMode") == 0)
{ 
this.fogAxisMode = FogAxisMode.XAxis;
}
else if (this.fogPreset.GetInt("_FogAxisMode") == 1)
{ 
this.fogAxisMode = FogAxisMode.YAxis;
}
else if (this.fogPreset.GetInt("_FogAxisMode") == 2)
{ 
this.fogAxisMode = FogAxisMode.ZAxis;
}
this.fogColor = this.fogPreset.GetColor("_FogColor");
this.fogDistanceStart = this.fogPreset.GetFloat("_FogDistanceStart");
this.fogDistanceEnd = this.fogPreset.GetFloat("_FogDistanceEnd");
this.fogHeightStart = this.fogPreset.GetFloat("_FogHeightStart");
this.fogHeightEnd = this.fogPreset.GetFloat("_FogHeightEnd");
this.skyboxFogHeight = this.fogPreset.GetFloat("_SkyboxFogHeight");
this.skyboxFogFill = this.fogPreset.GetFloat("_SkyboxFogFill");
this.directionalColor = this.fogPreset.GetColor("_DirectionalColor");
this.directionalIntensity = this.fogPreset.GetFloat("_DirectionalIntensity");
this.noiseIntensity = this.fogPreset.GetFloat("_NoiseIntensity");
this.noiseDistanceEnd = this.fogPreset.GetFloat("_NoiseDistanceEnd");
this.noiseScale = this.fogPreset.GetFloat("_NoiseScale");
this.noiseSpeed = this.fogPreset.GetVector("_NoiseSpeed");
if (this.fogPreset.GetInt("_DirectionalMode") == 1)
{ 
this.directionalMode = FogDirectionalMode.On;
}
else
{ 
this.directionalMode = FogDirectionalMode.Off;
}
if (this.fogPreset.GetInt("_NoiseMode") == 2)
{ 
this.noiseMode = FogNoiseMode.Procedural3D;
return;
}
this.noiseMode = FogNoiseMode.Off;
}

private void SetLocalMaterial()
{ 
this.localMaterial.SetFloat("_FogIntensity", this.fogIntensity);
if (this.fogAxisMode == FogAxisMode.XAxis)
{ 
this.localMaterial.SetInt("_FogAxisMode", 0);
}
else if (this.fogAxisMode == FogAxisMode.YAxis)
{ 
this.localMaterial.SetInt("_FogAxisMode", 1);
}
else if (this.fogAxisMode == FogAxisMode.ZAxis)
{ 
this.localMaterial.SetInt("_FogAxisMode", 2);
}
this.localMaterial.SetColor("_FogColor", this.fogColor);
this.localMaterial.SetFloat("_FogDistanceStart", this.fogDistanceStart);
this.localMaterial.SetFloat("_FogDistanceEnd", this.fogDistanceEnd);
this.localMaterial.SetFloat("_FogHeightStart", this.fogHeightStart);
this.localMaterial.SetFloat("_FogHeightEnd", this.fogHeightEnd);
this.localMaterial.SetFloat("_SkyboxFogHeight", this.skyboxFogHeight);
this.localMaterial.SetFloat("_SkyboxFogFill", this.skyboxFogFill);
this.localMaterial.SetFloat("_DirectionalIntensity", this.directionalIntensity);
this.localMaterial.SetColor("_DirectionalColor", this.directionalColor);
this.localMaterial.SetFloat("_NoiseIntensity", this.noiseIntensity);
this.localMaterial.SetFloat("_NoiseDistanceEnd", this.noiseDistanceEnd);
this.localMaterial.SetFloat("_NoiseScale", this.noiseScale);
this.localMaterial.SetVector("_NoiseSpeed", this.noiseSpeed);
if (this.directionalMode == FogDirectionalMode.On)
{ 
this.localMaterial.SetInt("_DirectionalMode", 1);
this.localMaterial.SetFloat("_DirectionalModeBlend", 1f);
}
else
{ 
this.localMaterial.SetInt("_DirectionalMode", 0);
this.localMaterial.SetFloat("_DirectionalModeBlend", 0f);
}
if (this.noiseMode == FogNoiseMode.Procedural3D)
{ 
this.localMaterial.SetInt("_NoiseMode", 2);
this.localMaterial.SetFloat("_NoiseModeBlend", 1f);
return;
}
this.localMaterial.SetInt("_NoiseMode", 0);
this.localMaterial.SetFloat("_NoiseModeBlend", 0f);
}
}
