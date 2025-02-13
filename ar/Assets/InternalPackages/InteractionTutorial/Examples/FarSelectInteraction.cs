using Microsoft.MixedReality.Toolkit.UI;
using UnityEngine;

namespace PhishAR.InteractionTutorial.Examples
{
    public class FarSelectInteraction : InteractionExample
    {
        [SerializeField] private Interactable _pressButton;

        protected override void Start()
        {
            base.Start();
            
            _pressButton.OnClick.AddListener(OnCurrentInteractionFinished);
        }
    }
}
