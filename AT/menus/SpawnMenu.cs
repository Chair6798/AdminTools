using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
namespace AT
{
    public class SpawnMenu : Menu
    {
        public virtual Collection<object> GetAll()
        {
            return new Collection<object>();
        }
        public override void Draw()
        {
            var style = MenuManager.style;
            if (GUI.Button(new Rect(style.paddingLR, 20, style.width, style.height), "<color=#FF7F7F>Back</color>"))
            {
                MenuManager.Set("Main");
            }
            mainScroll = GUI.BeginScrollView(
                new Rect(0, 15 + 30, style.width + style.paddingLR * 2, windowHeight - (15 - 30)),
                mainScroll,
                new Rect(0, 0, style.width + style.paddingLR * 2, style.paddingVert + (style.height + style.paddingVert) * GetCount()));

            DrawList();
            GUI.EndScrollView();
        }
        public virtual int GetCount()
        {
            return 0;
        }
        public virtual void DrawList()
        {

        }

    }
}
