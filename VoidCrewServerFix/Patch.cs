using CG.Profile;
using HarmonyLib;
using Photon.Pun;

namespace VoidCrewServerFix
{
    [HarmonyPatch(typeof(PlayerProfileLoader), "Awake")]
    internal class Patch
    {
        static void Postfix()
        {
            var PSO = PhotonNetwork.ServerPortOverrides;
            PSO.MasterServerPort = 27001;
            PhotonNetwork.ServerPortOverrides = PSO;
        }
    }
}
