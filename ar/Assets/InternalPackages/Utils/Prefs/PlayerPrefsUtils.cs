using UnityEngine;

namespace PhishAR.Utils.Prefs
{
    public class PlayerPrefsUtils : IPlayerPrefsUtils
    {
        public void SaveVector3(Vector3 vector, string vectorName)
        {
            PlayerPrefs.SetFloat(GetXAxisName(vectorName), vector.x);
            PlayerPrefs.SetFloat(GetYAxisName(vectorName), vector.y);
            PlayerPrefs.SetFloat(GetZAxisName(vectorName), vector.z);

            Debug.Log($"Saved vector {vectorName}");
        }

        public bool TryLoadVector3(string vectorName, out Vector3 vector)
        {
            if (!PlayerPrefs.HasKey(GetXAxisName(vectorName)) || !PlayerPrefs.HasKey(GetYAxisName(vectorName)) ||
                !PlayerPrefs.HasKey(GetZAxisName(vectorName)))
            {
                Debug.Log($"Vector {vectorName} doesn't exist in player prefs");
                vector = Vector3.zero;
                return false;
            }

            vector = new Vector3(PlayerPrefs.GetFloat(GetXAxisName(vectorName)),
                PlayerPrefs.GetFloat(GetYAxisName(vectorName)),
                PlayerPrefs.GetFloat(GetZAxisName(vectorName)));
            return true;
        }

        private string GetXAxisName(string vectorName)
        {
            return $"{vectorName}X";
        }

        private string GetYAxisName(string vectorName)
        {
            return $"{vectorName}Y";
        }

        private string GetZAxisName(string vectorName)
        {
            return $"{vectorName}Z";
        }
    }
}
