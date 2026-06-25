
using UnityEngine;

namespace AT
{
    public class ItemMenu : Menu
    {
        
        public override void Draw()
        {
            windowHeight = 25 + 30 + 35 * 3;

            
            ButtonStyle style = MenuManager.style;

            if (GUI.Button(new Rect(style.paddingLR, 20, style.width, style.height), "<color=#FF7F7F>Back</color>"))
            {
                MenuManager.Set("Main");
            }
            mainScroll = GUI.BeginScrollView(
                new Rect(0, 20 + style.height, style.width + style.paddingLR * 2, windowHeight - (20 - style.height)),
                mainScroll,
                new Rect(0, 0, style.width + style.paddingLR * 2, style.paddingVert + (style.height + style.paddingVert) * 3));
            int i = 0;
            if (GUI.Button(new Rect(style.paddingLR, CalcY(i), style.width, style.height), "Spawn"))
            {
                MenuManager.Set("ItemSpawnSelect");
            }

            GUI.EndScrollView();
        }
    }
}