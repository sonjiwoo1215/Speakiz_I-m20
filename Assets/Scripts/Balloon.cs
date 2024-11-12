using UnityEngine;
using UnityEngine.UI;

public class Balloon : MonoBehaviour
{
    public Text balloonCountText;  // ǳ�� ������ ǥ���� UI �ؽ�Ʈ

    void OnEnable()
    {
        // TrainingFetcher���� �����͸� ������ ǳ�� ������ ������Ʈ
        int balloonCount = TrainingFetcher.instance?.GetBalloonCount() ?? 0;
        UpdateBalloonCount(balloonCount);
    }

    public void UpdateBalloonCount(int balloonCount)
    {
        balloonCountText.text = balloonCount.ToString();
    }
}
