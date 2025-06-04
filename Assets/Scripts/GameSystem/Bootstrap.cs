using System.Collections;
using UnityEngine;
using Personal.Services;

namespace Personal.MobileGameX.GameSystem
{
    public class Bootstrap : MonoBehaviour
    {
        private void Awake()
        {
            StartCoroutine(InitializeApplicationCoroutine());
        }

        private IEnumerator InitializeApplicationCoroutine()
        {
            Debug.Log("Starting system initialization.");
            yield return new WaitForSeconds(1f);

            Debug.Log("Waiting for service initialization.");
            yield return new WaitUntil(() => ServiceInitializer.IsInitialized);

            Debug.Log("Loading default scene.");
            yield return LoadDefaultSceneCoroutine();
        }

        private IEnumerator LoadDefaultSceneCoroutine()
        {
            var sceneLoader = ServiceLocator.Instance.Get<ISceneLoader>();
            sceneLoader.LoadScene(1);

            yield return new WaitWhile(() => sceneLoader.IsLoading);
            sceneLoader.UnloadScene(0);
        }
    }
}