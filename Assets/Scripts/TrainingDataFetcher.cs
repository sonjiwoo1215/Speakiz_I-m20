using System.Collections;
using UnityEngine;

public class TrainingFetcher : MonoBehaviour
{
    public static TrainingFetcher instance;

    // ȣ�� �Ʒ� ������
    public float length;                   // ȣ�� �Ʒ�: ��� ���ӽð�
    public float[] bt_levels = new float[6]; // ȣ�� �Ʒ�: ���� �� (6�� ����)
    public int bt_success_cnt;             // ȣ�� �Ʒ�: ǳ�� ����

    // ���� �Ʒ� ������
    public string[] pt_words = new string[3];           // ���� �Ʒ�: �Ʒ� �ܾ�
    public string[] pt_text = new string[3];            // ���� �Ʒ�: �Ƶ� ���� �ؽ�Ʈ
    public float[] pt_score = new float[3];             // ���� �Ʒ�: ��Ȯ�� ����
    public string[] pt_feedback = new string[3];        // ���� �Ʒ�: �ǵ��
    public AudioClip[] pt_teacher_voice = new AudioClip[3];  // ���� �Ʒ�: ������ �Ҹ�
    public AudioClip[] pt_child_voice = new AudioClip[3];    // ���� �Ʒ�: �Ƶ� �Ҹ�

    // ��â�� �Ʒ� ������
    public string[][] ft_texts = new string[3][];      // ��â�� �Ʒ�: �� ���� ��ȭ 4��
    public float[] ft_score_1 = new float[3];          // ��â�� ����
    public float[] ft_score_2 = new float[3];          // ���� ����
    public float[] ft_score_3 = new float[3];          // ��� ����

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
        LoadFluencyTrainingData(); // ��â�� �Ʒ� ������ �ε�
    }

    // ȣ�� �Ʒ� ������ �ε�
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

    // ���� �Ʒ� ������ �ε�
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

    // ��â�� �Ʒ� ������ �ε�
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

    // Getters for ȣ�� �Ʒ� ������
    public float GetAverageDuration() => length;
    public float[] GetVolumeLevels() => bt_levels;
    public int GetBalloonCount() => bt_success_cnt;

    // Getters for ���� �Ʒ� ������
    public string[] GetWords() => pt_words;
    public string[] GetChildWords() => pt_text;
    public float[] GetScores() => pt_score;
    public string[] GetFeedback() => pt_feedback;
    public AudioClip[] GetTeacherVoices() => pt_teacher_voice;
    public AudioClip[] GetChildVoices() => pt_child_voice;

    // Getters for ��â�� �Ʒ� ������
    public string[][] GetFluencyTexts() => ft_texts;
    public float[] GetFluencyScores() => ft_score_1;
    public float[] GetPronunciationScores() => ft_score_2;
    public float[] GetIntonationScores() => ft_score_3;
}
