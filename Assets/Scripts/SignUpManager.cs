using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class SignUpManager : MonoBehaviour
{
    public InputField nameInputField;
    public InputField idInputField;
    public InputField passwordInputField;
    public InputField confirmPasswordInputField;
    public InputField emailInputField;
    public InputField emailVerificationInputField;
    public InputField parentPasswordInputField; // �кθ�� ��й�ȣ �Է� �ʵ�
    public Button signUpButton;
    public Text feedbackText;
    private string serverUrl = "http://localhost:8080/users/register";

    void Start()
    {
        signUpButton.onClick.AddListener(OnSignUpButtonClicked);
        confirmPasswordInputField.onValueChanged.AddListener(OnConfirmPasswordChanged);
    }

    void OnSignUpButtonClicked()
    {
        string name = nameInputField.text;
        string id = idInputField.text;
        string password = passwordInputField.text;
        string confirmPassword = confirmPasswordInputField.text;
        string email = emailInputField.text;
        string emailVerification = emailVerificationInputField.text;
        string parentPassword = parentPasswordInputField.text; // �кθ�� ��й�ȣ

        // ��й�ȣ Ȯ�� ����
        if (password != confirmPassword)
        {
            feedbackText.text = "��й�ȣ�� ��ġ���� �ʽ��ϴ�.";
            return;
        }

        StartCoroutine(SignUp(name, id, password, email, parentPassword));
    }

    void OnConfirmPasswordChanged(string confirmPassword)
    {
        string password = passwordInputField.text;

        if (password != confirmPassword)
        {
            feedbackText.text = "��й�ȣ�� ��ġ���� �ʽ��ϴ�.";
        }
        else
        {
            feedbackText.text = "";
        }
    }

    IEnumerator SignUp(string name, string id, string password, string email, string parentPassword)
    {
        User user = new User
        {
            user_name = name,
            user_login_id = id,
            user_password = password,
            user_email = email,
            user_level = 1, // �⺻�� ����
            user_parent_password = parentPassword // �кθ�� ��й�ȣ �߰�
        };

        string jsonData = JsonUtility.ToJson(user);

        UnityWebRequest request = new UnityWebRequest(serverUrl, "POST");
        byte[] bodyRaw = new System.Text.UTF8Encoding().GetBytes(jsonData);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            feedbackText.text = "����: " + request.error;
        }
        else
        {
            feedbackText.text = "����";
        }
    }

    [System.Serializable]
    public class User
    {
        public string user_name;            // ����� �̸�
        public string user_login_id;        // ����� �α��� ID
        public string user_password;        // ����� ��й�ȣ
        public string user_email;           // ����� �̸���
        public int user_level;              // ����� ���� (�⺻�� 1)
        public string user_parent_password; // �кθ�� ��й�ȣ
    }
}
