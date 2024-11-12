using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CircleTimer : MonoBehaviour
{
    public Image timerImage; // Fill ����� Image ������Ʈ ����
    public float duration = 3f; // Ÿ�̸� ���� �ð� (��)
    private float timeElapsed;
    private Coroutine timerCoroutine; // Ÿ�̸� �ڷ�ƾ�� �����ϱ� ���� ����
    private bool isSoundDetected = false; // "��" �Ҹ� ���� ����

    void Start()
    {
        timerImage.fillAmount = 0; // �ʱ�ȭ
    }

    public void StartTimer()
    {
        StopTimer(); // ������ ���� ���� Ÿ�̸Ӱ� ������ �ߴ�
        timeElapsed = 0; // �ð� �ʱ�ȭ
        timerCoroutine = StartCoroutine(UpdateTimer());
    }

    public void StopTimer()
    {
        if (timerCoroutine != null)
        {
            StopCoroutine(timerCoroutine); // ���� ���� �ڷ�ƾ�� ����
            timerCoroutine = null; // ���� �ʱ�ȭ
        }
        timerImage.fillAmount = 0; // �̹��� �ʷ� ����
        timeElapsed = 0; // �ð� ����
    }

    // �Ҹ� ���� ���¸� �����ϴ� �޼ҵ�
    public void SetSoundDetection(bool detected)
    {
        isSoundDetected = detected;
    }

    IEnumerator UpdateTimer()
    {
        while (timeElapsed < duration)
        {
            if (isSoundDetected) // "��" �Ҹ��� �����Ǹ�
            {
                timeElapsed += Time.deltaTime; // �ð� ����
                timerImage.fillAmount = Mathf.Clamp01(timeElapsed / duration); // ���� ��Ȳ�� ���� fillAmount ����
            }
            else
            {
                // �Ҹ� ������ �ߴܵǸ� Ÿ�̸� �ߴ�
                StopTimer();
                break;
            }
            yield return null;
        }

        if (timeElapsed >= duration)
        {
            timerImage.fillAmount = 1; // �Ϸ� ��, �� ������ ä���
        }
    }
}
