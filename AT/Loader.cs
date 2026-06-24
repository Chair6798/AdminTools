using System;
using BepInEx;
using HarmonyLib;
using UnityEngine;
namespace AT
{
    [BepInPlugin(ModData.UUID, ModData.Name, ModData.Version)]
    public class Loader : BaseUnityPlugin
    {
        void Awake()
        {
            LogLib.Log("Loading...");
            var harmony = new Harmony(ModData.UUID);
            harmony.PatchAll();
            InitConfig();
            LogLib.Log("Loaded!");
        }
        void InitConfig()
        {
            Cfg.menuToggleKey = Config.Bind<KeyCode>("Binds", "MenuToggle", KeyCode.F10);
        }
    }
}
