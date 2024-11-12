using UnityEngine;
using UnityEngine.UI;

public class FluencyArrow : MonoBehaviour
{
    public FluencyUIController fluencyUIController;
    public Button nextButton;      // ���� ������ �̵��� ��ư
    public Button previousButton;  // ���� ������ �̵��� ��ư

    private int currentSceneIndex = 0;
    private int totalScenes = 3; // ���� �� ���� (0, 1, 2)

    void Start()
    {
        // ��ư Ŭ�� �� �޼��� ����
        nextButton.onClick.AddListener(NextScene);
        previousButton.onClick.AddListener(PreviousScene);

        UpdateArrowVisibility();
    }

    public void NextScene()
    {
        if (currentSceneIndex < totalScenes - 1)
        {
            currentSceneIndex++;
            fluencyUIController.SetSceneIndex(currentSceneIndex);
            UpdateArrowVisibility();
        }
    }

    public void PreviousScene()
    {
        if (currentSceneIndex > 0)
        {
            currentSceneIndex--;
            fluencyUIController.SetSceneIndex(currentSceneIndex);
            UpdateArrowVisibility();
        }
    }

    private void UpdateArrowVisibility()
    {
        previousButton.gameObject.SetActive(currentSceneIndex > 0);
        nextButton.gameObject.SetActive(currentSceneIndex < totalScenes - 1);
    }
}
