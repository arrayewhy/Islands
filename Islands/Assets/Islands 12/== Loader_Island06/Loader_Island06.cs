using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Loader_Island06 : MonoBehaviour
{
    [Header ("Components:")]
    [SerializeField]
    private Image loadingBarFill;

    public void LoadScene (string sceneName) { StartCoroutine (LoadSceneAsync (sceneName)); }

    private IEnumerator LoadSceneAsync (string sceneName)
	{
        AsyncOperation operation = SceneManager.LoadSceneAsync (sceneName);

		while (!operation.isDone)
		{
            print (operation.progress);
            float progressValue = Mathf.Clamp01 (operation.progress / 0.9f);

            loadingBarFill.fillAmount = progressValue;

            yield return null;
        }
	}
}
