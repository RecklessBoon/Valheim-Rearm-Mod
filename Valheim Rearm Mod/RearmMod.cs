using BepInEx;
using HarmonyLib;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Valheim_Rearm_Mod
{
    [BepInPlugin("com.github.recklessboon.valheim.rearmmod", "Rearm Mod", "1.0.0")]
    [BepInProcess("valheim.exe")]
    public class RearmMod : BaseUnityPlugin
    {
        private readonly Harmony harmony = new Harmony("com.github.recklessboon.valheim.rearmmod");

        void Awake()
        {
            harmony.PatchAll();
        }

        [HarmonyPatch(typeof(Player), "Update")]
        class Update_Patch
        {
            static bool wasSwimming = false;
            static ItemDrop.ItemData rightItem = null;
            static ItemDrop.ItemData leftItem = null;

            static void Prefix(Player __instance)
            {
                try
                {
                    if (!wasSwimming && __instance.IsSwimming())
                    {
                        Debug.Log("Player is swimming. Caching items.");
                        rightItem = __instance.RightItem;
                        leftItem = __instance.LeftItem;
                    }
                    else if (wasSwimming && !__instance.IsSwimming())
                    {
                        Debug.Log("Player is no longer swimming. Rearming items.");
                        __instance.EquipItem(rightItem, false);
                        __instance.EquipItem(leftItem, false);
                        rightItem = null;
                        leftItem = null;
                    }
                    wasSwimming = __instance.IsSwimming();
                }
                catch (System.Exception e)
                {
                    Debug.LogError("Error in Rearm Mod: " + e);
                }
        }
    }


    }
}
