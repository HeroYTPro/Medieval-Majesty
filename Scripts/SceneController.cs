using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Cinemachine.DocumentationSortingAttribute;

public class SceneController : MonoBehaviour
{
    public static SceneController instance;
    [SerializeField] Animator transitionAnim;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void SelectedLevel(int levelId)
    {
        StartCoroutine(LoadSelectedLevel(levelId));
    }

    IEnumerator LoadSelectedLevel(int levelId)
    {
        transitionAnim.SetTrigger("End");
        yield return new WaitForSeconds(1);
        // Загружаем уровень по его номеру
        SceneManager.LoadScene("Level " + levelId);
        transitionAnim.SetTrigger("Start");
    }

    public void NextLevel()
    {
        StartCoroutine(LoadLevel());
        //SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
    IEnumerator LoadLevel()
    {
        transitionAnim.SetTrigger("End");
        yield return new WaitForSeconds(1);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        transitionAnim.SetTrigger("Start");
    }


    public void MainMenu()
    {
        StartCoroutine(LoadMainMenu());
    }
    IEnumerator LoadMainMenu()
    {
        transitionAnim.SetTrigger("End");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("MainMenu");
        transitionAnim.SetTrigger("Start");
    }

    public void GameOver()
    {
        StartCoroutine(LoadGameOver());
    }
    IEnumerator LoadGameOver()
    {
        transitionAnim.SetTrigger("End");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("GameOver");
        transitionAnim.SetTrigger("Start");
    }
    public void LastLevel()
    {
        StartCoroutine(LoadLastLevel());
    }
    IEnumerator LoadLastLevel()
    {
        transitionAnim.SetTrigger("End");
        yield return new WaitForSeconds(1);
        string lastLevel = PlayerPrefs.GetString("LastLevel");
        SceneManager.LoadScene(lastLevel);
        transitionAnim.SetTrigger("Start");
    }
    public void GameWin()
    {
        StartCoroutine(LoadGameWin());
    }
    IEnumerator LoadGameWin()
    {
        transitionAnim.SetTrigger("End");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("GameWin");
        transitionAnim.SetTrigger("Start");
    }




    // Возможно уже не нужно
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);
    }
}
