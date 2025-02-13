using Microsoft.MixedReality.Toolkit.UI;
using UnityEngine;

namespace PhishAR.InteractionTutorial.Examples
{
    public class NearSelectInteraction : InteractionExample
    {
        [SerializeField] private GameObject _toggleButtonOutline;
        [SerializeField] private GameObject _pressButtonOutline;
        [SerializeField] private Interactable _toggleButton;
        [SerializeField] private Interactable _pressButton;

        [SerializeField] private MeshRenderer _toggleButtonBackplateMeshRenderer;
        [SerializeField] private MeshRenderer _pressButtonBackplateMeshRenderer;
        [SerializeField] private Material _hololensButtonMaterial;
        [SerializeField] private Material _highlightedMaterial;

        [SerializeField] private GameObject _handCoach;

        private const float ToggleButtonPositionX = -0.024f;
        
        protected override void Start()
        {
            base.Start();
            
            _pressButton.gameObject.SetActive(false);
            _pressButtonOutline.SetActive(false);

            InteractableOnToggleReceiver toggleReceiver = _toggleButton.GetReceiver<InteractableOnToggleReceiver>();
            toggleReceiver.OnSelect.AddListener(OnToggleSelected);
            toggleReceiver.OnDeselect.AddListener(OnToggleDeselected);

            _pressButton.OnClick.AddListener(OnCurrentInteractionFinished);
        }
        
        private void OnToggleDeselected()
        {
            _pressButton.gameObject.SetActive(false);

            _toggleButton.gameObject.transform.localPosition = Vector3.zero;
            _toggleButtonBackplateMeshRenderer.material = _highlightedMaterial;
        }
        
        private void OnToggleSelected()
        {
            _pressButton.gameObject.SetActive(true);
            _pressButtonOutline.SetActive(true);
            _pressButtonBackplateMeshRenderer.material = _highlightedMaterial;

            _toggleButton.gameObject.transform.localPosition = new Vector3(ToggleButtonPositionX, 0, 0);
            _toggleButtonBackplateMeshRenderer.material = _hololensButtonMaterial;

            _handCoach.SetActive(false);
        }
    }
}
