using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class LoginManager : MonoBehaviour
{
    public InputField loginIdInputField;   // 'user_login_id' �Է� �ʵ�
    public InputField passwordInputField;  // 'user_password' �Է� �ʵ�
    public Button loginButton;

    private void Start()
    {
        loginButton.onClick.AddListener(OnLoginButtonClick);
    }

    private void OnLoginButtonClick()
    {
        string loginId = loginIdInputField.text;
        string password = passwordInputField.text;
        StartCoroutine(Login(loginId, password));
    }

    private IEnumerator Login(string loginId, string password) // 'loginId'�� ������ ����
    {
        WWWForm form = new WWWForm();
        form.AddField("user_login_id", loginId);  // 'user_login_id'�� ����
        form.AddField("user_password", password);  // 'user_password'�� ����

        UnityWebRequest www = UnityWebRequest.Post("http://localhost:8080/login", form);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("�α��� ����: " + www.error);
        }
        else
        {
            Debug.Log("�α��� ����");
        }
    }
}
