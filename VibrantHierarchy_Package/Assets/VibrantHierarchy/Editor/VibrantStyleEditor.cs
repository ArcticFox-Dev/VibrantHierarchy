using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace VibrantHierarchy.Editor
{
    [CustomPropertyDrawer(typeof(VibrantStyle))]
    public class VibrantStyleEditor : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            VisualElement myInspector = new VisualElement();

            var token = property.FindPropertyRelative("Token");
            var backgroundColor = property.FindPropertyRelative("BackgroundColor");
            var textColor = property.FindPropertyRelative("TextColor");
            var font = property.FindPropertyRelative("Font");
            var fontSize = property.FindPropertyRelative("FontSize");
            var fontStyle = property.FindPropertyRelative("FontStyle");
            var alignment = property.FindPropertyRelative("Alignment");

            var box = GetExampleBox();
            var exampleLabel = GetExampleLabel();

            box.Add(exampleLabel);
            
            var propertyFoldout = new Foldout();
            propertyFoldout.text = token.stringValue;

            var tokenField = new PropertyField(token);
            var backgroundColorField = new PropertyField(backgroundColor);
            var textColorField = new PropertyField(textColor);
            var fontField = new PropertyField(font);
            var fontSizeField = new PropertyField(fontSize);
            var fontStyleField = new PropertyField(fontStyle);
            var alignmentField = new PropertyField(alignment);


            tokenField.RegisterValueChangeCallback(
                (e => propertyFoldout.text = e.changedProperty.stringValue));
            backgroundColorField.RegisterValueChangeCallback(
                e => exampleLabel.style.backgroundColor = e.changedProperty.colorValue);
            textColorField.RegisterValueChangeCallback(
                e => exampleLabel.style.color = e.changedProperty.colorValue);
            fontField.RegisterValueChangeCallback(
                e =>
                {
                    var parent = property.serializedObject.targetObject as VibrantHierarchySettings;
                    if(parent == null) return;
                    var style = parent.Styles.Find(design => design.Token == token.stringValue);
                    if (style.Font != null)
                    {
                        exampleLabel.style.unityFontDefinition = new FontDefinition
                        {
                            fontAsset = null, font = style.Font
                        };
                    }
                });
            fontSizeField.RegisterValueChangeCallback(
                e => exampleLabel.style.fontSize = e.changedProperty.intValue);
            fontStyleField.RegisterValueChangeCallback(
                e => exampleLabel.style.unityFontStyleAndWeight = new StyleEnum<FontStyle>((FontStyle)e.changedProperty.enumValueIndex));
            alignmentField.RegisterValueChangeCallback(
                e => exampleLabel.style.unityTextAlign = new StyleEnum<TextAnchor>((TextAnchor)e.changedProperty.enumValueIndex));


            propertyFoldout.Add(box);
            
            propertyFoldout.Add(tokenField);
            propertyFoldout.Add(backgroundColorField);
            propertyFoldout.Add(textColorField);
            propertyFoldout.Add(fontField);
            propertyFoldout.Add(fontSizeField);
            propertyFoldout.Add(fontStyleField);
            propertyFoldout.Add(alignmentField);

            myInspector.Add(propertyFoldout);
            
            // Return the finished inspector UI
            return myInspector;
        }

        private static GroupBox GetExampleBox()
        {
            var box = new GroupBox();
            box.style.borderBottomColor = new StyleColor(Color.gray);
            box.style.borderTopColor = new StyleColor(Color.gray);
            box.style.borderLeftColor = new StyleColor(Color.gray);
            box.style.borderRightColor = new StyleColor(Color.gray);
            box.style.borderBottomWidth = 2.5f;
            box.style.borderTopWidth = 2.5f;
            box.style.borderLeftWidth = 2.5f;
            box.style.borderRightWidth = 2.5f;
            return box;
        }

        private static Label GetExampleLabel()
        {
            var exampleLabel = new Label("Example");
            
            exampleLabel.style.marginLeft = 5;
            exampleLabel.style.marginRight = 10;
            exampleLabel.style.unityTextAlign = new StyleEnum<TextAnchor>(TextAnchor.MiddleCenter);

            return exampleLabel;
        }
    }
}