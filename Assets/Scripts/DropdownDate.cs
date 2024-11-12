/*using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class DateDropdown : MonoBehaviour
{
    public Dropdown dateDropdown;  // ��Ӵٿ� UI
    public Text selectedDateText;  // ���õ� ��¥�� ǥ���� �ؽ�Ʈ
    public string trainingType;    // �Ʒ� ���� (ȣ��, ����, ��â��)

    void Start()
    {
        // ��Ӵٿ� �ʱ�ȭ
        dateDropdown.ClearOptions();

        // �ֱ� ������ġ ��¥�� ����Ʈ�� ����
        List<string> dates = new List<string>();
        for (int i = 0; i < 7; i++)
        {
            DateTime day = DateTime.Now.AddDays(-i);
            dates.Add(day.ToString("M�� dd��"));
        }

        // ��Ӵٿ ��¥ �߰�
        dateDropdown.AddOptions(dates);

        // ��Ӵٿ��� ���� ����Ǿ��� �� ȣ��Ǵ� ������ �߰�
        dateDropdown.onValueChanged.AddListener(delegate { OnDateSelected(); });

        // �⺻ ���õ� ��¥�� �ؽ�Ʈ�� ����
        OnDateSelected();
    }

    // ��Ӵٿ�� ��¥�� �����ϸ� ȣ��Ǵ� �Լ�
    public void OnDateSelected()
    {
        string selectedDate = dateDropdown.options[dateDropdown.value].text; // ������ ��¥ ��������
        selectedDateText.text = selectedDate; // ������ ��¥�� �ؽ�Ʈ�� ǥ��

        // ���õ� ��¥�� �´� �����͸� ������ (�Ʒ� ������ ����)
        FetchTrainingDataForSelectedDate();
    }

    // ������ ��¥�� �´� �Ʒ� �����͸� �������� �Լ�
    private void FetchTrainingDataForSelectedDate()
    {
        // TrainingDataFetcher�� �̿��Ͽ� �Ʒ� ������ ��¥�� �´� �����͸� ������
        TrainingDataFetcher.instance.FetchTrainingData(trainingType, dateDropdown.options[dateDropdown.value].text);
    }
}
*/