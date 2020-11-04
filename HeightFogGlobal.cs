using Boxophobic.StyledGUI;
using System;
using UnityEngine;


[ExecuteInEditMode, RequireComponent(typeof(MeshRenderer)), RequireComponent(typeof(MeshFilter))]
public class HeightFogGlobal : StyledMonoBehaviour
{ 
[StyledBanner(0.474f, 0.709f, 0.901f, "Height Fog Global", "", "https://docs.google.com/document/d/1pIzIHIZ-cSh2ykODSZCbAPtScJ4Jpuu7lS3rNEHCLbc/edit#heading=h.kfvqsi6kusw4")]
public bool styledBanner;

[StyledCategory("Update")]
public bool categoryUpdate;

[Tooltip("Choose if the fog settings are set on game start or updated in realtime for animation purposes.")]
public FogUpdateMode updateMode;

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
public Color fogColor = new Color(0.5f, 0.75f, 1f, 1f);

public float fogDistanceStart;

public float fogDistanceEnd = 30f;

public float fogHeightStart;

public float fogHeightEnd = 5f;

[StyledCategory("Skybox")]
public bool categorySkybox;

[Range(0f, 1f)]
public float skyboxFogHeight = 0.5f;

[Range(0f, 1f)]
public float skyboxFogFill;

[StyledCategory("Directional")]
public bool categoryDirectional;

[SerializeField]
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
public Material heightFogMaterial;

[HideInInspector]
public Material blendMaterial;

[HideInInspector]
public Material localMaterial;

[HideInInspector]
public Material overrideMaterial;

[HideInInspector]
public float overrideCamToVolumeDistance = 1f;

[HideInInspector]
public float overrideVolumeDistanceFade;

[HideInInspector]
public float updater;

private Camera cam;

private void Awake()
{ 
base.gameObject.name = "Height Fog Global";
base.gameObject.transform.position = Vector3.zero;
base.gameObject.transform.rotation = Quaternion.identity;
this.GetCamera();
if (this.cam != null)
{ 
this.SetFogSphereSize();
this.SetFogSpherePosition();
this.cam.depthTextureMode = DepthTextureMode.Depth;
}
else
{ 
Debug.Log("[Atmospheric Height Fog] Camera not found! Make sure you have a camera in the scene or your camera has the MainCamera tag!");
}
GameObject expr_78 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
Mesh sharedMesh = expr_78.GetComponent<MeshFilter>().sharedMesh;
UnityEngine.Object.DestroyImmediate(expr_78);
base.gameObject.GetComponent<MeshFilter>().sharedMesh = sharedMesh;
this.localMaterial = new Material(Shader.Find("BOXOPHOBIC/Atmospherics/Height Fog Preset"));
this.localMaterial.name = "Local";
this.SetLocalMaterial();
this.overrideMaterial = new Material(this.localMaterial);
this.overrideMaterial.name = "Override";
this.blendMaterial = new Material(this.localMaterial);
this.blendMaterial.name = "Blend";
this.heightFogMaterial = new Material(Shader.Find("Hidden/BOXOPHOBIC/Atmospherics/Height Fog Global"));
this.heightFogMaterial.name = "Height Fog Global";
this.RenderPipelineSetTransparentQueue();
base.gameObject.GetComponent<MeshRenderer>().sharedMaterial = this.heightFogMaterial;
this.SetGlobalShader();
this.SetGlobalKeywords();
expr_78
sharedMesh
}

private void OnEnable()
{ 
base.gameObject.GetComponent<MeshRenderer>().enabled = true;
}

private void OnDisable()
{ 
base.gameObject.GetComponent<MeshRenderer>().enabled = false;
Shader.DisableKeyword("AHF_ENABLED");
}

private void OnDestroy()
{ 
Shader.DisableKeyword("AHF_ENABLED");
}

private void Update()
{ 
if (base.gameObject.name != "Height Fog Global")
{ 
base.gameObject.name = "Height Fog Global";
}
if (this.cam == null)
{ 
Debug.Log("[Atmospheric Height Fog] Make sure you set scene camera tag to Main Camera for the fog to work!");
return;
}
this.SetFogSphereSize();
this.SetFogSpherePosition();
if (!Application.isPlaying || this.updateMode == FogUpdateMode.Realtime)
{ 
this.SetLocalMaterial();
}
if (this.overrideCamToVolumeDistance > this.overrideVolumeDistanceFade)
{ 
this.blendMaterial.CopyPropertiesFromMaterial(this.localMaterial);
}
else if (this.overrideCamToVolumeDistance < this.overrideVolumeDistanceFade)
{ 
float t = 1f - this.overrideCamToVolumeDistance / this.overrideVolumeDistanceFade;
this.blendMaterial.Lerp(this.localMaterial, this.overrideMaterial, t);
}
this.SetGlobalShader();
this.SetGlobalKeywords();
t
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

private void SetGlobalShader()
{ 
Shader.SetGlobalFloat("AHF_FogIntensity", this.blendMaterial.GetFloat("_FogIntensity"));
if (this.blendMaterial.GetInt("_FogAxisMode") == 0)
{ 
Shader.SetGlobalVector("AHF_FogAxisOption", new Vector4(1f, 0f, 0f, 0f));
}
else if (this.blendMaterial.GetInt("_FogAxisMode") == 1)
{ 
Shader.SetGlobalVector("AHF_FogAxisOption", new Vector4(0f, 1f, 0f, 0f));
}
else if (this.blendMaterial.GetInt("_FogAxisMode") == 2)
{ 
Shader.SetGlobalVector("AHF_FogAxisOption", new Vector4(0f, 0f, 1f, 0f));
}
Shader.SetGlobalColor("AHF_FogColor", this.blendMaterial.GetColor("_FogColor"));
Shader.SetGlobalFloat("AHF_FogDistanceStart", this.blendMaterial.GetFloat("_FogDistanceStart"));
Shader.SetGlobalFloat("AHF_FogDistanceEnd", this.blendMaterial.GetFloat("_FogDistanceEnd"));
Shader.SetGlobalFloat("AHF_FogHeightStart", this.blendMaterial.GetFloat("_FogHeightStart"));
Shader.SetGlobalFloat("AHF_FogHeightEnd", this.blendMaterial.GetFloat("_FogHeightEnd"));
Shader.SetGlobalFloat("AHF_SkyboxFogHeight", this.blendMaterial.GetFloat("_SkyboxFogHeight"));
Shader.SetGlobalFloat("AHF_SkyboxFogFill", this.blendMaterial.GetFloat("_SkyboxFogFill"));
Shader.SetGlobalFloat("AHF_DirectionalModeBlend", this.blendMaterial.GetFloat("_DirectionalModeBlend"));
Shader.SetGlobalColor("AHF_DirectionalColor", this.blendMaterial.GetColor("_DirectionalColor"));
Shader.SetGlobalFloat("AHF_DirectionalIntensity", this.blendMaterial.GetFloat("_DirectionalIntensity"));
Shader.SetGlobalFloat("AHF_NoiseModeBlend", this.blendMaterial.GetFloat("_NoiseModeBlend"));
Shader.SetGlobalFloat("AHF_NoiseIntensity", this.blendMaterial.GetFloat("_NoiseIntensity"));
Shader.SetGlobalFloat("AHF_NoiseDistanceEnd", this.blendMaterial.GetFloat("_NoiseDistanceEnd"));
Shader.SetGlobalFloat("AHF_NoiseScale", this.blendMaterial.GetFloat("_NoiseScale"));
Shader.SetGlobalVector("AHF_NoiseSpeed", this.blendMaterial.GetVector("_NoiseSpeed"));
}

private void SetGlobalKeywords()
{ 
Shader.EnableKeyword("AHF_ENABLED");
if (this.blendMaterial.GetFloat("_DirectionalModeBlend") > 0f)
{ 
Shader.DisableKeyword("AHF_DIRECTIONALMODE_OFF");
Shader.EnableKeyword("AHF_DIRECTIONALMODE_ON");
}
else
{ 
Shader.DisableKeyword("AHF_DIRECTIONALMODE_ON");
Shader.EnableKeyword("AHF_DIRECTIONALMODE_OFF");
}
if (this.blendMaterial.GetFloat("_NoiseModeBlend") > 0f)
{ 
Shader.DisableKeyword("AHF_NOISEMODE_OFF");
Shader.EnableKeyword("AHF_NOISEMODE_PROCEDURAL3D");
return;
}
Shader.DisableKeyword("AHF_NOISEMODE_PROCEDURAL3D");
Shader.EnableKeyword("AHF_NOISEMODE_OFF");
}

private void SetFogSphereSize()
{ 
float num = this.cam.farClipPlane - 1f;
base.gameObject.transform.localScale = new Vector3(num, num, num);
num
}

private void SetFogSpherePosition()
{ 
base.transform.position = this.cam.transform.position;
}

private void RenderPipelineSetTransparentQueue()
{ 
if (this.heightFogMaterial.HasProperty("_IsStandardPipeline"))
{ 
this.heightFogMaterial.renderQueue = 3001;
return;
}
this.heightFogMaterial.renderQueue = 3101;
}
}
