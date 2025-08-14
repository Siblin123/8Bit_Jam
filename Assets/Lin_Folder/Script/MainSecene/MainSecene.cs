using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSecene : MonoBehaviour
{
    [SerializeField] string sceneName;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoadNextScene_Btn()
    {
        // "GameScene"�� �̵��ϰ� ���� ���� �̸��Դϴ�.
        // ����Ƽ �����Ϳ��� �ش� ���� �̸��� ��Ȯ�� Ȯ���ϰ� �ٲ��ּ���.
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame_Btn()
    {
        // ����Ƽ �����Ϳ��� ���� ���� ��
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
                // ����� ������ �� (PC, ����� ��)
        #else
                    Application.Quit();
        #endif
    }
}
