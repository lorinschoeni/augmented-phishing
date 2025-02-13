using PhishAR.Core.Services;
using UnityEngine;

namespace PhishAR.Utils.Prefs
{
    public interface IPlayerPrefsUtils : IService
    {
        void SaveVector3(Vector3 vector, string vectorName);

        bool TryLoadVector3(string vectorName, out Vector3 vector);
    }
}
