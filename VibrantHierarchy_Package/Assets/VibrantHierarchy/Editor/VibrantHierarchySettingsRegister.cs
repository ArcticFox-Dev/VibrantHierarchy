using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;

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
                    var settings = VibrantHierarchySettings.GetSerializedSettings();

                    // rootElement is a VisualElement. If you add any children to it, the OnGUI function
                    // isn't called because the SettingsProvider uses the UIElements drawing framework.

                    rootElement.Add(new PropertyField(settings.FindProperty("Styles")));

                    rootElement.Bind(settings);
                },

                // Populate the search keywords to enable smart search filtering and label highlighting:
                keywords = new HashSet<string>(new[] { "Vibrant", "Hierarchy" })
            };

            return provider;
        }
    }
}