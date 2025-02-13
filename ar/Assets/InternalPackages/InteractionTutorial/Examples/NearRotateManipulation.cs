using UnityEngine;

namespace PhishAR.InteractionTutorial.Examples
{
    public class NearRotateManipulation : NearObjectManipulation
    {
        private Quaternion? _snappedRotation;

        private const float RotationThreshold = 10f;

        private void Update()
        {
            if (_snappedRotation != null) _manipulableObject.transform.rotation = _snappedRotation.Value;
        }

        public override bool IsConstraintSatisfied()
        {
            return Quaternion.Angle(_targetObject.transform.rotation, _manipulableObject.transform.rotation) <= RotationThreshold;
        }

        public override void FinishManipulation()
        {
            base.FinishManipulation();

            DisableBoundsUntilNextFrame();
            
            _snappedRotation = _targetObject.transform.rotation;
            _manipulableObject.transform.rotation = _snappedRotation.Value;
        }
    }
}
