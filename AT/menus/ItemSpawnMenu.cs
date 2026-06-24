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
