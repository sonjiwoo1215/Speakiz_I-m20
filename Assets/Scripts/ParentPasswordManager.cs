using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Networking;

public class ParentPasswordManager : MonoBehaviour
{
    public InputField passwordInputField; // ��й�ȣ �Է� �ʵ�
    public Button submitButton; // ���� ��ư
    public GameObject feedbackCanvas; // �ǵ�� ĵ����
    public GameObject currentCanvas; // ���� ĵ����
    private string serverUrl = "http://localhost:8080/userParentPassword"; // ���� ��������Ʈ URL

    void Start()
    {
        submitButton.onClick.AddListener(OnSubmitButtonClicked);
    }

    void OnSubmitButtonClicked()
    {
        string enteredPassword = passwordInputField.text;

        // ��й�ȣ ���� ���� ������ ĵ���� ��ȯ
        feedbackCanvas.SetActive(true);
        currentCanvas.SetActive(false);

        // ��й�ȣ�� ������ ������ �κ� (�ּ� ���� ����)
        /*
        StartCoroutine(SendPasswordToServer(enteredPassword));
        */
    }

    // ��й�ȣ�� ������ �����ϴ� �ڷ�ƾ
    IEnumerator SendPasswordToServer(string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("user_parent_password", password); // ������ ������ �ʵ�� ����

        UnityWebRequest www = UnityWebRequest.Post(serverUrl, form);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Error: " + www.error);
        }
        else
        {
            string serverResponse = www.downloadHandler.text;
            if (serverResponse == "Success")
            {
                // ��й�ȣ�� ��ġ�ϸ� �ǵ�� ĵ������ ��ȯ
                feedbackCanvas.SetActive(true);
                currentCanvas.SetActive(false);
            }
            else
            {
                Debug.LogError("Invalid password");
                // ��й�ȣ�� ��ġ���� ������ ���� �޽��� ǥ�� (�߰� ����)
            }
        }
    }
}
