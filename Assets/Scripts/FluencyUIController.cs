using UnityEngine;
using UnityEngine.UI;

public class FluencyUIController : MonoBehaviour
{
    public Text dialogueText;
    public Text fluencyScoreText;
    public Text pronunciationScoreText;
    public Text intonationScoreText;

    private int currentSceneIndex = 0;

    void OnEnable()
    {
        UpdateFluencyUI(currentSceneIndex);
    }

    public void UpdateFluencyUI(int sceneIndex)
    {
        // `TrainingFetcher`에서 대화 데이터를 가져오고 null인지 확인
        string[] dialogues = TrainingFetcher.instance.GetFluencyTexts()[sceneIndex];

        // `dialogues`가 null이 아니고 요소가 있는지 확인하여 텍스트 설정
        string fullDialogue = (dialogues != null && dialogues.Length > 0) ? string.Join("\n", dialogues) : "없음";
        dialogueText.text = fullDialogue;

        // 점수 데이터를 가져오고 기본값 설정
        float fluencyScore = TrainingFetcher.instance.ft_score_1[sceneIndex];
        float pronunciationScore = TrainingFetcher.instance.ft_score_2[sceneIndex];
        float intonationScore = TrainingFetcher.instance.ft_score_3[sceneIndex];

        fluencyScoreText.text = fluencyScore > 0 ? $"유창성 점수: {fluencyScore:F1}" : "없음";
        pronunciationScoreText.text = pronunciationScore > 0 ? $"발음 점수: {pronunciationScore:F1}" : "없음";
        intonationScoreText.text = intonationScore > 0 ? $"억양 점수: {intonationScore:F1}" : "없음";
    }


    public void SetSceneIndex(int newIndex)
    {
        currentSceneIndex = Mathf.Clamp(newIndex, 0, 2);
        UpdateFluencyUI(currentSceneIndex);
    }
}
