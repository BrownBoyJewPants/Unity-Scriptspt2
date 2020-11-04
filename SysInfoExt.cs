using System;
using UnityEngine;
using UnityEngine.Profiling;
using UnityEngine.UI;


namespace PivecLabs.Tools
{ 
public class SysInfoExt : MonoBehaviour
{ 
public static bool sysinfo = false;

public Text fps;

public Text min;

public Text max;

public Text memalloc;

public Text memtotal;

public Text memgxf;

public GameObject txtgxf;

private float sys_accumulatedTime;

private int sys_accumulatedFrames;

private float sys_lastUpdateTime;

private float sys_frameTime;

private float sys_frameRate;

private float sys_minFrameRate;

private float sys_maxFrameRate;

private float sys_minFrameTime;

private float sys_maxFrameTime;

public static float updateInterval = 0.5f;

public static float minTime = 1E-09f;

public Text os;

public Text type;

public Text graphics;

public Text gfx;

public Text window;

public Text product;

public Text version;

public Text company;

public Text build;

public Text language;

public Text platform;

public Text genuine;

private void Start()
{ 
}

private void Update()
{ 
if (SysInfoExt.sysinfo)
{ 
float unscaledDeltaTime = Time.unscaledDeltaTime;
this.sys_accumulatedTime += unscaledDeltaTime;
this.sys_accumulatedFrames++;
if (unscaledDeltaTime < SysInfoExt.minTime)
{ 
unscaledDeltaTime = SysInfoExt.minTime;
}
if (unscaledDeltaTime < this.sys_minFrameTime)
{ 
this.sys_minFrameTime = unscaledDeltaTime;
}
if (unscaledDeltaTime > this.sys_maxFrameTime)
{ 
this.sys_maxFrameTime = unscaledDeltaTime;
}
float realtimeSinceStartup = Time.realtimeSinceStartup;
if (realtimeSinceStartup - this.sys_lastUpdateTime < SysInfoExt.updateInterval)
{ 
return;
}
if (this.sys_accumulatedTime < SysInfoExt.minTime)
{ 
this.sys_accumulatedTime = SysInfoExt.minTime;
}
if (this.sys_accumulatedFrames < 1)
{ 
this.sys_accumulatedFrames = 1;
}
this.sys_frameTime = this.sys_accumulatedTime / (float)this.sys_accumulatedFrames;
this.sys_frameRate = 1f / this.sys_frameTime;
this.sys_minFrameRate = 1f / this.sys_maxFrameTime;
this.sys_maxFrameRate = 1f / this.sys_minFrameTime;
this.UpdateDisplayContent();
this.ResetCounters();
this.sys_lastUpdateTime = realtimeSinceStartup;
this.os.text = SystemInfo.operatingSystem;
this.type.text = SystemInfo.processorType;
this.graphics.text = SystemInfo.graphicsDeviceName;
this.gfx.text = string.Format("{0} - Shader: {1} - VRam: {2}MB", SystemInfo.graphicsDeviceVersion, SystemInfo.graphicsShaderLevel, SystemInfo.graphicsMemorySize.ToString());
this.window.text = string.Format("Window size: {1} x {2} - {3}dpi {0}Hz ", new object[]
{ 
Screen.currentResolution.refreshRate,
Screen.width,
Screen.height,
Screen.dpi
});
this.product.text = string.Format("{0}{1}", "Product name = ", Application.productName);
this.version.text = string.Format("{0}{1}", "Product version = ", Application.version);
this.company.text = string.Format("{0}{1}", "Company name = ", Application.companyName);
this.build.text = string.Format("{0}{1}", "Unity Build version = ", Application.unityVersion);
this.language.text = string.Format("{0}{1}", "Local Operating System Language = ", Application.systemLanguage);
this.platform.text = string.Format("{0}{1}", "Current Platform = ", Application.platform);
this.genuine.text = string.Format("{0}{1}", "Verified app integrity = ", Application.genuine);
}
unscaledDeltaTime
realtimeSinceStartup
}

private void UpdateDisplayContent()
{ 
this.fps.text = string.Format("{0} - {1} ms", this.sys_frameRate.ToString("F1"), (this.sys_frameTime * 1000f).ToString("F1"));
this.min.text = string.Format("{0} - {1} ms", this.sys_minFrameRate.ToString("F1"), (this.sys_maxFrameTime * 1000f).ToString("F1"));
this.max.text = string.Format("{0} - {1} ms", this.sys_maxFrameRate.ToString("F1"), (this.sys_minFrameTime * 1000f).ToString("F1"));
this.memtotal.text = string.Format("{0:0.0#} MB", SysInfoExt.ConvertBytesToMegabytes(Profiler.GetTotalReservedMemoryLong()));
this.memalloc.text = string.Format("{0:0.0#} MB", SysInfoExt.ConvertBytesToMegabytes(Profiler.GetTotalAllocatedMemoryLong()));
if (Debug.isDebugBuild)
{ 
this.memgxf.text = string.Format("{0:0.0#} MB", SysInfoExt.ConvertBytesToMegabytes(Profiler.GetAllocatedMemoryForGraphicsDriver()));
return;
}
this.memgxf.text = "";
this.txtgxf.SetActive(false);
}

private void ResetCounters()
{ 
this.sys_minFrameTime = 3.40282347E+38f;
this.sys_maxFrameTime = -3.40282347E+38f;
this.sys_accumulatedTime = 0f;
this.sys_accumulatedFrames = 0;
}

private static double ConvertBytesToMegabytes(long bytes)
{ 
return (double)((float)bytes / 1024f / 1024f);
}
}
}
