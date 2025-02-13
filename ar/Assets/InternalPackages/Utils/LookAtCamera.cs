using UnityEngine;

namespace PhishAR.Utils
{
    public class LookAtCamera : MonoBehaviour
    {
        [SerializeField] private bool _onlyRotateY;

        private Transform _cameraTransform;

        private void Start()
        {
            _cameraTransform = Camera.main.transform;
        }

        private void Update()
        {
            var targetY = _onlyRotateY ? transform.position.y : _cameraTransform.position.y;
            transform.LookAt(new Vector3(_cameraTransform.position.x, targetY, _cameraTransform.position.z));
        }
    }
}
