using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace VibrantHierarchy.Editor
{
    [CreateAssetMenu(menuName = "ArcticFox/VibrantHierarchy/CreateSettings", fileName = "VibrantHierarchySettings")]
    public class VibrantHierarchySettings : ScriptableObject
    {
        private const string MyCustomSettingsPath = "Assets/VibrantHierarchy/Editor/VibrantHierarchySettings.asset";

        public List<VibrantStyle> Styles = new List<VibrantStyle>();
        internal static VibrantHierarchySettings GetOrCreateSettings()
        {
            var settingsPaths = AssetDatabase.FindAssets($"t:{nameof(VibrantHierarchySettings)}");
            VibrantHierarchySettings settings = null;
            if (settingsPaths.Length == 1)
            {
                var assetPath = AssetDatabase.GUIDToAssetPath(settingsPaths[0]);
                settings = AssetDatabase.LoadAssetAtPath<VibrantHierarchySettings>(assetPath);
            }
            
            return settings ? settings : CreateNewSettings();
        }

        internal static SerializedObject GetSerializedSettings()
        {
            return new SerializedObject(GetOrCreateSettings());
        }

        [MenuItem("ArcticFox/Vibrant Hierarchy/Create Settings")]
        private static VibrantHierarchySettings CreateNewSettings()
        {
            VibrantHierarchySettings settings;
            settings = ScriptableObject.CreateInstance<VibrantHierarchySettings>();

            AssetDatabase.CreateAsset(settings, MyCustomSettingsPath);
            AssetDatabase.SaveAssets();
            return settings;
        }
    }
}