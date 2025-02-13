using System;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit.UI;
using PhishAR.Core.Services;
using PhishAR.ETHTB.Annotations;
using PhishAR.ETHTB.Models;
using PhishAR.ETHTB.QR;
using UnityEngine;

namespace PhishAR.ETHTB.Services
{
    public class AnnotationDisplayService : MonoBehaviour
    {
        [SerializeField] private Annotation _annotationPrefab;
        [SerializeField] private QrAnchor _anchor;
        [SerializeField] private Interactable _showAllToggle;

        private readonly Dictionary<string, Annotation> _annotations = new Dictionary<string, Annotation>();
        private DataContext _dataContext;

        private void Start()
        {
            ServiceLocator.TryGetService(out _dataContext);
            _dataContext.OverlayModelUpdated += OnOverlayModelUpdated;

            _showAllToggle.OnClick.AddListener(() =>
            {
                foreach (var annotation in _annotations.Values)
                    if (annotation.IsVisible != _showAllToggle.IsToggled)
                        annotation.Toggle();
            });
        }

        private void OnOverlayModelUpdated(object sender, EventArgs e)
        {
            _showAllToggle.gameObject.SetActive(_dataContext.OverlayModel.Target.DisplayState == DisplayState.OnToggle);

            if (_dataContext.WebQr == null) return;

            foreach (var annotationModel in _dataContext.OverlayModel.Annotations)
            {
                if (!_annotations.ContainsKey(annotationModel.Id))
                    _annotations.Add(annotationModel.Id, Instantiate(_annotationPrefab, _anchor.transform));

                _annotations[annotationModel.Id]
                    .Set(annotationModel, _dataContext.ScreenWidth, _dataContext.ScreenHeight);
            }

            foreach (var annotation in _annotations)
                annotation.Value.ShowVisuals(false);

            if (_dataContext.OverlayModel.Target.DisplayState == DisplayState.OnStart)
                foreach (var annotationModel in _dataContext.OverlayModel.Annotations)
                    _annotations[annotationModel.Id].ShowVisuals(true);
        }
    }
}
