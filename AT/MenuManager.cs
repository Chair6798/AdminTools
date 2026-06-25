using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using AT.Components;
namespace AT
{
    public class MenuManager : MonoBehaviour
    {
        public static MenuManager instance {  get; private set; }
        public static bool Render = false;

        public static string CurrentMenuId;
        public static Menu CurrentMenu;

        public static ButtonStyle style;

        public static void ToggleRender()
        {
            Render= !Render;
        }
        void Update()
        {
            if (Input.GetKeyUp(Cfg.menuToggleKey.Value))
            {
                ToggleRender();
                if (!Render)
                {
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                }
            }
        }
        void Awake()
        {
            instance = this;
            style = new ButtonStyle();
            Set("Main");
        }

        void OnGUI()
        {
            if (CurrentMenu != null && Render)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                GUI.Window(1, new Rect(Screen.width / 2 - 100, Screen.height / 2 - CurrentMenu.windowHeight/2, 200, CurrentMenu.windowHeight), Draw, "AT by CoolChair");
            }
        }
        void Draw(int i)
        {
            CurrentMenu.Draw();
        }

        public static Menu Init(string menuId, Collection<object> additional)
        {
            switch (menuId)
            {
                case "Main":
                    return new MainMenu();

                case "ItemSpawn":
                    return new ItemSpawnMenu((int) additional[0]);

                case "Item":
                    return new ItemMenu();

                case "ItemSpawnSelect":
                    return new ItemSpawnSelectMenu();

                case "PlayerSelect":
                    return new PlayerSelectMenu((Action<PlayerControl>)additional[0], (Action)additional[1]);
                default:
                    return new Menu();
            }
        }

        public static void Set(string id, Collection<object> additional)
        {
            CurrentMenuId = id;
            CurrentMenu = Init(CurrentMenuId, additional);
        }
        public static void Set(string id)
        {
            Set(id, null);
        }
    }
}
