using System;
using UnityEngine;

namespace PhishAR.Utils
{
    [Flags]
    public enum Axis
    {
        X = 1 << 0,
        Y = 1 << 1,
        Z = 1 << 2
    }

    public class CopyWorldPositionAndRotation : MonoBehaviour
    {
        [field: SerializeField] public Transform Target { get; set; }

        [field: SerializeField] public Axis PositionAxisToCopy { get; set; }
        [field: SerializeField] public Axis RotationAxisToCopy { get; set; }

        private void Update()
        {
            if (Target == null) return;

            transform.position = GetNewPosition();
            transform.eulerAngles = GetNewRotation();
        }

        private Vector3 GetNewPosition()
        {
            var newPosition = transform.position;

            if ((PositionAxisToCopy & Axis.X) != 0) newPosition.x = Target.position.x;
            if ((PositionAxisToCopy & Axis.Y) != 0) newPosition.y = Target.position.y;
            if ((PositionAxisToCopy & Axis.Z) != 0) newPosition.z = Target.position.z;

            return newPosition;
        }

        private Vector3 GetNewRotation()
        {
            var newRotation = transform.eulerAngles;

            if ((RotationAxisToCopy & Axis.X) != 0) newRotation.x = Target.eulerAngles.x;
            if ((RotationAxisToCopy & Axis.Y) != 0) newRotation.y = Target.eulerAngles.y;
            if ((RotationAxisToCopy & Axis.Z) != 0) newRotation.z = Target.eulerAngles.z;

            return newRotation;
        }
    }
}
