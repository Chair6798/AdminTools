using System.Collections.ObjectModel;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI.MessageBox;
namespace AT
{
    public class ItemSpawnMenu : SpawnMenu
    {
        int category;
        public ItemSpawnMenu(int CategoryID)
        {
            category = CategoryID;
        }
        public override void Draw()
        {
            ButtonStyle style = MenuManager.style;
            if (GUI.Button(new Rect(style.paddingLR, 15, style.width, style.height), "<color=#FF7F7F>Back</color>"))
            {
                MenuManager.Set("ItemSpawnSelect");
            }
            
            mainScroll = GUI.BeginScrollView(
                new Rect(0, 15 + 30, style.width + style.paddingLR * 2, windowHeight - (15 - 30)),
                mainScroll,
                new Rect(0, 0, style.width+style.paddingLR*2, style.paddingVert + (style.height + style.paddingVert) * GetCount()));

            DrawList();
            GUI.EndScrollView();
        }
        public override void DrawList()
        {
            ButtonStyle style = MenuManager.style;
            int i = 0;
            foreach (Item item in GetItems())
            {
                var str = item.itemName;
                if (GUI.Button(new Rect(style.paddingLR, CalcY(i), style.width, style.height), str)){ObjectLib.Spawn(item); }
                i++;
            }
        }
        public override int GetCount()
        {
            return GetItems().Count;
        }
        public Collection<Item> GetItems()
        {
            return ItemLib.Get(category);
        }
    }
}
