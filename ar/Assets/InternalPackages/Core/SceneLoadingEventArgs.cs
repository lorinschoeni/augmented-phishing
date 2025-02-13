using System;

namespace PhishAR.Core
{
    public class SceneLoadingEventArgs : EventArgs
    {
        public SceneLoadingEventArgs(string sceneName)
        {
            SceneName = sceneName;
        }

        public string SceneName { get; set; }
    }
}
