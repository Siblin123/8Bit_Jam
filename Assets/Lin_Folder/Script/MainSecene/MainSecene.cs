using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSecene : MonoBehaviour
{
    [SerializeField] string sceneName;
    [SerializeField] string stage1Name;
    [SerializeField] string stage2Name;
    [SerializeField] string stage3Name;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoadNextScene_Btn(string s)
    {
        // "GameScene"은 이동하고 싶은 씬의 이름입니다.
        // 유니티 에디터에서 해당 씬의 이름을 정확히 확인하고 바꿔주세요.
        SceneManager.LoadScene(s);
    }

    public void QuitGame_Btn()
    {
        // 유니티 에디터에서 실행 중일 때
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
                // 빌드된 게임일 때 (PC, 모바일 등)
        #else
                    Application.Quit();
        #endif
    }
}
