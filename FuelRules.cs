using System;using System.Globalization;using System.Collections.Generic;using UnityEngine;
namespace Groundworks.FuelFire {
public static class FuelRules {
 public static bool Finite(double x){return !double.IsNaN(x)&&!double.IsInfinity(x);}
 public static double Positive(double x){return Finite(x)?Math.Max(0,x):0;}
 public static float ImpactSeverity(double normalSpeed,double targetMass,double otherMass,double tolerance){
  if(!Finite(normalSpeed)||normalSpeed<=0||!Finite(targetMass)||targetMass<=0||!Finite(tolerance)||tolerance<=0)return 0;
  double fraction=double.IsPositiveInfinity(otherMass)?1:Finite(otherMass)&&otherMass>0?otherMass/(targetMass+otherMass):0;
  double effective=normalSpeed*Math.Sqrt(fraction),threshold=Math.Max(3,tolerance*.3);
  return (float)Math.Max(0,Math.Min(1,(effective-threshold)/tolerance));
 }
 public static bool CanRepair(float severity,bool burning,double temperature,bool engineer,bool nearby,bool kit){return Finite(severity)&&severity>0&&severity<=.35f&&!burning&&Finite(temperature)&&temperature<350&&engineer&&nearby&&kit;}
 public static void ConfigureProfile(ConfigNode n,Dictionary<string,FuelProfile> profiles){
  string name=n.GetValue("name");if(string.IsNullOrEmpty(name))return;FuelProfile p;
  if(!profiles.TryGetValue(name,out p))p=new FuelProfile{name=name,kind=FluidKind.Inert};
  FluidKind kind;bool b;if(Enum.TryParse<FluidKind>(n.GetValue("kind"),true,out kind)&&Enum.IsDefined(typeof(FluidKind),kind))p.kind=kind;
  if(bool.TryParse(n.GetValue("gas"),out b))p.gas=b;if(bool.TryParse(n.GetValue("selfContained"),out b))p.selfContained=b;
  p.oxidizerRatio=Read(n,"oxidizerRatio",p.oxidizerRatio,.01,20);p.ignitionTemperature=Read(n,"ignitionTemperature",p.ignitionTemperature,250,3000);
  p.burnMultiplier=Read(n,"burnMultiplier",p.burnMultiplier,.05,10);p.smoke=(float)Read(n,"smoke",p.smoke,0,5);
  p.flame=ReadColor(n.GetValue("flameColor"),p.flame);p.spray=ReadColor(n.GetValue("sprayColor"),p.spray);profiles[name]=p;
 }
 static Color ReadColor(string text,Color fallback){if(text==null)return fallback;var a=text.Split(',');if(a.Length!=3&&a.Length!=4)return fallback;float[] v={0,0,0,1};for(int i=0;i<a.Length;i++){float f;if(!float.TryParse(a[i],NumberStyles.Float,CultureInfo.InvariantCulture,out f)||!Finite(f))return fallback;v[i]=Mathf.Clamp01(f);}return new Color(v[0],v[1],v[2],v[3]);}
 static double Read(ConfigNode n,string key,double fallback,double min,double max){double x;return double.TryParse(n.GetValue(key),NumberStyles.Float,CultureInfo.InvariantCulture,out x)&&Finite(x)?Math.Max(min,Math.Min(max,x)):fallback;}
}
}
