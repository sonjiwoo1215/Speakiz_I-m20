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

    private int currentWordIndex = 0; // 현재 표시 중인 단어의 인덱스
    private TrainingFetcher trainingFetcher; // TrainingFetcher 인스턴스 참조

    void Start()
    {
        trainingFetcher = TrainingFetcher.instance;

        // 데이터를 제대로 가져왔는지 확인
        if (trainingFetcher != null)
        {
            UpdateWordUI(); // 첫 번째 단어로 UI 업데이트
        }
        else
        {
            Debug.LogError("TrainingFetcher instance is null.");
        }

        // 버튼 클릭 이벤트 추가
        nextButton.onClick.AddListener(GoToNextWord);
        previousButton.onClick.AddListener(GoToPreviousWord);
    }

    // UI를 현재 단어 인덱스에 따라 업데이트
    private void UpdateWordUI()
    {
        if (trainingFetcher != null && currentWordIndex < trainingFetcher.GetWords().Length)
        {
            wordText.text = trainingFetcher.GetWords()[currentWordIndex];
            childWordText.text = trainingFetcher.GetChildWords()[currentWordIndex];
            accuracyText.text = $"{trainingFetcher.GetScores()[currentWordIndex] * 100:F1}%";
            feedbackText.text = trainingFetcher.GetFeedback()[currentWordIndex];

            // 화살표 버튼 활성화/비활성화 처리
            previousButton.gameObject.SetActive(currentWordIndex > 0);
            nextButton.gameObject.SetActive(currentWordIndex < trainingFetcher.GetWords().Length - 1);
        }
    }

    // 다음 단어로 이동
    public void GoToNextWord()
    {
        if (currentWordIndex < trainingFetcher.GetWords().Length - 1)
        {
            currentWordIndex++;
            UpdateWordUI();
        }
    }

    // 이전 단어로 이동
    public void GoToPreviousWord()
    {
        if (currentWordIndex > 0)
        {
            currentWordIndex--;
            UpdateWordUI();
        }
    }
}
