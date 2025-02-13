using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit.UI;
using PhishAR.Core.Services;
using PhishAR.ETHTB.Models;
using PhishAR.ETHTB.Services;
using UnityEngine;
using UnityEngine.UI;

namespace PhishAR.ETHTB.Annotations
{
    public class Annotation : MonoBehaviour
    {
        private static readonly int ColorPropertyId = Shader.PropertyToID("_Color");

        [SerializeField] private GameObject _rectangle;
        [SerializeField] private MeshRenderer _rectangleRenderer;
        [SerializeField] private AnnotationTooltipBase _tooltip;
        [SerializeField] private Image _icon;
        [SerializeField] private List<Sprite> _icons;
        [SerializeField] private GameObject _canvas;
        [SerializeField] private GameObject _canvasIcon;

        private DataContext _dataContext;
        [field: SerializeField] public Interactable ToggleEnabled { get; private set; }

        public bool IsVisible { get; set; } = true;

        private void Start()
        {
            ToggleEnabled.OnClick.AddListener(Toggle);
        }

        public void Set(AnnotationModel annotation, float screenWidth, float screenHeight)
        {
            ServiceLocator.TryGetService(out _dataContext);
            ToggleEnabled.gameObject.SetActive(_dataContext.OverlayModel.Target.DisplayState == DisplayState.OnClick);
            var centerX = screenWidth * (annotation.OffsetX + annotation.Width / 2 - 0.5f);
            var centerY = screenHeight * (0.5f - (annotation.OffsetY + annotation.Height / 2));

            transform.localPosition = new Vector3(centerX, centerY, -0.01f);
            _rectangle.transform.localScale =
                new Vector3(annotation.Width * screenWidth, annotation.Height * screenHeight, 0.1f);

            SetRectangleColor(FixDarkColor(annotation.BorderColor));

            _tooltip.SetTextColor(FixDarkColor(annotation.TextColor));
            _tooltip.SetText(annotation.Label);
            _tooltip.ApplyFontSizeModifier(annotation.TextSize);

            _icon.sprite = _icons.Find(icon => icon.name == annotation.Icon?.Remove(annotation.Icon.IndexOf('.')));
            _icon.enabled = _icon.sprite != null;
        }

        private Color FixDarkColor(Color color)
        {
            if (color.r + color.g + color.b < .5f) color = Color.white;
            return color;
        }

        public void SetRectangleColor(Color color)
        {
            // Alpha should be transparent so only border is displayed on this material. 
            color.a = 0f;
            _rectangleRenderer.material.SetColor(ColorPropertyId, color);
        }

        public void Toggle()
        {
            ShowVisuals(!IsVisible);
        }

        public void ShowVisuals(bool visible)
        {
            IsVisible = visible;
            _rectangleRenderer.enabled = visible;
            _canvas.SetActive(visible);
            _canvasIcon.SetActive(visible);
        }
    }
}
