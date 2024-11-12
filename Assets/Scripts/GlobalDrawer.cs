using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GlobalDrawer : MonoBehaviour
{
    public static GlobalDrawer instance;

    public GameObject drawerPanel;
    public Button closeAreaButton;
    private GameObject currentCanvas;
    public Text user_nameText; // ����� �̸��� ǥ���� Text UI

    private bool isDrawerOpen = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (drawerPanel == null)
        {
            Debug.LogError("DrawerPanel�� �������� �ʾҽ��ϴ�.");
        }

        if (closeAreaButton == null)
        {
            Debug.LogError("CloseAreaButton�� �������� �ʾҽ��ϴ�.");
        }

        if (user_nameText == null)
        {
            Debug.LogError("����� �̸��� ǥ���� Text ������Ʈ�� �������� �ʾҽ��ϴ�.");
        }

        closeAreaButton.gameObject.SetActive(false);
        closeAreaButton.onClick.AddListener(CloseDrawer);

        // UserDataFetcher���� ����� �̸� �����ͼ� UI ������Ʈ
        UpdateUserNameUI();
    }

    private void UpdateUserNameUI()
    {
        if (UserDataFetcher.instance != null && user_nameText != null)
        {
            user_nameText.text = UserDataFetcher.instance.GetUserName();
        }
    }

    public void ToggleDrawer(GameObject callingCanvas)
    {
        currentCanvas = callingCanvas;
        isDrawerOpen = !isDrawerOpen;
        drawerPanel.SetActive(isDrawerOpen);
        closeAreaButton.gameObject.SetActive(isDrawerOpen);
    }

    public void CloseDrawer()
    {
        isDrawerOpen = false;
        drawerPanel.SetActive(false);
        closeAreaButton.gameObject.SetActive(false);
    }

    public void SwitchCanvas(GameObject targetCanvas)
    {
        if (targetCanvas != null)
        {
            targetCanvas.SetActive(true);
            if (currentCanvas != null)
            {
                currentCanvas.SetActive(false);
            }
            CloseDrawer();
        }
        else
        {
            Debug.LogError("TargetCanvas�� �������� �ʾҽ��ϴ�.");
        }
    }
}
