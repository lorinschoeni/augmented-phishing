using UnityEngine;

namespace PhishAR.InteractionTutorial.Examples
{
    public class FarMoveInteraction : InteractionExample
    {
        private const float DistanceThreshold = 0.6f;
        [SerializeField] private GameObject _objectToMove;
        private bool _didFinish;

        private void Update()
        {
            if (_didFinish) return;
            if (Vector3.Distance(Camera.main.transform.position, _objectToMove.transform.position) <= DistanceThreshold)
            {
                OnCurrentInteractionFinished();
                _didFinish = true;
            }
        }
    }
}
