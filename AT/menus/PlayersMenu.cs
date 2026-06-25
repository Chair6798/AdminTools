using UnityEngine;
namespace AT
{
    public class PlayersMenu : Menu
    {
        public PlayersMenu()
        {
            windowHeight = 25 + 5 + 35 * 3;
        }
        public override void Draw()
        {
            ButtonStyle style = MenuManager.style;
            if (GUI.Button(new Rect(style.paddingLR, 30, style.width, style.height), "<color=#FF7F7F>Back</color>"))
            {
                MenuManager.Set("Main");
            }
            mainScroll = GUI.BeginScrollView(
                new Rect(0, 15 + style.height, style.width + style.paddingLR * 2, windowHeight - (20 - style.height)),
                mainScroll,
                new Rect(0, 0, style.width + style.paddingLR * 2, style.paddingVert + (style.height + style.paddingVert) * 3));
            
            int i = 0;
            if (GUI.Button(new Rect(style.paddingLR, CalcY(i), style.width, style.height), "All"))
            {
                MenuManager.Set("AllPlayersFuncs");
            }
            i++;
            if (GUI.Button(new Rect(style.paddingLR, CalcY(i), style.width, style.height), "List"))
            {
                MenuManager.Set("PlayerSelectMenu");
            }
            i++;
            GUI.EndScrollView();
        }
    }
}