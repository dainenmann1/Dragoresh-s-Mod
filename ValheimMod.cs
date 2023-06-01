using BepInEx;
using HarmonyLib;
using UnityEngine;
using BepInEx.Configuration;

namespace Dragoresh_s_Mod
{
    [BepInPlugin("org.bepinex.plugins.Dragomod", "Dragomod", version)]
    [BepInProcess("valheim.exe")]
    public class ValheimMod : BaseUnityPlugin
    {
        public const string version = "1.0";
        private readonly Harmony harmony = new Harmony("mod.Dragomod");
        private static ConfigEntry<float> sailForceIncrease;
        private static ConfigEntry<float> tameTimeReduction;
        void Awake()
        {
            sailForceIncrease = Config.Bind<float>("General", "SailForceFactorMultiplier", 1f, "Sail Force Increase");
            harmony.PatchAll();
        }

        //This adds a configuration that can multiply the value of sail force
        [HarmonyPatch(typeof(Ship), "Awake")]
        class ShipSpeed_Patch
        {
            static void Postfix(ref float ___m_sailForceFactor)
            {
                Debug.Log($"Old Speed: {___m_sailForceFactor}");
                ___m_sailForceFactor *= sailForceIncrease.Value;
                Debug.Log($"New Speed: {___m_sailForceFactor}");
            }
        }

        //This adds a configuration that will reduce the time it takes to tame an animal in seconds
        class TameTimeReduction_Patch
        {
            static void Postfix(ref float ___m_tamingTime)
            {
                Debug.Log($"Old tame time: {___m_tamingTime}");
                ___m_tamingTime -= tameTimeReduction.Value;
                Debug.Log($"New tame time: {___m_tamingTime}");
            }
        }
    }
}
