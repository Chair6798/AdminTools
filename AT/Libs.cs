using AT.Components;
using Photon.Pun;
using Photon.Voice;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.UIElements;
using static ES3;
namespace AT
{
    public static class StringLib
    {
        public static string Connect(Collection<string> list, string connector)
        {
            string str = "";
            int i = 0;
            foreach(string s in list)
            {
                if (i!=0)
                {
                    str += connector;
                }
                str += s;
                i++;
            }
            return str;
        }
    }
    public static class ConvertLib
    {
        public static Collection<string> stringArrayToStringCollection(string[] array)
        {
            var coll = new Collection<string>();
            foreach (string s in array)
            {
                coll.Add(s);
            }
            return coll;
        }
        public static Collection<object> pack(object o)
        {
            var a = new Collection<object>();
            a.Add(o);
            return a;
        }
    }
    public static class UILib
    {
        public static GUIStyle Style(TextAnchor all, int fontsize)
        {
            var st = new GUIStyle();
            st.alignment = all;
            st.fontSize = fontsize;
            return st;
        }



        public static void Label(int i, string text, GUIStyle style)
        {
            Label(new Rect(5, 5 + i * 35, 140, 30), text, style);
        }

        public static void Label(Rect transform, string text, GUIStyle style)
        {
            GUI.Label(transform, text, style);
        }
        public static void BackButton()
        {
            Button(new Rect(5, 20, 140, 30), "Back", () =>
            {
                MenuManager.Set("Main");
            });
        }

        public static void Button(int i, string text, Action action)
        {
            Button(new Rect(5, 5 + i * 35, 140, 30), text, action);
        }
        public static void Button(Rect transform, string text, Action action)
        {
            if(GUI.Button(transform, text))
            {
                action?.Invoke();
            }
        }
        public static string GetToggleString(string text, bool value)
        {
            return $"<color={(value ? "green" : "red")}>{text}</color> ({(value ? "✓" : "✗")})";
        }
        public static void ToggleButton(Rect transform, string text, ref bool value, Action<bool> OnChange = null, Action OnEnable = null, Action OnDisable = null)
        {
            if (GUI.Button(transform, GetToggleString(text, value)))
            {
                value = !value;
                if (value) OnEnable?.Invoke();
                else OnDisable?.Invoke();
                OnChange?.Invoke(value);
            }
        }
    }

    public class ObjectLib : MonoBehaviour
    {
        static PhotonView photonView;
        void Awake()
        {
            photonView = GetComponent<PhotonView>();
        }

        void SpawnRPC(PrefabRef prefab, Vector3 position, Quaternion rotation, PhotonMessageInfo _info= default(PhotonMessageInfo))
        {
            if (!SemiFunc.IsMasterClientOrSingleplayer())
                return;
            if (AdminManager.IsAdmin(SemiFunc.PlayerGetSteamID(SemiFunc.PlayerAvatarGetFromPhotonPlayer(_info.Sender))))
            {
                HostSpawn(prefab, position, rotation);
            }
        }

        void DestroyRPC(GameObject obj, PhotonMessageInfo _info = default(PhotonMessageInfo))
        {
            if (!SemiFunc.IsMasterClientOrSingleplayer())
                return;
            if (AdminManager.IsAdmin(SemiFunc.PlayerGetSteamID(SemiFunc.PlayerAvatarGetFromPhotonPlayer(_info.Sender))))
            {
                HostDestroy(obj);
            }
        }

        static void HostSpawn(PrefabRef prefab, Vector3 position, Quaternion rotation)
            
        {
            LogLib.Log("Host Spawn!");
            if (SemiFunc.IsMultiplayer()) { PhotonNetwork.InstantiateRoomObject(prefab.ResourcePath, position, rotation); } else { UnityEngine.Object.Instantiate(prefab.Prefab, position, rotation); }
        }

        static void HostDestroy(GameObject obj)
        {
            if (SemiFunc.IsMultiplayer()) { PhotonNetwork.Destroy(obj); } else { Destroy(obj); }
        }

        static void HostTransform(GameObject obj, Vector3 position, Quaternion rotation)
        {
            var rb = obj.GetComponent<Rigidbody>();
            if (rb == null)
            {
                if (SemiFunc.IsMultiplayer())
                {
                    LogLib.Log("RigidBody Required to transform object!", 1);
                    return;
                }
                else
                {
                    obj.transform.position = position;
                    obj.transform.rotation = rotation;
                }

            }
            rb.position = position;
            rb.rotation = rotation;
        }

        public static void Spawn(Item item)
        {
            var tr = PlayerControl.GetLocal().GetCameraTransform();
            Spawn(item.prefab, tr.position+tr.forward*3, Quaternion.identity);
        }
        public static void Spawn(Item item, Vector3 position, Quaternion rotation)
        {
            Spawn(item.prefab, position, rotation);  
        }
        public static void Spawn( PrefabRef prefab, Vector3 position, Quaternion rotation)
        {
            LogLib.Log("Spawn!");
            if (SemiFunc.IsMasterClientOrSingleplayer())
            {
                HostSpawn(prefab, position, rotation);
            }
            else
            {
                if (SemiFunc.IsMultiplayer())
                {
                    LogLib.Log("RPC Spawn!");
                    photonView.RPC("SpawnRPC", RpcTarget.MasterClient, prefab, position, rotation);
                }
            }
            
        }
        public static void Destroy(GameObject obj)
        {
            if (SemiFunc.IsMasterClientOrSingleplayer())
            {
                HostDestroy(obj);
            }
            else
            {
                if (SemiFunc.IsMultiplayer())
                {
                    photonView.RPC("DestroyRPC", RpcTarget.MasterClient, obj);
                }
            }
        }
        public static void Transform(GameObject obj, Vector3 position, Quaternion rotation)
        {

            var rb = obj.GetComponent<Rigidbody>();
            if (rb==null)
            {
                if (SemiFunc.IsMultiplayer())
                {
                    LogLib.Log("RigidBody Required to transform object!", 1);
                    return;
                }
                else
                {
                    obj.transform.position = position;
                    obj.transform.rotation = rotation;
                }
                
            }
            rb.position = position;
            rb.rotation = rotation;
        }
        public static void Velocity(GameObject obj, Vector3 velocity)
        {
            var rb = obj.GetComponent<Rigidbody>();
            if (rb == null)
            {
                return;
            }
            rb.velocity = velocity;
        }
    }
    public static class ItemLib
    {
        static Collection<Item> Grenades;
        static Collection<Item> Guns;
        static Collection<Item> Orbs;
        static Collection<Item> Drones;
        static Collection<Item> Upgrades;
        static Collection<Item> Other;

        [Obsolete("Use ObjectLib.Spawn")]
        public static void Spawn(Item item, Vector3 position, Quaternion rotation)
        {
            ObjectLib.Spawn(item, position, rotation);
        }

        public static Collection<Item> GetAll()
        {
            var items = new Collection<Item>();
            foreach (Item item in StatsManager.instance.itemDictionary.Values)
            {
                items.Add(item);
            }
            return items;
        }
        public static Collection<Item> Get(int category)
        {
            switch(category)
            {
                case 0:
                    return Grenades;
                case 1:
                    return Guns;
                case 2:
                    return Orbs;
                case 3:
                    return Drones;
                case 4:
                    return Upgrades;
                case 5:
                    return Other;


                default:
                    return GetAll();
            }
        }
        public static void Init()
        {
            Setup();
            var items = GetAll();
            foreach (Item item in items)
            {
                Sort(item);
            }
        }
        static void Setup()
        {
            Grenades = new Collection<Item>();
            Guns = new Collection<Item>();
            Orbs = new Collection<Item>();
            Drones = new Collection<Item>();
            Upgrades = new Collection<Item>();
            Other = new Collection<Item>();
        }
        static void Sort(Item item)
        {
            var name = item.name;
            if (name.Contains("Grenade"))
            {
                Grenades.Add(item);
                return;
            }
            if (name.Contains("Gun"))
            {
                Guns.Add(item);
                return;
            }
            if (name.Contains("Drone"))
            {
                Drones.Add(item);
                return;
            }
            if (name.Contains("Orb"))
            {
                Orbs.Add(item);
                return;
            }
            if (name.Contains("Upgrade"))
            {
                Upgrades.Add(item);
                return;
            }
            Other.Add(item);
        }
    }


    public static class DataLib
    {
        public static FieldInfo GetField(Type type, string fieldName)
        {
            return type.GetField(fieldName, BindingFlags.NonPublic|BindingFlags.Instance);
        }
        public static object GetValue(object obj, string fieldName)
        {
            return GetField(obj.GetType(), fieldName).GetValue(obj);
        }
        public static void SetValue(object obj, string fieldName, object value)
        {
            GetField(obj.GetType(), fieldName).SetValue(obj, value);
        }
    }

    public static class LogLib
    {
        internal static void Log(string msg, int level)
        {
            string lvl = level == 0 ? "Info" : level == 1 ? "Warning" : level == 2 ? "Error" : "Unknown";
            Console.WriteLine($"[{ModData.Name}][{lvl}] {msg}");
        }
        internal static void Log(string msg)
        {
            Log(msg, 0);
        }
    }
}