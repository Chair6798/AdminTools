
using UnityEngine;

namespace AT
{
    public class ItemMenu : Menu
    {
        public new int windowHeight = 25 + 5 + 35 * 3;
        public override void Draw()
        {
            if (GUI.Button(new Rect(5, 20, 140, 30), "Back"))
            {
                MenuManager.Set("Main");
            }
            mainScroll = GUI.BeginScrollView(
                new Rect(0, 20 + 30, 150, windowHeight - (20 + 30)),
                mainScroll,
                new Rect(0, 0, 150, 5 + 35 * 3));
            int i = 0;
            if (GUI.Button(new Rect(5, CalcY(i), 140, 30), "Spawn"))
            {
                MenuManager.Set("ItemSpawnSelect");
            }

            GUI.EndScrollView();
        }
    }
}