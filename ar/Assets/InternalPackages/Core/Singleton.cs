using UnityEngine;

namespace PhishAR.Core
{
    /// <summary>
    ///     Inherit from this base class to create a singleton.
    ///     e.g. public class MyClassName : Singleton<MyClassName> {}
    /// </summary>
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        // Check to see if we're about to be destroyed.
        private static bool _isShuttingDown;
        private static readonly object _lock = new object();
        private static T _instance;

        /// <summary>
        ///     Access singleton instance through this propriety.
        /// </summary>
        public static T Instance
        {
            get
            {
                if (_isShuttingDown)
                {
                    Debug.LogWarning("[Singleton] Instance '" + typeof(T) +
                                     "' already destroyed. Returning null.");
                    return null;
                }

                lock (_lock)
                {
                    if (_instance != null) return _instance;
                    // Search for existing instance.
                    _instance = (T) FindObjectOfType(typeof(T));

                    // Create new instance if one doesn't already exist.
                    if (_instance != null) return _instance;
                    // Need to create a new GameObject to attach the singleton to.
                    var singletonObject = new GameObject();
                    _instance = singletonObject.AddComponent<T>();
                    singletonObject.name = typeof(T) + " (Singleton)";

                    // Make instance persistent.
                    DontDestroyOnLoad(singletonObject);

                    return _instance;
                }
            }
        }

        private void OnDestroy()
        {
            _isShuttingDown = true;
        }

        private void OnApplicationQuit()
        {
            _isShuttingDown = true;
        }
    }
}
