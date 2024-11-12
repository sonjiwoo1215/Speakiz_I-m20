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
        // `TrainingFetcher`���� ��ȭ �����͸� �������� null���� Ȯ��
        string[] dialogues = TrainingFetcher.instance.GetFluencyTexts()[sceneIndex];

        // `dialogues`�� null�� �ƴϰ� ��Ұ� �ִ��� Ȯ���Ͽ� �ؽ�Ʈ ����
        string fullDialogue = (dialogues != null && dialogues.Length > 0) ? string.Join("\n", dialogues) : "����";
        dialogueText.text = fullDialogue;

        // ���� �����͸� �������� �⺻�� ����
        float fluencyScore = TrainingFetcher.instance.ft_score_1[sceneIndex];
        float pronunciationScore = TrainingFetcher.instance.ft_score_2[sceneIndex];
        float intonationScore = TrainingFetcher.instance.ft_score_3[sceneIndex];

        fluencyScoreText.text = fluencyScore > 0 ? $"��â�� ����: {fluencyScore:F1}" : "����";
        pronunciationScoreText.text = pronunciationScore > 0 ? $"���� ����: {pronunciationScore:F1}" : "����";
        intonationScoreText.text = intonationScore > 0 ? $"��� ����: {intonationScore:F1}" : "����";
    }


    public void SetSceneIndex(int newIndex)
    {
        currentSceneIndex = Mathf.Clamp(newIndex, 0, 2);
        UpdateFluencyUI(currentSceneIndex);
    }
}
