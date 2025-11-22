using System.Reflection;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;

namespace Malignment;

[BepInPlugin(PluginMetadata.PLUGIN_GUID, PluginMetadata.PLUGIN_NAME, PluginMetadata.PLUGIN_VERSION)]
public class Plugin : BaseUnityPlugin
{
    internal static ManualLogSource PluginLogger;
    
    internal static ConfigEntry<int> ScreenResW;
    internal static ConfigEntry<int> ScreenResH;
    internal static ConfigEntry<bool> FullscreenMode;
    
    private void Awake()
    {
        PluginLogger = Logger;
        
        ScreenResW = Config.Bind("Resolution", "ScreenWidth", Display.main.systemWidth, "Target screen resolution width.");
        ScreenResH = Config.Bind("Resolution", "ScreenHeight", Display.main.systemHeight, "Target screen resolution height.");
        FullscreenMode = Config.Bind("Fullscreen", "Fullscreen", true, "Fullscreen mode.");
        
        var harmony = new Harmony(PluginMetadata.PLUGIN_GUID);
        harmony.PatchAll();
        
        PluginLogger.LogInfo($"{PluginMetadata.PLUGIN_GUID}: loaded.");
    }
    
    internal static class PluginMetadata
    {
        public const string PLUGIN_NAME = "Malignment";
        public const string PLUGIN_GUID = "ca.bushtail.malignment";
        public const string PLUGIN_VERSION = "1.0.1";
    }
}