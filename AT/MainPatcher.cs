using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using UnityEngine;
using AT.Components;
using Photon.Pun;
namespace AT
{
    [HarmonyPatch(typeof(PlayerTumble))]
    public static class PlayerTumblePatch
    {
        [HarmonyPatch("TumbleSet")]
        [HarmonyPrefix]
        public static bool TumbleSetPrefix(PlayerTumble __instance, bool _isTumbling)
        {
            PlayerControl controller = __instance.GetComponentInParent<PlayerControl>();
            if (controller == null)
            {
                return true;
            }

            return true;
        }
    }

    [HarmonyPatch(typeof(PlayerAvatar))]
    public static class PlayerAvatarPatch
    {
        [HarmonyPatch("Start")]
        [HarmonyPostfix]
        public static void StartPostfix(PlayerAvatar __instance)
        {
            if (__instance.GetComponentInParent<PlayerControl>() == null)
            {
                __instance.transform.parent.gameObject.AddComponent<PlayerControl>();
            }
        }
    }

    [HarmonyPatch(typeof(GameplayManager))]
    public static class GameplayManagerPatch
    {
        [HarmonyPatch("Start")]
        [HarmonyPostfix]
        public static void StartPostfix()
        {
            ItemLib.Init();
            if (GameObject.Find("AT")!=null)
            {
                return;
            }
            var go = new GameObject("AT");
            GameObject.DontDestroyOnLoad(go);
            go.AddComponent<PhotonView>();
            go.AddComponent<ObjectLib>();
            go.AddComponent<RightManager>();
            go.AddComponent<MenuManager>();
            ItemLib.Init();
        }
    }

}
