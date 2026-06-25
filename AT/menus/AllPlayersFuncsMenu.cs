using AT.Components;
using Mono.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
namespace AT
{
    public class AllPlayersFuncsMenu : Menu
    {
        bool excludeMe = false;
        public AllPlayersFuncsMenu()
        {
            windowHeight = 400;
        }
        public override void Draw()
        {
            ButtonStyle style = MenuManager.style;
            if (GUI.Button(new Rect(style.paddingLR, 20, style.width, style.height), "<color=#FF7F7F>Back</color>"))
            {
                MenuManager.Set("Players");
            }
            mainScroll = GUI.BeginScrollView(
                new Rect(0, 20+ style.height, style.width + style.paddingLR * 2, windowHeight - (20 - style.height)),
                mainScroll,
                new Rect(0, 0, style.width + style.paddingLR * 2, style.paddingVert+(style.height + style.paddingVert) * 5));
            
            int i = 0;
            UILib.ToggleButton(new Rect(style.paddingLR, CalcY(i), style.width, style.height), "Exclude Me", ref excludeMe);
            i++;
            if (GUI.Button(new Rect(style.paddingLR, CalcY(i), style.width, style.height), "Kill"))
            {
                PlayerControl.KillAll(excludeMe? PlayerControl.GetLocalCollection() : null);
            }
            i++;
            if (GUI.Button(new Rect(style.paddingLR, CalcY(i), style.width, style.height), "Revive"))
            {
                PlayerControl.ReviveAll(excludeMe ? PlayerControl.GetLocalCollection() : null);
            }
            i++;
            if (GUI.Button(new Rect(style.paddingLR, CalcY(i), style.width, style.height), "Teleport Here"))
            {
                PlayerControl.TeleportAll(PlayerControl.GetLocal().GetPosition(), PlayerControl.GetLocalCollection());
            }
            i++;
            GUI.EndScrollView();
        }
    }
}