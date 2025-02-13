using Microsoft.MixedReality.Toolkit.UI;
using UnityEngine;

namespace PhishAR.QR.Detection.UI
{
    public class Notify : MonoBehaviour
    {
        [SerializeField] private ToolTip _tooltip;

        private void Start()
        {
            transform.LookAt(Camera.main.transform);
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + 180, transform.eulerAngles.z);
        }

        public void Show(string message, float destroyDelay = 3f)
        {
            _tooltip.ToolTipText = message;
            Destroy(gameObject, destroyDelay);
        }
    }
}
