using Photon.Pun;
using Steamworks;
using System.Collections.ObjectModel;
using UnityEngine;
using AT.Components;
namespace AT
{
    public class Admin
    {
        public enum RightType
        {
            Spawn,
            Settings,
            Players
        }
        public Collection<RightType> rights {  get; set; }
        public string SteamID { get; private set;}
        public Admin (string steamId)
        {
            SteamID = steamId;
        }
        public PlayerAvatar GetAvatar()
        {
            return SemiFunc.PlayerGetFromSteamID(SteamID);
        }
    }
    public class RightManager : MonoBehaviour
    {
        public static RightManager instance;
        public static Collection<Admin> Admins;
        public static bool Admin = false;

        private static PhotonView photonView;


        void Awake()
        {
            photonView = gameObject.GetComponent<PhotonView>();
        }

        public static bool IsAdmin()
        {
            return (SemiFunc.IsMasterClientOrSingleplayer()||Admin);
        }

        public static bool IsAdmin(string SteamId)
        {
            foreach (Admin admin in Admins)
            {
                if (admin.SteamID == SteamId)
                {
                    return true;
                }
            }
            return false;
        }
        
        public static void SetAdmin(string SteamId, bool admin)
        {
            if (admin)
            {
                if (!IsAdmin(SteamId))
                {
                    Admins.Add(new Admin(SteamId));
                    photonView.RPC("AdminRPC", PlayerControl.Get(SteamId).avatar.GetComponent<PhotonView>().Owner, true);

                }
            }
        }

        public static bool IsAdminAsk(string SteamID)
        {
            if (Admins == null)
            {
                Admins = new Collection<Admin>();
            }
            foreach (Admin admin in Admins)
            {
                if (admin.SteamID == SteamID)
                {
                    return true;
                }
            }
            return false;
        }
        [PunRPC]
        public static void IsAdminRPC(PhotonMessageInfo _info)
        {
            if(!SemiFunc.IsMasterClient())
            {
                return;
            }

        }
        [PunRPC]
        public static void AdminRPC(bool admin, PhotonMessageInfo _info = default(PhotonMessageInfo))
        {
            if (!SemiFunc.MasterOnlyRPC(_info))
            {
                return;
            }
            if (SemiFunc.PlayerAvatarGetFromPhotonPlayer(_info.Sender)==SemiFunc.PlayerGetLocal())
            {
                Admin = admin;
                if (!Admin)
                    MenuManager.Render = false;
            }
        }
    }
}
