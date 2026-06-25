using UnityEngine;

namespace AT
{
    public class ItemSpawnSelectMenu : Menu
    {
        
        public override void Draw()
        {
            windowHeight = 20 + 35 + 35 * 6;
            if (GUI.Button(new Rect(5, 20, 140, 30), "Back"))
            {
                MenuManager.Set("Item");
            }
            mainScroll = GUI.BeginScrollView(
                new Rect(0, 20 + 30, 150, windowHeight - (20 + 30)),
                mainScroll,
                new Rect(0, 0, 150, 5 + 35 * 6));

            int i = 0;
            if (GUI.Button(new Rect(5, CalcY(i), 140, 30), "Grenades"))
            {
                MenuManager.Set("ItemSpawn", ConvertLib.pack(i));
            }
            i++;
            if (GUI.Button(new Rect(5, CalcY(i), 140, 30), "Guns"))
            {
                MenuManager.Set("ItemSpawn", ConvertLib.pack(i));
            }
            i++;
            if (GUI.Button(new Rect(5, CalcY(i), 140, 30), "Orbs"))
            {
                MenuManager.Set("ItemSpawn", ConvertLib.pack(i));
            }
            i++;
            if (GUI.Button(new Rect(5, CalcY(i), 140, 30), "Drones"))
            {
                MenuManager.Set("ItemSpawn", ConvertLib.pack(i));
            }
            i++;
            if (GUI.Button(new Rect(5, CalcY(i), 140, 30), "Upgrades"))
            {
                MenuManager.Set("ItemSpawn", ConvertLib.pack(i));
            }
            i++;
            if (GUI.Button(new Rect(5, CalcY(i), 140, 30), "Other"))
            {
                MenuManager.Set("ItemSpawn", ConvertLib.pack(i));
            }
            i++;

            GUI.EndScrollView();
        }
    }
}