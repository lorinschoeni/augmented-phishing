using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PhishAR.InteractionTutorial.Examples
{
    public class NearObjectManipulationInteraction : InteractionExample
    {
        private const float DelayFinishedInteractionTimeOut = 0.5f;
        [SerializeField] private NearObjectManipulation _scaleInteraction;
        [SerializeField] private NearObjectManipulation _moveInteraction;
        [SerializeField] private NearObjectManipulation _rotateInteraction;

        [SerializeField] private MeshRenderer _meshRenderer;
        [SerializeField] private Material _successfullyPositionedMaterial;
        [SerializeField] private GameObject _manipulableObject;

        private NearObjectManipulation _currentManipulation;
        private bool _didFinish;
        private Queue<NearObjectManipulation> _orderedManipulationTypes;

        protected override void Start()
        {
            base.Start();

            _orderedManipulationTypes =
                new Queue<NearObjectManipulation>(new[] {_scaleInteraction, _moveInteraction, _rotateInteraction});

            StartCoroutine(ChangeStatusAtEndOfFrame());
        }

        private void Update()
        {
            if (!_currentManipulation.IsConstraintSatisfied()) return;

            _currentManipulation.FinishManipulation();
            StartNextManipulationType();
        }

        private IEnumerator ChangeStatusAtEndOfFrame()
        {
            yield return new WaitForEndOfFrame();
            StartNextManipulationType();
        }

        private void StartNextManipulationType()
        {
            if (_orderedManipulationTypes.Count == 0)
            {
                _meshRenderer.material = _successfullyPositionedMaterial;

                StartCoroutine(DelayFinishedInteraction());
                return;
            }

            _currentManipulation = _orderedManipulationTypes.Dequeue();
            _currentManipulation.StartManipulation();
        }

        private IEnumerator DelayFinishedInteraction()
        {
            yield return new WaitForSeconds(DelayFinishedInteractionTimeOut);

            if (_didFinish) yield break;

            _didFinish = true;
            OnCurrentInteractionFinished();
        }
    }
}
