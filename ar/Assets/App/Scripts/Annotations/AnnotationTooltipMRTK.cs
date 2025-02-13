using System;
using Microsoft.MixedReality.Toolkit.UI;
using TMPro;
using UnityEngine;

namespace PhishAR.ETHTB.Annotations
{
    public class AnnotationTooltipMRTK : AnnotationTooltipBase
    {
        [SerializeField] private ToolTip _toolTip;
        [SerializeField] private TMP_Text _text;

        public override void SetText(string text)
        {
            _toolTip.ToolTipText = text;
        }

        public override void SetTextColor(Color color)
        {
            _text.color = color;
        }

        public override void ApplyFontSizeModifier(float modifier)
        {
            _toolTip.FontSize = (int) Math.Round(DefaultFontSize * modifier);
        }
    }
}
