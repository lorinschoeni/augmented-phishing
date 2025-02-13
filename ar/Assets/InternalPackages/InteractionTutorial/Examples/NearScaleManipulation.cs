using UnityEngine;

namespace PhishAR.InteractionTutorial.Examples
{
    public class NearScaleManipulation : NearObjectManipulation
    {
        private Vector3? _snappedScale;
        
        private const float ScalingThreshold = 0.01f;

        private void Update()
        {
            if (_snappedScale != null) _manipulableObject.transform.localScale = _snappedScale.Value;
        }

        public override bool IsConstraintSatisfied()
        {
            return Vector3.Distance(_targetObject.transform.localScale, _manipulableObject.transform.localScale) <= ScalingThreshold;
        }

        public override void FinishManipulation()
        {
            base.FinishManipulation();

            DisableBoundsUntilNextFrame();
            
            _snappedScale = _targetObject.transform.localScale;
            _manipulableObject.transform.localScale = _snappedScale.Value;
        }
    }
}
