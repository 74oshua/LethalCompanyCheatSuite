using BepInEx;
using HarmonyLib;
using GameNetcodeStuff;

namespace LethalCompanyCheatSuite
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    [BepInProcess("Lethal Company.exe")]
    public class Plugin : BaseUnityPlugin
    {
        private void Awake()
        {
            // Plugin startup logic
            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");

            // patch
            Harmony.CreateAndPatchAll(typeof(PlayerPatches));
            Harmony.CreateAndPatchAll(typeof(ItemPatches));
        }
    }

    public class PlayerPatches
    {
        // Emote2 speed hack
        [HarmonyPatch(typeof(PlayerControllerB), "Emote2_performed")]
        [HarmonyPrefix]
        static void Emote2_performed(PlayerControllerB __instance)
        {
            UnityEngine.Debug.Log("Toggling movement speed from " + __instance.movementSpeed);
            if (__instance.movementSpeed <= 4.6f)
            {
                __instance.movementSpeed = 10f;
            }
            else
            {
                __instance.movementSpeed = 4.6f;
            }
            UnityEngine.Debug.Log("Speed is now " + __instance.movementSpeed);
        }

        // infinite sprint
        [HarmonyPatch(typeof(PlayerControllerB), "LateUpdate")]
        [HarmonyPostfix]
        static void PlayerLateUpdatePostfix(PlayerControllerB __instance)
        {
            __instance.sprintMeter = 1f;
        }

        // infinite health
        [HarmonyPatch(typeof(PlayerControllerB), "DamagePlayer")]
        [HarmonyPrefix]
        static void DamagePlayerPrefix(PlayerControllerB __instance)
        {
            __instance.health = 100;
        }

        // disable death
        [HarmonyPatch(typeof(PlayerControllerB), "AllowPlayerDeath")]
        [HarmonyPostfix]
        static void AllowPlayerDeathPostfix(ref bool __result)
        {
            __result = false;
        }

        // disable turrets
        [HarmonyPatch(typeof(Turret), "Update")]
        [HarmonyPrefix]
        static void TurretUpdatePrefix(Turret __instance)
        {
            __instance.turretActive = false;
        }

        // stunned enemies (only works on entities that use EnemyAI)
        [HarmonyPatch(typeof(EnemyAI), "Update")]
        [HarmonyPrefix]
        static void EnemyLateUpdatePrefix(ref EnemyAI __instance)
        {
            __instance.SetEnemyStunned(true);
        }

        public class CustomGrabbable : GrabbableObject
        {
            public CustomGrabbable(Item item)
            {
                itemProperties = item;
            }
        }
    }

    public class ItemPatches
    {
        // Infinite charge
        [HarmonyPatch(typeof(GrabbableObject), "Update")]
        [HarmonyPostfix]
        static void GrabbableObjectUpdatePostfix(ref GrabbableObject __instance)
        {
            if (__instance.insertedBattery != null)
            {
                __instance.insertedBattery.charge = 1f;
            }
        }
    }
}
