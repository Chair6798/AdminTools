using System.Collections.ObjectModel;
using UnityEngine;
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
            if (GUI.Button(new Rect(5, 20, 140, 30), "Back"))
            {
                MenuManager.Set("ItemSpawnSelect");
            }
            mainScroll = GUI.BeginScrollView(
                new Rect(0, 20 + 30, 150, windowHeight - (20 + 30)),
                mainScroll,
                new Rect(0, 0, 150, 5 + 35 * GetCount()));

            DrawList();
            GUI.EndScrollView();
        }
        public override void DrawList()
        {
            bool all = GetItems() == ItemLib.GetAll();
            int i = 0;
            foreach (Item item in GetItems())
            {
                var str = item.itemName;
                if (GUI.Button(new Rect(5, CalcY(i), 140, 30), str) ){ObjectLib.Spawn(item); }
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
