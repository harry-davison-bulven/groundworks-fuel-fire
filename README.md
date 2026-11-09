# Groundworks Fuel & Fire

Impact-driven fuel leaks, finite spills and quantity-dependent fire for ordinary KSP tanks.

Version **0.1.0-alpha.1**, experimental alpha for **KSP 1.12.5**. Tested on Windows. MIT license. No parts pack is required.

Impact contacts can puncture a tank. Leaks decrease its actual local resources; connected intact tanks are not drained by the leak. Fuel spray and fire scale with actual released mass. Ignition needs fuel, an abstract oxygen supply and heat or sparks. Empty and oxidizer-only tanks do not create a custom fuel fire. Rupture transfers remaining resources once into a finite, temporary local spill. F8 opens the enable/quality window; Save and close writes PluginData/settings.cfg.

No custom parts, mesh dents, fragments, building destruction or Harmony patches are included. Native KSP breakup and explosion effects remain, including stock minimum effects on an empty tank. This is an abstract gameplay model: no real chemistry, pressure/slosh, or persistent orbital liquid parcels. Spills are not saved as vessels; local effects pause/clear when packed, changing scenes or above 4x warp. Existing GroundworksDestruction must be removed before using this standalone replacement. Profiles can be extended using GROUNDWORKS_FLUID nodes.

## Install

Download `GroundworksFuelFire-0.1.0-alpha.1.zip` from Releases, then merge its GameData folder into KSP with the game closed. Do not install the source archive as a mod. Restart KSP. The two standalone plugins may be installed independently or together. Neither requires the other.

To uninstall, close KSP and remove only `GameData/GroundworksFuelFire`. Keep a backup of your saves. No stock files or save files need editing. Plugin settings or module records can remain in existing saves after removal.

## Validation and limitations

`NATIVE-TESTS.txt` records controlled stock-only integration tests using an uncrewed probe and three stock tanks. Fuel tests cover isolated depletion, fill-dependent flow, ignition, negative cases, visuals, saved module state, native rupture transfer and a physical external contact. Flight-computer checks cover stock-probe readiness, SAS hold, abort and rejection of a grounded tank bench. Model/math unit checks are included separately. Prior automatic flight development used other craft; this alpha does not claim a fresh end-to-end stock launch/landing qualification or certification of arbitrary mod packs.

## Source and build

Production C# source is in this repository root. `Build.ps1 -KspRoot <path>` builds against your locally installed KSP managed assemblies using the Windows .NET Framework C# compiler. The KSP SDK/game DLLs and test harness binaries are not redistributed. Test source files are excluded from the production build. `GroundworksFuelFire-Source-0.1.0-alpha.1.zip` is also attached to the release.

## Distribution status

GitHub is the canonical release source. CurseForge and CKAN listings require separate submissions; metadata in this repository does not itself mean either listing is live.
