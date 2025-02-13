using System;
using System.Linq;
using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Extensions.SceneTransitions;
using Microsoft.MixedReality.Toolkit.SceneSystem;
using Unity.Profiling;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PhishAR.Core
{
    public class SceneLoader
    {
        private static readonly ProfilerMarker LoadContentPerfMarker =
            new ProfilerMarker("[MRTK] LoadContentScene.LoadContent");

        private readonly LoadSceneMode _loadSceneMode = LoadSceneMode.Single;
        private readonly IMixedRealitySceneSystem _sceneSystem;

        public SceneLoader()
        {
            _sceneSystem = CoreServices.SceneSystem;
            if (_sceneSystem == null)
            {
                Debug.LogError("Scene system is missing, make sure to wait until the system is ready");
                return;
            }

            _sceneSystem.OnSceneLoaded += OnSceneLoaded;
            _sceneSystem.OnSceneUnloaded += OnSceneUnloaded;
        }

        public EventHandler<SceneLoadingEventArgs> SceneLoaded { get; set; }
        public EventHandler<SceneLoadingEventArgs> SceneUnloaded { get; set; }

        public void LoadContentScene(string sceneName)
        {
            var contentSceneName = _sceneSystem.ContentSceneNames.FirstOrDefault(name =>
                name.Equals(sceneName, StringComparison.InvariantCultureIgnoreCase));
            if (contentSceneName == null)
            {
                Debug.LogError($"Scene not assigned: {sceneName}");
                return;
            }

            LoadScene(contentSceneName);
        }

        public void UnloadContentScene(string sceneName)
        {
            var contentSceneName = _sceneSystem.ContentSceneNames.FirstOrDefault(name =>
                name.Equals(sceneName, StringComparison.InvariantCultureIgnoreCase));
            if (contentSceneName == null)
            {
                Debug.LogError($"Scene not assigned: {sceneName}");
                return;
            }

            _sceneSystem.UnloadContent(sceneName);
        }

        private void OnSceneLoaded(string sceneName)
        {
            SceneLoaded?.Invoke(this, new SceneLoadingEventArgs(sceneName));
        }

        private void OnSceneUnloaded(string sceneName)
        {
            SceneUnloaded?.Invoke(this, new SceneLoadingEventArgs(sceneName));
        }

        private void LoadScene(string sceneName)
        {
            using (LoadContentPerfMarker.Auto())
            {
                var transitions = MixedRealityToolkit.Instance.GetService<ISceneTransitionService>();
                if (transitions.TransitionInProgress) return;
                transitions.DoSceneTransition(() => CoreServices.SceneSystem.LoadContent(sceneName, _loadSceneMode));
            }
        }
    }
}
