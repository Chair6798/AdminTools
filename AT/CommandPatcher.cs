using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using UnityEngine;
namespace AT
{
    [HarmonyPatch(typeof(ChatManager))]
    public static class ChatManagerPatch
    {
        [HarmonyPatch("MessageSend")]
        [HarmonyPrefix]
        public static bool MessageSendPrefix(ChatManager __instance, ref string ___chatMessage)
        {
            string text = ___chatMessage.Replace("<b>|</b>", string.Empty);
            LogLib.Log("CATCHED MESSAGE!");
            string[] args = text.ToLower().Split(' ');
            try
            {
                switch (args[0])
                {
                    case "/menu":
                        LogLib.Log("MENU CALLED!");
                        switch (args[1])
                        {
                            case "itemspawn":
                                LogLib.Log("ITEMSPAWN CALLED!");
                                MenuManager.Set("ItemSpawn", ConvertLib.pack(int.Parse(args[2])));
                                break;
                        }
                        return false;
                    default:
                        LogLib.Log("MESSAGE ISNT A COMMAND! SKIPPING!");
                        return true;
                }
            }
            catch (Exception e)
            {
                LogLib.Log("ERROR ON PARSING MESSAGE! SKIPPING!");
                return true;
            }
        }
    }
}
