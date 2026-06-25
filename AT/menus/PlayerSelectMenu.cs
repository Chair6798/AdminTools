using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using AT.Components;
namespace AT
{
    public class PlayerSelectMenu : Menu
    {
        Action<PlayerControl> action;
        Action back;
        public PlayerSelectMenu(Action<PlayerControl> act, Action bck)
        {
            action = act;
            back = bck;
        }
        public override void Draw()
        {
            ButtonStyle style = MenuManager.style;
            if (GUI.Button(new Rect(style.paddingLR, 20, style.width, style.height), "<color=#FF7F7F>Back</color>")) { back?.Invoke(); }
            mainScroll = GUI.BeginScrollView(
                new Rect(0, 15 + 30, style.width + style.paddingLR * 2, windowHeight - (15 - 30)),
                mainScroll,
                new Rect(0, 0, style.width + style.paddingLR * 2, style.paddingVert + (style.height + style.paddingVert) * 3));
            
            int i = 0;
            foreach (PlayerAvatar player in GameDirector.instance.PlayerList)
            {
                PlayerControl p = player.GetComponentInParent<PlayerControl>();
                if (GUI.Button(new Rect(style.paddingLR, CalcY(i), style.width, style.height), $"<color={(p.isLocal?"blue":"white")}>{p.playerName}</color>"))
                {
                    action?.Invoke(p);
                }
                i++;
            }
            GUI.EndScrollView();
        }
    }
}
