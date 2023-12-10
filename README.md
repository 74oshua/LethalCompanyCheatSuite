# LethalCompanyTestMod

This mod implements a number of cheats for Lethal Company.

Currently implemented:
  - infinite stamina
  - infinite health
  - battery power is constantly full
  - Emote2 (pointing) will double your movement speed
  - Some enemies will be permanently stunned
  - Turrets will be deactivated
  - The client running the mod cannot die

In order to build, you will need to create a directory `lib/` and add the following files from `Lethal Company/Lethal Company_Data/Managed/` to it:
  - `Assembly-CSharp.dll`
  - `UnityEngine.dll`
  - `UnityEngine.CoreModule.dll`
  - `netstandard.dll`
  - `Unity.Netcode.Runtime.dll`

All cheats are clientside, but Lethal Company doesn't validate data sent from clients, so this mod will function online.

Mod responsibly.
