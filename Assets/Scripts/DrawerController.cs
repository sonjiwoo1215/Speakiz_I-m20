using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DrawerController : MonoBehaviour
{
    public GameObject drawerPanel;
    public Button closeAreaButton;
    public CanvasGroup mainPanelCanvasGroup;
    public GameObject targetCanvas; // ��ȯ�� ��ǥ ĵ����
    public GameObject currentCanvas; // ���� Ȱ��ȭ�� ĵ����
    public Text user_nameText; // ��ξ� �гο� ǥ�õ� ����� �̸� �ؽ�Ʈ
    private bool isDrawerOpen = false;

    // ����� �̸��� �����ϴ� ���� (�������� ������ �̸�)
    private string user_name = "����� �̸�"; // ���⿡ ���� ����� �̸��� �������� ������ ����

    void Start()
    {
        // �ʼ� ������Ʈ���� �����Ǿ����� Ȯ��
        if (drawerPanel == null)
        {
            Debug.LogError("DrawerPanel�� �������� �ʾҽ��ϴ�.");
        }

        if (mainPanelCanvasGroup == null)
        {
            Debug.LogError("MainPanel�� CanvasGroup�� �������� �ʾҽ��ϴ�.");
        }

        if (closeAreaButton == null)
        {
            Debug.LogError("CloseAreaButton�� �������� �ʾҽ��ϴ�.");
        }

        if (user_nameText == null)
        {
            Debug.LogError("����� �̸��� ǥ���� Text ������Ʈ�� �������� �ʾҽ��ϴ�.");
        }

        // ��ξ� �ܺθ� Ŭ���� �� ������ ���� �� ��Ȱ��ȭ
        closeAreaButton.gameObject.SetActive(false);

        // CloseDrawer �޼��带 closeAreaButton�� ����
        closeAreaButton.onClick.AddListener(CloseDrawer);

        // ����� �̸��� ��ξ� �гο� ǥ��
        Updateuser_nameUI();
    }

    // ����� �̸��� ��ξ� �гο� ������Ʈ�ϴ� �޼���
    private void Updateuser_nameUI()
    {
        if (user_nameText != null)
        {
            user_nameText.text = user_name;
        }
    }

    public void ToggleDrawer()
    {
        // ��ξ��� ����/���� ���¸� ���
        isDrawerOpen = !isDrawerOpen;

        // ��ξ� �гΰ� ��ξ� �ܺ� ��ư�� Ȱ��ȭ ���¸� ����
        drawerPanel.SetActive(isDrawerOpen);
        closeAreaButton.gameObject.SetActive(isDrawerOpen);

        // ���� �г��� ��ȣ�ۿ� ���� ���θ� ����
        SetMainPanelInteractable(!isDrawerOpen);
    }

    public void CloseDrawer()
    {
        // ��ξ �ݴ� �޼���
        isDrawerOpen = false;
        drawerPanel.SetActive(false);
        closeAreaButton.gameObject.SetActive(false);
        SetMainPanelInteractable(true);
    }

    private void SetMainPanelInteractable(bool interactable)
    {
        // ���� �г��� ��ȣ�ۿ� ���� ���ο� Raycast�� Ȱ��ȭ�� ����
        if (mainPanelCanvasGroup != null)
        {
            mainPanelCanvasGroup.interactable = interactable;
            mainPanelCanvasGroup.blocksRaycasts = interactable;
        }
    }

    public void SwitchCanvas()
    {
        // ��ǥ ĵ������ �����Ǿ� �ִ��� Ȯ��
        if (targetCanvas != null)
        {
            // ���� ĵ������ ��Ȱ��ȭ�ϰ� ��ǥ ĵ������ Ȱ��ȭ
            if (currentCanvas != null)
            {
                currentCanvas.SetActive(false);
            }

            targetCanvas.SetActive(true);

            // ĵ���� ��ȯ �� ��ξ ����
            CloseDrawer();

            // ��ǥ ĵ���� ���� ù ��° InputField�� ��Ŀ���� ����
            var inputField = targetCanvas.GetComponentInChildren<InputField>();
            if (inputField != null)
            {
                inputField.Select();
                inputField.ActivateInputField();
            }
        }
        else
        {
            Debug.LogError("TargetCanvas�� �������� �ʾҽ��ϴ�.");
        }
    }
}
