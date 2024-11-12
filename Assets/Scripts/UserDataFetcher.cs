using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

[System.Serializable]
public class UserData
{
    public string user_name; // �������� ���� ����� �̸� �ʵ�
}

public class UserDataFetcher : MonoBehaviour
{
    public static UserDataFetcher instance;
    public string user_name;

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
        // �� ���� �� ����� �����͸� �������� ������
        StartCoroutine(FetchUserData());
    }

    IEnumerator FetchUserData()
    {
        UnityWebRequest www = UnityWebRequest.Get("http://localhost:8080/users"); // ���� URL
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.ConnectionError && www.result != UnityWebRequest.Result.ProtocolError)
        {
            string jsonResult = www.downloadHandler.text;
            UserData userData = JsonUtility.FromJson<UserData>(jsonResult); // JSON �����͸� �Ľ��Ͽ� UserData ��ü�� ��ȯ

            if (userData != null)
            {
                user_name = userData.user_name; // �Ľ̵� user_name �� ����
                Debug.Log("UserData fetched: " + user_name);
            }
            else
            {
                Debug.LogError("Failed to parse user data");
            }
        }
        else
        {
            Debug.LogError("Failed to fetch user data: " + www.error);
        }
    }

    public string GetUserName()
    {
        return user_name;
    }
}
