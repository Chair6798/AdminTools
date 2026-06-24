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
        public override void Draw()
        {
            windowHeight = 25 + 5 + 35 * 3;
            mainScroll = GUI.BeginScrollView(
                new Rect(0, 20, 150, windowHeight - (20)),
                mainScroll,
                new Rect(0, 0, 150, 5 + 35 * 3));
            int i = 0;
            if (GUI.Button(new Rect(5,CalcY(i), 140, 30), "Items"))
            {
                MenuManager.Set("Item");
            }
            GUI.EndScrollView();
        }
    }
}
