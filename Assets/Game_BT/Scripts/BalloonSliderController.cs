using UnityEngine;
using UnityEngine.UI; // UI ���ӽ����̽� ����

public class BalloonSliderController : MonoBehaviour
{
    public Slider balloonSlider; // �����̴� ������Ʈ ����
    public int totalBalloons = 5; // �� ǳ�� ����
    private int currentBalloons = 0; // ���� ǳ�� ����

    void Start()
    {
        balloonSlider.maxValue = totalBalloons; // �����̴��� �ִ� ���� �� ǳ�� ������ ����
        balloonSlider.value = currentBalloons; // �ʱ� �����̴� �� ����
    }

    public void AddBalloon()
    {
        if (currentBalloons < totalBalloons)
        {
            currentBalloons++; // ǳ�� ���� ����
            balloonSlider.value = currentBalloons; // �����̴� �� ������Ʈ
            Debug.Log("Current Balloons: " + currentBalloons);
            Debug.Log("Slider Value: " + balloonSlider.value);
        }
    }
}
