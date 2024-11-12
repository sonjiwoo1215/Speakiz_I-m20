using UnityEngine;
using UnityEngine.UI;

public class VolumeGraph : MonoBehaviour
{
    public LineRenderer lineRenderer;  // LineRenderer ������Ʈ
    public RectTransform graphArea;    // �׷����� �׷��� UI ����
    private float scaleMultiplier = 50f; // ���� ��ġ�� Ȯ���ϱ� ���� ���� ����

    void OnEnable()
    {
        UpdateVolumeGraph(); // �ǵ�� ȭ���� Ȱ��ȭ�� ������ �׷��� ������Ʈ
    }

    public void UpdateVolumeGraph()
    {
        // PlayerPrefs�κ��� �����͸� �������� ���� �α� �߰�
        float[] volumes = TrainingFetcher.instance?.GetVolumeLevels() ?? new float[6];
        Debug.Log("Volume levels loaded: " + string.Join(", ", volumes)); // ������ Ȯ�ο� �α�

        // ���� �����Ͱ� ���ų� ��ȿ���� ���� ��� �⺻�� ����
        if (volumes != null && volumes.Length > 0)
        {
            DrawGraph(volumes); // LineRenderer�� ��ǥ ����
        }
        else
        {
            Debug.LogError("Volume data not found or empty.");
        }
    }

    private void DrawGraph(float[] volumes)
    {
        int pointCount = volumes.Length;
        Vector3[] points = new Vector3[pointCount];

        // RectTransform�� ũ�� ��������
        Vector2 sizeDelta = graphArea.sizeDelta;

        // �׷����� �׸� �� ���� ũ�� ���� ����
        float xScaleFactor = sizeDelta.x / (pointCount - 1); // x���� ����Ʈ ���� ����
        float yScaleFactor = sizeDelta.y * scaleMultiplier;  // y���� ���� ũ�⸦ ������ ���� Ȯ��

        for (int i = 0; i < pointCount; i++)
        {
            // x ��ġ�� ����Ʈ�� ����, y ��ġ�� ���� ���� ���
            float xPosition = i * xScaleFactor;
            float yPosition = Mathf.Clamp(volumes[i] * yScaleFactor, 0, sizeDelta.y);

            // RectTransform ������ ��ǥ�� ���� ��ǥ�� ��ȯ
            Vector2 localPoint = new Vector2(xPosition, yPosition);
            Vector3 worldPoint = graphArea.TransformPoint(localPoint);

            points[i] = worldPoint;
        }

        // LineRenderer�� ������ ��ǥ �ݿ�
        lineRenderer.positionCount = points.Length;
        lineRenderer.SetPositions(points);

        // LineRenderer�� Sorting Layer ���� (UI ���̾� ���� ���̵���)
        lineRenderer.sortingLayerName = "UI";
        lineRenderer.sortingOrder = 5;
    }
}
