using TMPro;
using UnityEngine;

namespace PhishAR.ETHTB.Annotations
{
    public class AnnotationTooltip : AnnotationTooltipBase
    {
        [SerializeField] private TMP_Text _text;

        public override void SetText(string text) => _text.text = text;

        public override void SetTextColor(Color color) => _text.color = color;

        public override void ApplyFontSizeModifier(float modifier) => _text.fontSize = DefaultFontSize * modifier;
    }
}
