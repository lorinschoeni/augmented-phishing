using Microsoft.MixedReality.Toolkit.UI;
using System;
using UnityEngine;

namespace PhishAR.InteractionTutorial
{
    public abstract class InteractionExample : MonoBehaviour
    {
        [SerializeField] private Interactable _previousButton;
        [SerializeField] private Interactable _nextButton;
        [SerializeField] private Interactable _finishButton;

        public event EventHandler CurrentInteractionFinished;
        public event EventHandler PreviousButtonClicked;
        public event EventHandler NextButtonClicked;
        public event EventHandler FinishButtonClicked;
        
        protected virtual void Start()
        {
            _previousButton.OnClick.AddListener(() => PreviousButtonClicked?.Invoke(this, EventArgs.Empty));
            _nextButton.OnClick.AddListener(() => NextButtonClicked?.Invoke(this, EventArgs.Empty));
            _finishButton.OnClick.AddListener(() => FinishButtonClicked?.Invoke(this, EventArgs.Empty));
        }

        public void DisablePreviousButton() => _previousButton.gameObject.SetActive(false);
        public void DisableNextButton() => _nextButton.gameObject.SetActive(false);

        protected void OnCurrentInteractionFinished() => CurrentInteractionFinished?.Invoke(this, EventArgs.Empty);
    }
}
