using UnityEngine;

namespace AT
{
    public class ItemSpawnSelectMenu : Menu
    {
        
        public override void Draw()
        {
            windowHeight = 20 + 35 + 35 * 6;
            ButtonStyle style = MenuManager.style;
            if (GUI.Button(new Rect(5, 20, 140, 30), "<color=#FF7F7F>Back</color>"))
            {
                MenuManager.Set("Item");
            }
            mainScroll = GUI.BeginScrollView(
                new Rect(0, 20 + 30, 150, windowHeight - (20 + 30)),
                mainScroll,
                new Rect(0, 0, 150, 5 + 35 * 6));

            int i = 0;
            if (GUI.Button(new Rect(style.paddingLR, CalcY(i), style.width, style.height), "Grenades"))
            {
                MenuManager.Set("ItemSpawn", ConvertLib.pack(i));
            }
            i++;
            if (GUI.Button(new Rect(style.paddingLR, CalcY(i), style.width, style.height), "Guns"))
            {
                MenuManager.Set("ItemSpawn", ConvertLib.pack(i));
            }
            i++;
            if (GUI.Button(new Rect(style.paddingLR, CalcY(i), style.width, style.height), "Orbs"))
            {
                MenuManager.Set("ItemSpawn", ConvertLib.pack(i));
            }
            i++;
            if (GUI.Button(new Rect(style.paddingLR, CalcY(i), style.width, style.height), "Drones"))
            {
                MenuManager.Set("ItemSpawn", ConvertLib.pack(i));
            }
            i++;
            if (GUI.Button(new Rect(style.paddingLR, CalcY(i), style.width, style.height), "Upgrades"))
            {
                MenuManager.Set("ItemSpawn", ConvertLib.pack(i));
            }
            i++;
            if (GUI.Button(new Rect(style.paddingLR, CalcY(i), style.width, style.height), "Other"))
            {
                MenuManager.Set("ItemSpawn", ConvertLib.pack(i));
            }
            i++;

            GUI.EndScrollView();
        }
    }
}