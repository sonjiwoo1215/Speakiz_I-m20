using System.Collections;
using UnityEngine;

public class TrainingFetcher : MonoBehaviour
{
    public static TrainingFetcher instance;

    // 호흡 훈련 데이터
    public float length;                   // 호흡 훈련: 평균 지속시간
    public float[] bt_levels = new float[6]; // 호흡 훈련: 볼륨 값 (6개 구간)
    public int bt_success_cnt;             // 호흡 훈련: 풍선 개수

    // 조음 훈련 데이터
    public string[] pt_words = new string[3];           // 조음 훈련: 훈련 단어
    public string[] pt_text = new string[3];            // 조음 훈련: 아동 발음 텍스트
    public float[] pt_score = new float[3];             // 조음 훈련: 정확도 점수
    public string[] pt_feedback = new string[3];        // 조음 훈련: 피드백
    public AudioClip[] pt_teacher_voice = new AudioClip[3];  // 조음 훈련: 선생님 소리
    public AudioClip[] pt_child_voice = new AudioClip[3];    // 조음 훈련: 아동 소리

    // 유창성 훈련 데이터
    public string[][] ft_texts = new string[3][];      // 유창성 훈련: 각 씬별 대화 4줄
    public float[] ft_score_1 = new float[3];          // 유창성 점수
    public float[] ft_score_2 = new float[3];          // 발음 점수
    public float[] ft_score_3 = new float[3];          // 억양 점수

    private string resourcesPath = "Game_PT/Resources/";

    void Awake()
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

    void Start()
    {
        LoadBreathTrainingData();
        LoadPronunciationTrainingData();
        LoadFluencyTrainingData(); // 유창성 훈련 데이터 로드
    }

    // 호흡 훈련 데이터 로드
    public void LoadBreathTrainingData()
    {
        length = PlayerPrefs.GetFloat("length", 0.0f);
        Debug.Log($"Loaded length: {length}");

        for (int i = 0; i < bt_levels.Length; i++)
        {
            bt_levels[i] = PlayerPrefs.GetFloat($"bt_level{i + 1}", 0.0f); // bt_level1 ~ bt_level6
            Debug.Log($"Loaded bt_level{i + 1}: {bt_levels[i]}");
        }

        bt_success_cnt = PlayerPrefs.GetInt("bt_success_cnt", 0);
        Debug.Log($"Loaded bt_success_cnt: {bt_success_cnt}");
    }

    // 조음 훈련 데이터 로드
    public void LoadPronunciationTrainingData()
    {
        pt_words = LoadStringArrayFromPrefs("pt_word", new string[] { "", "", "" });
        pt_text = LoadStringArrayFromPrefs("pt_text", new string[] { "", "", "" });

        float[] rawScores = LoadFloatArrayFromPrefs("pt_score", new float[] { 0.0f, 0.0f, 0.0f });
        for (int i = 0; i < pt_score.Length; i++)
        {
            pt_score[i] = Mathf.Round(rawScores[i] * 10f) / 10f;
        }

        pt_feedback = LoadStringArrayFromPrefs("pt_feedback", new string[] { "", "", "" });

        for (int i = 0; i < 3; i++)
        {
            pt_teacher_voice[i] = Resources.Load<AudioClip>($"{resourcesPath}TeacherVoice_{i}") ?? null;
            pt_child_voice[i] = Resources.Load<AudioClip>($"{resourcesPath}ChildVoice_{i}") ?? null;
        }
    }

    // 유창성 훈련 데이터 로드
    public void LoadFluencyTrainingData()
    {
        for (int i = 0; i < 3; i++)
        {
            ft_texts[i] = LoadStringArrayFromPrefs($"ft_texts_{i}", new string[4] { "", "", "", "" });
            ft_score_1[i] = PlayerPrefs.GetFloat($"ft_score_1_{i}", 0.0f);
            ft_score_2[i] = PlayerPrefs.GetFloat($"ft_score_2_{i}", 0.0f);
            ft_score_3[i] = PlayerPrefs.GetFloat($"ft_score_3_{i}", 0.0f);
        }
    }

    // Helper to load string array from PlayerPrefs
    private string[] LoadStringArrayFromPrefs(string key, string[] defaultValue)
    {
        string jsonData = PlayerPrefs.GetString(key, "");
        if (string.IsNullOrEmpty(jsonData)) return defaultValue;

        var wrapper = JsonUtility.FromJson<StringArrayWrapper>(jsonData);
        return wrapper?.items ?? defaultValue;
    }

    // Helper to load float array from PlayerPrefs
    private float[] LoadFloatArrayFromPrefs(string key, float[] defaultValue)
    {
        string jsonData = PlayerPrefs.GetString(key, "");
        if (string.IsNullOrEmpty(jsonData)) return defaultValue;

        var wrapper = JsonUtility.FromJson<FloatArrayWrapper>(jsonData);
        return wrapper?.items ?? defaultValue;
    }

    [System.Serializable]
    private class StringArrayWrapper { public string[] items; }

    [System.Serializable]
    private class FloatArrayWrapper { public float[] items; }

    // Getters for 호흡 훈련 데이터
    public float GetAverageDuration() => length;
    public float[] GetVolumeLevels() => bt_levels;
    public int GetBalloonCount() => bt_success_cnt;

    // Getters for 조음 훈련 데이터
    public string[] GetWords() => pt_words;
    public string[] GetChildWords() => pt_text;
    public float[] GetScores() => pt_score;
    public string[] GetFeedback() => pt_feedback;
    public AudioClip[] GetTeacherVoices() => pt_teacher_voice;
    public AudioClip[] GetChildVoices() => pt_child_voice;

    // Getters for 유창성 훈련 데이터
    public string[][] GetFluencyTexts() => ft_texts;
    public float[] GetFluencyScores() => ft_score_1;
    public float[] GetPronunciationScores() => ft_score_2;
    public float[] GetIntonationScores() => ft_score_3;
}
