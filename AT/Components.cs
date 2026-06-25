using Photon.Pun;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Localization.SmartFormat.Utilities;
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

        void Awake()
        {
            avatar = GetComponentInChildren<PlayerAvatar>();
            health = avatar.GetComponent<PlayerHealth>();
            tumble = GetComponentInChildren<PlayerTumble>();
            head = GetComponentInChildren<PlayerDeathHead>();
            camera = GetComponentInChildren<PlayerLocalCamera>();
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


        //STATIC
        public static PlayerControl GetLocal()
        {
            return SemiFunc.PlayerAvatarLocal().GetComponentInParent<PlayerControl>();
        }
        public static PlayerControl Get(PlayerAvatar avatar)
        {
            return avatar.GetComponentInParent<PlayerControl>();
        }
        public static PlayerControl Get(string SteamId)
        {
            return Get(SemiFunc.PlayerAvatarGetFromSteamID(SteamId));
        }
    }
    public class ItemController : MonoBehaviour
    {
        ItemBattery IBattery;
        ItemEquippable IEquippable;
        ItemToggle IToggle;
        ItemUpgrade IUpgrade;

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
