using UnityEditor;
using UnityEngine;

namespace VibrantHierarchy.Editor
{
    [InitializeOnLoad]
    public static class VibrantHierarchyRenderer
    {
        private static VibrantHierarchySettings Settings;
        static VibrantHierarchyRenderer()
        {
            EditorApplication.hierarchyWindowItemOnGUI += HandleHierarchyWindowItemOnGUI;
            LoadSettings();
        }

        private static void HandleHierarchyWindowItemOnGUI(int instanceID, Rect selectionRect)
        {
            var instance = EditorUtility.InstanceIDToObject(instanceID) as GameObject;
            if (instance == null) return;
            
            foreach (var style in Settings.Styles)
            {
                if (string.CompareOrdinal(instance.name, style.Token) != 0) continue;
                
                PaintBackground(style, selectionRect);
                PaintText(instance as GameObject, style, selectionRect);
                break;
            }
        }

        private static void PaintBackground(VibrantStyle style, Rect rectangle)
        {
            EditorGUI.DrawRect(rectangle, style.BackgroundColor);
        }

        private static void PaintText(GameObject item, VibrantStyle style, Rect rectangle)
        {
            GUIStyle labelGUIStyle = new GUIStyle
            {
                normal = new GUIStyleState { textColor = style.TextColor },
                fontStyle = style.FontStyle,
                alignment = style.Alignment,
                fontSize = style.FontSize,
                font = style.Font,
            };
            EditorGUI.LabelField(rectangle, item.name, labelGUIStyle);
        }

        private static void LoadSettings()
        {
            var settingsPaths = AssetDatabase.FindAssets($"t:{nameof(VibrantHierarchySettings)}");
            if (settingsPaths.Length == 1)
            {
                var assetPath = AssetDatabase.GUIDToAssetPath(settingsPaths[0]);
                Settings = AssetDatabase.LoadAssetAtPath<VibrantHierarchySettings>(assetPath);
            }
            else
            {
                Settings = VibrantHierarchySettings.GetOrCreateSettings();
            }
        }
    }
}
