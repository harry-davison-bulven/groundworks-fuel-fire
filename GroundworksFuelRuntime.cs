using System;using System.IO;using System.Linq;using UnityEngine;
namespace Groundworks.FuelFire {
[KSPAddon(KSPAddon.Startup.MainMenu,true)]public class FuelRuntime:MonoBehaviour {
 public static FuelRuntime Instance;public static bool Enabled=true;public static int Quality=1,RegisteredParts;bool visible;Rect window=new Rect(45,90,330,210);
 string ConfigPath{get{return Path.Combine(KSPUtil.ApplicationRootPath,"GameData/GroundworksFuelFire/PluginData/settings.cfg");}}
 void Awake(){if(Instance!=null){Destroy(gameObject);return;}Instance=this;DontDestroyOnLoad(gameObject);
 if(AppDomain.CurrentDomain.GetAssemblies().Any(a=>a.GetType("Groundworks.Destruction.DestructionRuntime")!=null)){Enabled=false;Debug.LogError("[GW Fuel] Remove GroundworksDestruction before using standalone Fuel & Fire.");return;}
 var path=File.Exists(ConfigPath)?ConfigPath:Path.Combine(KSPUtil.ApplicationRootPath,"GameData/GroundworksFuelFire/Settings.cfg");var cfg=ConfigNode.Load(path);if(cfg!=null){var n=cfg.GetNode("GROUNDWORKS_FUEL_FIRE");bool enabled;int quality;if(n!=null){if(bool.TryParse(n.GetValue("enabled"),out enabled))Enabled=enabled;if(int.TryParse(n.GetValue("quality"),out quality))Quality=Mathf.Clamp(quality,0,2);}}
 FuelProfiles.Load();foreach(var ap in PartLoader.LoadedPartsList){var p=ap.partPrefab;if(p==null||p.isKerbalEVA()||ap.name.StartsWith("kerbalEVA",StringComparison.OrdinalIgnoreCase))continue;try{if(!p.Modules.Contains("GroundworksFuelLeak"))p.AddModule("GroundworksFuelLeak",true);RegisteredParts++;}catch(Exception ex){Debug.LogWarning("[GW Fuel] Skipped "+ap.name+": "+ex.Message);}}
 GameEvents.onPartWillDie.Add(PartWillDie);Debug.Log("[GW Fuel] Standalone fuel/fire ready; ordinary prefabs="+RegisteredParts+"; no custom destruction or Harmony patches.");}
 void PartWillDie(Part p){if(!Enabled||!HighLogic.LoadedSceneIsFlight||p==null||p.isKerbalEVA())return;var leak=p.FindModuleImplementing<GroundworksFuelLeak>();if(leak!=null)leak.ReleaseOnDestruction();}
 void Update(){if(HighLogic.LoadedSceneIsFlight&&Input.GetKeyDown(KeyCode.F8))visible=!visible;}
 void OnGUI(){if(visible&&HighLogic.LoadedSceneIsFlight)window=GUILayout.Window(GetInstanceID(),window,Draw,"Groundworks Fuel & Fire");}
 void Draw(int id){Enabled=GUILayout.Toggle(Enabled,"Enable leaks and fire");GUILayout.Label("Visual quality");Quality=GUILayout.SelectionGrid(Quality,new[]{"Light","Balanced","Detailed"},3);GUILayout.Label("Impact leaks consume real tank resources.\nStock breakup and explosions remain native.");if(GUILayout.Button("Save and close")){Directory.CreateDirectory(Path.GetDirectoryName(ConfigPath));var root=new ConfigNode();var n=root.AddNode("GROUNDWORKS_FUEL_FIRE");n.AddValue("enabled",Enabled);n.AddValue("quality",Quality);root.Save(ConfigPath);visible=false;}GUI.DragWindow();}
 void OnDestroy(){if(Instance!=this)return;GameEvents.onPartWillDie.Remove(PartWillDie);Instance=null;}
}
public class FuelParticleLifetime:MonoBehaviour {public ParticleSystem system;void OnDestroy(){if(system!=null)FloatingOrigin.UnregisterParticleSystem(system);}}
}
