using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BackgroundMusicManager : MonoBehaviour
{
    public static BackgroundMusicManager instance;
    public AudioSource backgroundMusic;
    public Toggle musicToggle; // 토글 버튼

    private string[] gameScenes = { "BT_Scene", "PT_Scene", "FT_Scene" }; // 게임 씬들

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // 다른 씬으로 전환해도 이 오브젝트가 유지되게 함
        }
        else
        {
            Destroy(gameObject); // 중복된 오브젝트가 생성되는 것을 방지
        }

        SceneManager.sceneLoaded += OnSceneLoaded; // 씬이 로드될 때마다 호출되는 메서드 등록
    }

    void Start()
    {
        // 앱이 실행되면 자동으로 배경음악 재생
        if (!backgroundMusic.isPlaying)
        {
            backgroundMusic.Play();
        }

        // 토글 상태에 따라 음악 재생/중지 설정
        if (musicToggle != null)
        {
            musicToggle.isOn = backgroundMusic.isPlaying;
            musicToggle.onValueChanged.AddListener(ToggleMusic);
        }
    }

    // 씬이 로드될 때 호출되는 메서드
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 현재 씬이 게임 씬이라면 배경음악 중지
        if (IsGameScene(scene.name))
        {
            if (backgroundMusic.isPlaying)
            {
                backgroundMusic.Pause();
            }
        }
        else // UI 씬이라면 배경음악 재생
        {
            if (!backgroundMusic.isPlaying)
            {
                backgroundMusic.UnPause();
            }
        }
    }

    // 토글 버튼을 통해 배경음악을 재생하거나 중지하는 함수
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

    // 게임 씬인지 확인하는 함수
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
