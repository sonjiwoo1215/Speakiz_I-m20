using System.Collections;
using UnityEngine;
using UnityEngine.UI; // UI ���� ���ӽ����̽� �߰�

public class Duration : MonoBehaviour
{
    public Image durationBar;       // UI�� ���� �׷����� ��Ÿ�� Image
    public Text durationText;       // UI�� ǥ���� ��� ���ӽð� �ؽ�Ʈ

    void OnEnable()
    {
        UpdateGraph(); // �ǵ�� ȭ���� Ȱ��ȭ�� ������ �׷��� ������Ʈ
    }

    public void UpdateGraph()
    {
        // TrainingFetcher���� ��� ���ӽð� ��������
        float averageDuration = TrainingFetcher.instance?.GetAverageDuration() ?? 0;

        if (averageDuration > 0)
        {
            durationBar.fillAmount = Mathf.Clamp01(averageDuration / 3f); // �ִ� 3�ʷ� ����ȭ
            durationText.text = averageDuration.ToString("F1") + "��"; // �Ҽ��� 1�ڸ����� ǥ��
        }
        else
        {
            durationBar.fillAmount = 0f;
            durationText.text = "������ ����";
            Debug.LogWarning("��� ���ӽð� �����Ͱ� ��ȿ���� �ʽ��ϴ�.");
        }
    }
}
