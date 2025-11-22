using HarmonyLib;
using JetBrains.Annotations;
using UnityEngine;

namespace Malignment;

[HarmonyPatch(typeof(UIController), nameof(UIController.StartGameAfterLoading))]
public class StartGameAfterLoadingPatch
{
    [UsedImplicitly]
    private static void Postfix()
    {
        Plugin.PluginLogger.LogInfo("Setting resolution...");
        Screen.SetResolution(Plugin.ScreenResW.Value, Plugin.ScreenResH.Value, Plugin.FullscreenMode.Value);
    }
}