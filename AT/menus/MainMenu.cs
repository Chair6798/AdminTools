using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
namespace AT
{
    public class MainMenu : Menu
    {
        public MainMenu()
        {
            windowHeight = 25 + 5 + 35 * 3;
        }
        public override void Draw()
        {
            ButtonStyle style = MenuManager.style;
            mainScroll = GUI.BeginScrollView(
                new Rect(0, 15, style.width + style.paddingLR * 2, windowHeight - (15)),
                mainScroll,
                new Rect(0, 0, style.width + style.paddingLR * 2, style.paddingVert + (style.height + style.paddingVert) * 3));
            
            int i = 0;
            if (GUI.Button(new Rect(style.paddingLR,CalcY(i), style.width, style.height), "Items"))
            {
                MenuManager.Set("Item");
            }
            i++;
            if (GUI.Button(new Rect(style.paddingLR, CalcY(i), style.width, style.height), "Players"))
            {
                MenuManager.Set("Players");
            }
            i++;
            GUI.EndScrollView();
        }
    }
}
