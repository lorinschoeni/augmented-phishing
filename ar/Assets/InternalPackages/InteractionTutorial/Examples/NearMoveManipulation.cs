using UnityEngine;

namespace PhishAR.InteractionTutorial.Examples
{
    public class NearMoveManipulation : NearObjectManipulation 
    {
        private Vector3? _snappedPosition;
        
        private const float MoveThreshold = 0.05f;
        
        private void Update()
        {
            if (_snappedPosition != null) _manipulableObject.transform.position = _snappedPosition.Value;
        }

        public override bool IsConstraintSatisfied()
        {
            return Vector3.Distance(_targetObject.transform.position, _manipulableObject.transform.position) <= MoveThreshold;
        }

        public override void FinishManipulation()
        {
            base.FinishManipulation();

            DisableBoundsUntilNextFrame();
            
            _snappedPosition = _targetObject.transform.position;
            _manipulableObject.transform.position = _snappedPosition.Value;
        }
    }
}
