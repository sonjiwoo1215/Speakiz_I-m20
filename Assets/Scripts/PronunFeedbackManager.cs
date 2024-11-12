using UnityEngine;
using UnityEngine.UI;

public class PronunFeedbackManager : MonoBehaviour
{
    public Text wordText;
    public Text childWordText;
    public Text accuracyText;
    public Text feedbackText;
    public Button nextButton;
    public Button previousButton;

    private int currentWordIndex = 0; // ���� ǥ�� ���� �ܾ��� �ε���
    private TrainingFetcher trainingFetcher; // TrainingFetcher �ν��Ͻ� ����

    void Start()
    {
        trainingFetcher = TrainingFetcher.instance;

        // �����͸� ����� �����Դ��� Ȯ��
        if (trainingFetcher != null)
        {
            UpdateWordUI(); // ù ��° �ܾ�� UI ������Ʈ
        }
        else
        {
            Debug.LogError("TrainingFetcher instance is null.");
        }

        // ��ư Ŭ�� �̺�Ʈ �߰�
        nextButton.onClick.AddListener(GoToNextWord);
        previousButton.onClick.AddListener(GoToPreviousWord);
    }

    // UI�� ���� �ܾ� �ε����� ���� ������Ʈ
    private void UpdateWordUI()
    {
        if (trainingFetcher != null && currentWordIndex < trainingFetcher.GetWords().Length)
        {
            wordText.text = trainingFetcher.GetWords()[currentWordIndex];
            childWordText.text = trainingFetcher.GetChildWords()[currentWordIndex];
            accuracyText.text = $"{trainingFetcher.GetScores()[currentWordIndex] * 100:F1}%";
            feedbackText.text = trainingFetcher.GetFeedback()[currentWordIndex];

            // ȭ��ǥ ��ư Ȱ��ȭ/��Ȱ��ȭ ó��
            previousButton.gameObject.SetActive(currentWordIndex > 0);
            nextButton.gameObject.SetActive(currentWordIndex < trainingFetcher.GetWords().Length - 1);
        }
    }

    // ���� �ܾ�� �̵�
    public void GoToNextWord()
    {
        if (currentWordIndex < trainingFetcher.GetWords().Length - 1)
        {
            currentWordIndex++;
            UpdateWordUI();
        }
    }

    // ���� �ܾ�� �̵�
    public void GoToPreviousWord()
    {
        if (currentWordIndex > 0)
        {
            currentWordIndex--;
            UpdateWordUI();
        }
    }
}
