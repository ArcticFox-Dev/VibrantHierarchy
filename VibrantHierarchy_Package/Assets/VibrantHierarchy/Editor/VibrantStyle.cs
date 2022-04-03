using System;
using UnityEngine;

namespace VibrantHierarchy.Editor
{
    [Serializable]
    public class VibrantStyle
    {
        [Tooltip("Apply to GameObjects with names matching this Token.")]
        public string Token;
        [SerializeField]
        [Tooltip("Background Color - Remember to set the alpha to more than 0")]
        public Color32 BackgroundColor = new Color32(255, 255, 255, 255);
        [SerializeField]
        [Tooltip("Text Color - Remember to set the alpha to more than 0")]
        public Color32 TextColor = new Color32(0, 0, 0, 255);
        [SerializeField]
        public Font Font;
        [SerializeField]
        [Range(8,14)]
        public int FontSize = 12;
        [SerializeField]
        public FontStyle FontStyle = FontStyle.Normal;
        [SerializeField]
        public TextAnchor Alignment = TextAnchor.UpperLeft;
    }
}