using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameJudge : MonoBehaviour
{
    public bool isWin;
    public Dictionary<string, int> scenes = new Dictionary<string, int>()
    {
        { "MainScene", 0 },
        { "Stage1", 1 },
        { "Stage2", 2 },
        { "Stage3", 3 }
    };

    private void OnEnable()
    {
        if (isWin && SceneManager.GetActiveScene().name != "Stage3")
        {
            StartCoroutine(MoveToScene(++scenes[SceneManager.GetActiveScene().name]));
        }
        else
        {
            StartCoroutine(MoveToScene(scenes["MainScene"]));
        }
    }

    private IEnumerator MoveToScene(string s)
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(s);
        yield return null;
    }
    private IEnumerator MoveToScene(int i)
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(i);
        yield return null;
    }
}