using UnityEngine;

namespace PhishAR.ETHTB.Annotations
{
    public abstract class AnnotationTooltipBase : MonoBehaviour
    {
        [field: SerializeField] public float DefaultFontSize { get; set; }
        public abstract void SetText(string text);
        public abstract void SetTextColor(Color color);
        public abstract void ApplyFontSizeModifier(float modifier);
    }
}
