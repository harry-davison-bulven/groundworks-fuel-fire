# Groundworks Fuel & Fire 1.0.0

Fuel leaks, finite spills and fuel-dependent fire for KSP 1.12.5. No external mod dependencies or custom parts. Tested on Windows. MIT license.

## New in v1

- Collision severity accounts for effective impact mass and normal contact speed. A tiny object does not automatically damage a massive tank as much as terrain does at the same speed.
- Residual vapour disperses or finishes burning after a leak stops. Final traces extinguish and clean up their effects while preserving the model's mass ledger.
- Separate controls for leak simulation, fire and repair availability; adjustable tank-leak difficulty from 0.25x to 3x. Visual quality changes particle budgets, not tank leakage or spill simulation lifetime.
- A stock-toolbar button and F8 settings window show active leak, fire, mass-flow and spill counts.
- Minor damage can be patched by an EVA engineer within three metres of the leak, using one stock EVA repair kit. The tank must be below 350 K, unlit, and have severity at most 0.35. A patch stops new leakage; it does not replace lost fuel or erase residual vapour. Severe damage requires another solution. Cockpit activation cannot bypass these requirements.
- Custom resource profiles support fuel/oxidizer mass ratio, ignition temperature, burn factor, smoke and spray/flame colours. Partial overrides retain unspecified defaults. Malformed values are bounded or ignored.

## Installation

Close KSP and merge the release's GameData folder into the game. Replace the existing GroundworksFuelFire.dll rather than installing two versions. Restart KSP. Preserve your PluginData/settings.cfg if you have preferences; alpha settings remain readable and missing v1 options receive defaults. Groundworks Flight Computer is optional and independent. Remove the old GroundworksDestruction plugin before using this replacement.

Open the flame toolbar button or press F8 in flight. Save and close stores preferences. The part action window shows tank-local leakage and remaining fuel. Turning off crossfeed does not patch a hole. Repairs use a kit from the active EVA engineer's inventory.

## Behaviour and limits

Leaking removes actual local tank resources. Cold leaks remain unlit until the model receives an ignition source; fuel, ambient oxygen or an abstract stored oxidizer/self-contained reaction determine whether custom fire can burn. Empty and oxidizer-only tanks do not create a custom fuel fire. Native stock breakup and minimum explosion effects remain unchanged.

Chemistry, ignition temperatures, impacts and heat transfer are gameplay approximations. Tank pressure, liquid slosh, structural cracking, water interaction and persistent orbital liquid parcels are outside this model. Ordinary EVA Kerbals are excluded from automatic module registration.

Simulation pauses when a vessel is packed or above 4x warp; this version does not provide background leak catch-up. Part leak/cloud state is saved. Rupture spills are temporary local effects, limited to 64 simulated spills with a 60-second lifetime, and are cleared on scene changes or high warp. Visual quality separately limits displayed sources and particles; simulation may continue without an admitted visual source. Disabling the entire simulation clears local spill effects.

## Validation

The native suite uses stock probe, tank and cargo hardware in a private uncrewed save. It checks ordinary prefab registration, inventory item consumption, denial of cockpit repairs, custom GameDatabase profiles, quantity-dependent depletion and fire, negative cases, fire-off mode, native rupture, physical external contact and a live captured saved-state reload. Model regressions cover finite mass accounting, sealed residual clouds, configurable mixture ratios, nonfinite inputs, mass-aware contacts and repair eligibility. Settings use native ConfigNode serialization. The suite does not claim a complete live EVA engineer walkthrough, arbitrary-mod-pack certification or validation of real fuel chemistry.

Source is in the repository root. Build.ps1 compiles against the managed assemblies of your own KSP installation. KSP/game assemblies, QA harness binaries and third-party plugins are not included in the mod archive. Test sources are excluded from the production build.

To uninstall, close KSP and remove only GameData/GroundworksFuelFire. Existing module records may remain in saved games. Back up saves before changing mods.
