using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // �� ���� ���ӽ����̽� �߰�

public class GameController : MonoBehaviour
{
    public GameObject gameSuccessUI; // ���� ���� UI ����
    public GameObject gameFailUI; // ���� ���� UI ����
    public Mic micScript; // Mic ��ũ��Ʈ ����
    public Text attemptsText; // ���� ���� UI�� ǥ���� Attempts Text ����
    public Text failAttemptsText; // ���� ���� UI�� ǥ���� Attempts Text ����
    public Text successBalloonsText; // ������ ǳ�� ������ ǥ���� Text ����

    public GameObject image1; // ������ ǳ�� 1���� �� Ȱ��ȭ�� �̹���
    public GameObject image2; // ������ ǳ�� 2���� �� Ȱ��ȭ�� �̹���
    public GameObject image3; // ������ ǳ�� 3���� �� Ȱ��ȭ�� �̹���
    public GameObject image4; // ������ ǳ�� 4���� �� Ȱ��ȭ�� �̹���

    public GameObject FCanvas; // Fĵ���� ���� �߰�
    public GameObject currentCanvas; // ���� ĵ���� ���� �߰�

    void Start()
    {
        gameSuccessUI.SetActive(false); // ���� ���� �� ���� UI ����
        gameFailUI.SetActive(false); // ���� ���� �� ���� UI ����
        FCanvas.SetActive(false); // Fĵ���� ����

        // ��� �̹����� ��Ȱ��ȭ
        image1.SetActive(false);
        image2.SetActive(false);
        image3.SetActive(false);
        image4.SetActive(false);
    }

    void Update()
    {
        CheckEndGameCondition();
        UpdateSuccessImages(); // ������ ǳ�� ������ ���� �̹��� ������Ʈ
    }

    public void CheckEndGameCondition()
    {
        if (micScript.successfulAttempts >= 5)
        {
            gameSuccessUI.SetActive(true);
            attemptsText.text = micScript.totalAttempts.ToString() + " ȸ";
            micScript.enabled = false;
        }
        else if (micScript.totalAttempts >= 10)
        {
            gameFailUI.SetActive(true);
            failAttemptsText.text = micScript.totalAttempts.ToString() + " ȸ";
            successBalloonsText.text = "���� ǳ�� ���� : ";
            micScript.enabled = false;
        }
    }

    public void UpdateSuccessImages()
    {
        image1.SetActive(false);
        image2.SetActive(false);
        image3.SetActive(false);
        image4.SetActive(false);

        if (micScript.successfulAttempts == 1)
        {
            image1.SetActive(true);
        }
        else if (micScript.successfulAttempts == 2)
        {
            image2.SetActive(true);
        }
        else if (micScript.successfulAttempts == 3)
        {
            image3.SetActive(true);
        }
        else if (micScript.successfulAttempts == 4)
        {
            image4.SetActive(true);
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ExitGame()
    {
        Debug.Log("ExitGame ��ư�� ���Ƚ��ϴ�.");
        micScript.SaveFinalDataToPlayerPrefs(); // PlayerPrefs�� ������ ����

        // PlayerPrefs�� ����� �����ͷ� TrainingFetcher ������Ʈ
        TrainingFetcher.instance.LoadBreathTrainingData();
        TrainingFetcher.instance.LoadPronunciationTrainingData();

        // FCanvas Ȱ��ȭ
        FCanvas.SetActive(true);
        gameSuccessUI.SetActive(false);
        gameFailUI.SetActive(false);
        micScript.enabled = false;

        if (currentCanvas != null)
        {
            currentCanvas.SetActive(false);
        }
    }

}
