using UnityEngine;

namespace PhishAR.Utils
{
    public class CameraPose : MonoBehaviour
    {
        private Transform _camera;

        private void Start()
        {
            _camera = Camera.main.transform;
        }

        private void Update()
        {
            transform.SetPositionAndRotation(
                _camera.position,
                _camera.rotation
            );
        }
    }
}
