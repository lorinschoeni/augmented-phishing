using System.Collections;
using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.MixedReality.Toolkit.UI.BoundsControl;
using UnityEngine;

namespace PhishAR.InteractionTutorial.Examples
{
    public abstract class NearObjectManipulation : MonoBehaviour
    {
        [SerializeField] protected GameObject _targetObject;
        [SerializeField] protected GameObject _manipulableObject;
        
        [SerializeField] private TransformConstraint _constraint;
        [SerializeField] private BoundsControl _boundsControl;
        
        [SerializeField] private GameObject _handCoach;
        [SerializeField] private GameObject _text;

        private void Start()
        {
            DisableManipulation();
        }

        public abstract bool IsConstraintSatisfied();

        public void StartManipulation()
        {
            _handCoach.SetActive(true);
            _text.SetActive(true);
            
            EnableConstraint(false);
        }

        public virtual void FinishManipulation() => DisableManipulation();

        protected void DisableBoundsUntilNextFrame()
        {
            _boundsControl.Active = false;
            StartCoroutine(ActivateBoundsControlNextFrame());
        }

        private void DisableManipulation()
        {
            EnableConstraint(true);

            _handCoach.SetActive(false);
            _text.SetActive(false);
        }
        
        private void EnableConstraint(bool enabledConstraint)
        {
            _constraint.enabled = enabledConstraint;
        }

        private IEnumerator ActivateBoundsControlNextFrame()
        {
            yield return null;

            _boundsControl.Active = true;
        }
    }
}
