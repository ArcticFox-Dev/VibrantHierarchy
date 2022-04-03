using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace VibrantHierarchy.Editor
{
    public class VibrantHierarchySettingsRegister
    {
        [SettingsProvider]
        public static SettingsProvider CreateMyCustomSettingsProvider()
        {
            // First parameter is the path in the Settings window.
            // Second parameter is the scope of this setting: it only appears in the Settings window for the Project scope.
            var provider = new SettingsProvider($"Project/{nameof(VibrantHierarchySettings)}", SettingsScope.Project)
            {
                // activateHandler is called when the user clicks on the Settings item in the Settings window.
                activateHandler = (searchContext, rootElement) =>
                {
                    var settingsPaths = AssetDatabase.FindAssets($"t:{nameof(VibrantHierarchySettings)}");
                    if (settingsPaths.Length == 1)
                    {
                        DrawStylesSettings(rootElement);
                    }
                    else
                    {
                        var textBox = new TextField();
                        textBox.value = "It seems you haven't started the plugin in this project yet.";
                        textBox.SetEnabled(false);
                        var button = new Button();
                        button.text = "Start Plugin";
                        button.clicked += () =>
                        {
                            VibrantHierarchySettings.GetOrCreateSettings();
                            VibrantHierarchyRenderer.RegisterRenderer();
                            rootElement.Clear();
                            DrawStylesSettings(rootElement);
                        };
                        rootElement.Add(textBox);
                        rootElement.Add(button);
                    }
                },

                // Populate the search keywords to enable smart search filtering and label highlighting:
                keywords = new HashSet<string>(new[] { "Vibrant", "Hierarchy" })
            };

            return provider;
        }

        private static void DrawStylesSettings(VisualElement rootElement)
        {
            var settings = VibrantHierarchySettings.GetSerializedSettings();

            rootElement.Add(new PropertyField(settings.FindProperty("Styles")));

            rootElement.Bind(settings);
        }
    }
}