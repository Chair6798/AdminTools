using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AT
{
    public class Menu
    {
        public Vector2 mainScroll;

        public int windowHeight = 400;

        public virtual void Draw()
        {
            GUI.Label(new Rect(5, 20, 150, 40), "<color=yellow>Base menu</color>");
        }

        public int CalcY(int i, bool fullcalc=false)
        {
            return 5 + 35 * i + (fullcalc?20:0);
        }
    }
}
