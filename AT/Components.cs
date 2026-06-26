using Photon.Pun;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;

namespace AT.Components
{
    public class EnemyController : MonoBehaviour
    {
        public Enemy EnemyComponent;

        void Awake()
        {
            EnemyComponent = GetComponentInChildren<Enemy>();
        }
    }
    public class PlayerControl : MonoBehaviour
    {
        public PlayerAvatar avatar;
        public PlayerHealth health;

        public PlayerTumble tumble;

        public PlayerDeathHead head;

        public PlayerLocalCamera camera;

        public bool isLocal = false;

        public string steamID;

        public string playerName;

        void Awake()
        {
            avatar = GetComponentInChildren<PlayerAvatar>();
            health = avatar.GetComponent<PlayerHealth>();
            tumble = GetComponentInChildren<PlayerTumble>();
            head = GetComponentInChildren<PlayerDeathHead>();
            camera = GetComponentInChildren<PlayerLocalCamera>();

            isLocal = (avatar == SemiFunc.PlayerGetLocal());
            

            steamID = (string)DataLib.GetValue(avatar, "steamID");
            playerName = SemiFunc.PlayerGetName(avatar);
        }
        void Update()
        {
            if (head==null)
            {
                head = GetComponentInChildren<PlayerDeathHead>();
            }
            if (tumble==null)
            {
                tumble = GetComponentInChildren<PlayerTumble>();
            }
        }

        public Vector3 GetPosition()
        {
            if(!Alive())
            {
                return head.transform.position;
            }
            if(IsTumbling())
            {
                return tumble.transform.position;
            }
            return avatar.transform.position;
        }

        public Transform GetCameraTransform()
        {
            return camera.transform;
        }

        public bool IsTumbling()
        {
            return (bool)DataLib.GetValue(avatar, "isTumbling");
        }

        public bool Alive()
        {
            return !(bool)DataLib.GetValue(avatar, "deadSet");
        }
        public void SetAlive(bool value)
        {
            if(value)
            {
                Revive();
            }
            else
            {
                Kill();
            }
        }
        public void SetTumble(bool value)
        {
            tumble.TumbleSet(value, false);
        }
        public void SetHealth(int amount)
        {
            if (amount<=0)
            {
                DataLib.SetValue(health, "health", 0);
                Kill();
            }
            else
            {
                Revive();
                DataLib.SetValue(health, "health", amount);
            }
        }
        public void Kill()
        {
            if (Alive())
            {
                avatar.PlayerDeath(-1);
            }
        }
        public void Revive()
        {
            if (!Alive())
            {
                avatar.Revive();
            }
        }

        public void Teleport(Vector3 targetPosition, Quaternion targetRotation)
        {
            if (!Alive())
            {
                avatar.Spawn(targetPosition, Quaternion.identity);
            }
            if (IsTumbling())
            {
                ObjectLib.Transform(tumble.gameObject, targetPosition, targetRotation);
            }
            ObjectLib.Transform(head.gameObject, targetPosition, targetRotation);
        }
        public void Teleport(Vector3 targetPosition)
        {
            Teleport(targetPosition, Quaternion.identity);
        }

        //STATIC

        public static Collection<PlayerControl> GetAll(Collection<PlayerControl> exclude = null)
        {
            var all = new Collection<PlayerControl>();
            foreach (PlayerAvatar avatar in GameDirector.instance.PlayerList)
            {
                var p = Get(avatar);
                if (exclude != null && exclude.Contains(p))
                    continue;
                all.Add(p);
            }
            return all;
        }

        public static PlayerControl GetLocal()
        {
            return SemiFunc.PlayerAvatarLocal().GetComponentInParent<PlayerControl>();
        }
        public static Collection<PlayerControl> GetLocalCollection()
        {
            var coll = new Collection<PlayerControl>(); coll.Add(GetLocal()); return coll;
        }
        public static PlayerControl Get(PlayerAvatar avatar)
        {
            return avatar.GetComponentInParent<PlayerControl>();
        }
        public static PlayerControl Get(string SteamId)
        {
            return Get(SemiFunc.PlayerAvatarGetFromSteamID(SteamId));
        }

        public static void KillAll(Collection<PlayerControl> exclude = null)
        {
            foreach (PlayerControl control in GetAll(exclude))
            {
                control.Kill();
            }
        }
        public static void ReviveAll(Collection<PlayerControl> exclude = null)
        {
            foreach (PlayerControl control in GetAll(exclude))
            {
                control.Revive();
            }
        }
        public static void TeleportAll(Vector3 targetPosition, Quaternion targetRotation, Collection<PlayerControl> exclude = null)
        {
            foreach (PlayerControl control in GetAll(exclude))
            {
                control.Teleport(targetPosition, targetRotation);
            }
        }
        public static void TeleportAll(Vector3 targetPosition, Collection<PlayerControl> exclude = null)
        {
            TeleportAll(targetPosition, Quaternion.identity, exclude);
        }

        public static void SetTumbleAll(bool value, Collection<PlayerControl> exclude = null)
        {
            foreach (PlayerControl control in GetAll(exclude))
            {
                control.SetTumble(value);
            }
        }

    }
    public class ItemController : MonoBehaviour
    {
        public ItemBattery IBattery;
        public ItemEquippable IEquippable;
        public ItemToggle IToggle;
        public ItemUpgrade IUpgrade;


        void Awake()
        {
            IBattery = GetComponent<ItemBattery>();
            IEquippable = GetComponent<ItemEquippable>();
            IToggle = GetComponent<ItemToggle>();
            IUpgrade = GetComponent<ItemUpgrade>();
        }
        public bool Toggleable()
        {
            return IToggle != null;
        }
        public bool Equippable()
        {
            return IEquippable != null;
        }
        public bool IsEquipped()
        {
            return Equippable() && IEquippable.IsEquipped();
        }
        public bool IsUpgrade()
        {
            return IUpgrade != null;
        }
        public void Toggle(int player = -1)
        {
            if (Toggleable())
                IToggle.ToggleItem(!IToggle.toggleState, player);
        }
        public void Unequip()
        {
            if (Equippable()&&IsEquipped())
                IEquippable.RequestUnequip();
        }

        public void Equip(PlayerControl player, int spot)
        {
            IEquippable.RequestEquip(spot, player.avatar.physGrabber.photonView.ViewID);
        }
    }
}
