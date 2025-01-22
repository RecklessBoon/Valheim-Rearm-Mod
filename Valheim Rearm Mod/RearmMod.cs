using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Valheim_Rearm_Mod
{
    [BepInPlugin("com.github.recklessboon.valheim.rearmmod", "Rearm Mod", "1.0.2")]
    [BepInProcess("valheim.exe")]
    public class RearmMod : BaseUnityPlugin
    {
        internal static new ManualLogSource Logger;
        private readonly Harmony harmony = new Harmony("com.github.recklessboon.valheim.rearmmod");

        void Awake()
        {
            harmony.PatchAll();
            Logger = base.Logger;
        }

        void OnDestroy()
        {
            harmony?.UnpatchSelf();
            Logger = null;
        }

        [HarmonyPatch(typeof(Humanoid), "ShowHandItems")]
        class Humanoid_Patch
        {
            [HarmonyReversePatch]
            [HarmonyPatch("ShowHandItems")]
            public static void ShowHandItems(Humanoid __instance, bool onlyRightHand = false, bool animation = true) => throw new NotImplementedException("This is a stub only");
        }

        [HarmonyPatch(typeof(Player), "Update")]
        class Update_Patch
        {
            static bool playerWasSwimming = false;

            static void Prefix(Player __instance)
            {
                try
                {
                    if (__instance.IsOwner())
                    {
                        if (!playerWasSwimming && __instance.IsSwimming())
                        {
                            playerWasSwimming = true;
                        }
                        else if (playerWasSwimming && !__instance.IsSwimming() && __instance.IsOnGround())
                        {
                            Humanoid_Patch.ShowHandItems(__instance, false, false);
                            Logger.LogInfo($"Rearming player after swimming");
                            playerWasSwimming = false;
                        }
                    }
                }
                catch (System.Exception e)
                {
                    Logger.LogError("Error in Rearm Mod: " + e);
                }
            }
        }
    }
}
