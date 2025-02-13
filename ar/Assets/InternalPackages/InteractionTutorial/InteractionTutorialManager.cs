using System;
using UnityEngine;

namespace PhishAR.InteractionTutorial
{
    public class InteractionTutorialManager : MonoBehaviour
    {
        private const float InteractionInstantiateForwardOffset = 0.6f;
        private const float InteractionInstantiateUpOffset = -0.15f;
        private const float InteractionInstantiateRightOffset = 0.05f;
        [SerializeField] private InteractionExample[] _interactionExamplePrefabs;
        [SerializeField] private GameObject _mixedRealitySceneContentObject;

        private Transform _cameraTransform;
        private InteractionExample _currentExample;

        private int _currentExampleIndex = -1;
        private Vector3 _examplePosition;
        private Quaternion? _exampleRotation;

        public event EventHandler TutorialStarted;
        public event EventHandler TutorialFinished;
        public event EventHandler CurrentInteractionFinished;

        public void StartTutorial()
        {
            _currentExampleIndex = -1;

            _examplePosition = Vector3.forward * InteractionInstantiateForwardOffset +
                               Vector3.up * InteractionInstantiateUpOffset +
                               Vector3.right * InteractionInstantiateRightOffset;

            SetUpNextInteractionExample(_currentExampleIndex + 1);

            TutorialStarted?.Invoke(this, EventArgs.Empty);
        }

        public void SetCurrentExampleActive(bool value)
        {
            _currentExample.gameObject.SetActive(value);
        }

        private void FinishTutorial()
        {
            if (_currentExample != null) Destroy(_currentExample.gameObject);
            _currentExampleIndex = -1;

            TutorialFinished?.Invoke(this, EventArgs.Empty);
        }

        public void SetUpNextInteractionExample(int nextIndex)
        {
            DestroyCurrentExample();

            if (nextIndex >= _interactionExamplePrefabs.Length)
            {
                _currentExampleIndex = -1;
                return;
            }

            _currentExampleIndex = nextIndex;
            _currentExample =
                Instantiate(_interactionExamplePrefabs[_currentExampleIndex].gameObject, _examplePosition,
                    Quaternion.identity, _mixedRealitySceneContentObject.transform).GetComponent<InteractionExample>();

            //SetCurrentExampleRotation();

            SetUpExampleEvents();
        }

        public void DestroyCurrentExample()
        {
            if (_currentExample != null) Destroy(_currentExample.gameObject);
        }

        private void SetCurrentExampleRotation()
        {
            if (_exampleRotation == null)
            {
                _currentExample.transform.LookAt(_cameraTransform, Vector3.up);
                var nonModifiedRotation = _currentExample.transform.rotation;
                _currentExample.transform.rotation = new Quaternion(0, nonModifiedRotation.y, 0, nonModifiedRotation.w);
                _currentExample.transform.eulerAngles = Vector3.up * (_currentExample.transform.eulerAngles.y + 180);

                _exampleRotation = _currentExample.transform.rotation;
            }
            else
            {
                _currentExample.transform.rotation = _exampleRotation.Value;
            }
        }

        private void SetUpExampleEvents()
        {
            if (_currentExampleIndex == _interactionExamplePrefabs.Length - 1) _currentExample.DisableNextButton();
            if (_currentExampleIndex == 0) _currentExample.DisablePreviousButton();

            _currentExample.CurrentInteractionFinished += (sender, args) => CurrentInteractionFinished?.Invoke(this,
                EventArgs.Empty);

            // _currentExample.CurrentInteractionFinished += (sender, args) => SetUpNextInteractionExample(_currentExampleIndex + 1);
            // _currentExample.PreviousButtonClicked += (sender, args) => SetUpNextInteractionExample(_currentExampleIndex - 1);
            // _currentExample.NextButtonClicked += (sender, args) => SetUpNextInteractionExample(_currentExampleIndex + 1);
            // _currentExample.FinishButtonClicked += (sender, args) => FinishTutorial();
        }
    }
}
