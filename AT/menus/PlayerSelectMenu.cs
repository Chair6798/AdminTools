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
            if (GUI.Button(new Rect(style.paddingLR, 20, style.width, style.height),"Back")){back?.Invoke();}
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
        }
    }
}
