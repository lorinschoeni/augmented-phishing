using UnityEngine;

namespace PhishAR.Utils
{
    public class FixedRotation : MonoBehaviour
    {
        [SerializeField] private bool _fixedX;
        [SerializeField] private bool _fixedY;
        [SerializeField] private bool _fixedZ;

        [SerializeField] private Vector3 _fixedRotationValues;

        private void Update()
        {
            var rotationX = _fixedX ? _fixedRotationValues.x : transform.eulerAngles.x;
            var rotationY = _fixedY ? _fixedRotationValues.y : transform.eulerAngles.y;
            var rotationZ = _fixedX ? _fixedRotationValues.z : transform.eulerAngles.z;

            transform.eulerAngles = new Vector3(rotationX, rotationY, rotationZ);
        }
    }
}
