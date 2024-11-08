using HarmonyLib;
using Photon.Pun;
using Photon.Realtime;

namespace VoidCrewServerFix
{
    [HarmonyPatch(typeof(PhotonService), "AutoConnectIfDisconnected")]
    internal class Patch
    {
        static bool SecondAttempt = true;
        static string lastRegionAttempt = "us";
        static bool Prefix(PhotonService __instance)
        {
            if (PhotonNetwork.NetworkClientState == ClientState.ConnectedToMasterServer || PhotonNetwork.NetworkClientState == ClientState.ConnectedToGameServer)
            {
                SecondAttempt = true;
                return true;
            }
            if (PhotonNetwork.NetworkClientState == ClientState.Disconnected)
            {
                SecondAttempt = !SecondAttempt;
                if (SecondAttempt)
                {
                    string nextRegion = GetNextAvailableRegion();
                    Plugins.Log.LogMessage("Detected issue with regions, attempting " + nextRegion);
                    __instance.targetRegion = nextRegion;
                    return false;
                }
            }
            return true;
        }

        static string GetNextAvailableRegion()
        {
            switch (lastRegionAttempt)
            {
                case "us":
                    return "eu";
                case "eu":
                    return "au";
                case "au":
                    return "us";
                default:
                    return "us";
            }
        }
    }
}
