using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BackgroundMusicManager : MonoBehaviour
{
    public static BackgroundMusicManager instance;
    public AudioSource backgroundMusic;
    public Toggle musicToggle; // ��� ��ư

    private string[] gameScenes = { "BT_Scene", "PT_Scene", "FT_Scene" }; // ���� ����

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // �ٸ� ������ ��ȯ�ص� �� ������Ʈ�� �����ǰ� ��
        }
        else
        {
            Destroy(gameObject); // �ߺ��� ������Ʈ�� �����Ǵ� ���� ����
        }

        SceneManager.sceneLoaded += OnSceneLoaded; // ���� �ε�� ������ ȣ��Ǵ� �޼��� ���
    }

    void Start()
    {
        // ���� ����Ǹ� �ڵ����� ������� ���
        if (!backgroundMusic.isPlaying)
        {
            backgroundMusic.Play();
        }

        // ��� ���¿� ���� ���� ���/���� ����
        if (musicToggle != null)
        {
            musicToggle.isOn = backgroundMusic.isPlaying;
            musicToggle.onValueChanged.AddListener(ToggleMusic);
        }
    }

    // ���� �ε�� �� ȣ��Ǵ� �޼���
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // ���� ���� ���� ���̶�� ������� ����
        if (IsGameScene(scene.name))
        {
            if (backgroundMusic.isPlaying)
            {
                backgroundMusic.Pause();
            }
        }
        else // UI ���̶�� ������� ���
        {
            if (!backgroundMusic.isPlaying)
            {
                backgroundMusic.UnPause();
            }
        }
    }

    // ��� ��ư�� ���� ��������� ����ϰų� �����ϴ� �Լ�
    public void ToggleMusic(bool isOn)
    {
        if (isOn)
        {
            backgroundMusic.Play();
        }
        else
        {
            backgroundMusic.Pause();
        }
    }

    // ���� ������ Ȯ���ϴ� �Լ�
    private bool IsGameScene(string sceneName)
    {
        foreach (string gameScene in gameScenes)
        {
            if (sceneName == gameScene)
            {
                return true;
            }
        }
        return false;
    }
}
