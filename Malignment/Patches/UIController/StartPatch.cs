using HarmonyLib;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace Malignment;

[HarmonyPatch(typeof(UIController), "Start")]
public class StartPatch
{
    [UsedImplicitly]
    private static void Postfix(UIController __instance)
    {
        var resized = 0;
        
        if (!__instance.canvasOverlayScreen) return;
        
        var scaler = __instance.canvasOverlayScreen.GetComponentInParent<CanvasScaler>();
        if (!scaler) return;
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.matchWidthOrHeight = 1f;
        scaler.referenceResolution = new Vector2(Plugin.ScreenResW.Value, Plugin.ScreenResH.Value);
        Plugin.PluginLogger.LogInfo($"{Plugin.PluginMetadata.PLUGIN_GUID}: CanvasScaler set to height-based scaling");

        if (__instance.canvasPauseMenu)
        {
            var highresGameObjectTransform = __instance.canvasPauseMenu.transform.Find("highres");
            if (highresGameObjectTransform)
            {
                var highresImage = highresGameObjectTransform.GetComponent<Image>();
                if (highresImage)
                {
                    Plugin.PluginLogger.LogInfo($"{Plugin.PluginMetadata.PLUGIN_GUID}: Resizing pause menu black background...");
                    ResizeHighres(highresImage);
                    resized++;
                    Plugin.PluginLogger.LogInfo($"{Plugin.PluginMetadata.PLUGIN_GUID}: ... done.");
                }
            }
        }

        if (__instance.canvasOther)
        {
            var extraLayer2 = __instance.canvasOther.transform.Find("extra_layer_2");
            if (extraLayer2)
            {
                var puzzleScreen = extraLayer2.transform.Find("Puzzle Screen");
                if (puzzleScreen)
                {
                    var highresGameObjectTransform = puzzleScreen.transform.Find("highres");
                    if (highresGameObjectTransform)
                    {
                        var highresImage = highresGameObjectTransform.GetComponent<Image>();
                        if (highresImage)
                        {
                            Plugin.PluginLogger.LogInfo($"{Plugin.PluginMetadata.PLUGIN_GUID}: Resizing puzzle menu black background...");
                            ResizeHighres(highresImage);
                            resized++;
                            Plugin.PluginLogger.LogInfo($"{Plugin.PluginMetadata.PLUGIN_GUID}: ... done.");
                        }
                    }
                }
            }
        }

        if (__instance.navigationToolbar)
        {
            var rectTransform = __instance.navigationToolbar.GetComponent<RectTransform>();

            rectTransform.anchorMin = new Vector2(0f, rectTransform.anchorMin.y);
            rectTransform.anchorMax = new Vector2(0f, rectTransform.anchorMax.y);
            
            rectTransform.pivot = new Vector2(0f, rectTransform.pivot.y);
            rectTransform.anchoredPosition = new Vector2(0f, rectTransform.anchoredPosition.y);
        }
        
        Plugin.PluginLogger.LogInfo($"{Plugin.PluginMetadata.PLUGIN_GUID}: Resized {resized} background images.");
    }
    

    
    private static void ResizeHighres(Image image)
    {
        var imageTransform = image.GetComponent<RectTransform>();
        imageTransform.localPosition = new Vector3(0f, 0f, 1f);
        imageTransform.localScale = new Vector3(Screen.width, Screen.height, 1f);
    }
}