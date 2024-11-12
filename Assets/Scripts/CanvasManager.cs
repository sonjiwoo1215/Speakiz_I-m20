using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager instance;

    // ��� ĵ������ ����
    public Canvas startCanvas;
    public Canvas loginCanvas;
    public Canvas signUpCanvas;
    public Canvas sufinishCanvas;
    public Canvas test1Canvas;
    public Canvas test2Canvas;
    public Canvas test3Canvas;
    public Canvas test4Canvas;
    public Canvas test5Canvas;
    public Canvas levelCanvas;
    public Canvas placeCanvas;
    public Canvas training1Canvas;
    public Canvas training2Canvas;
    public Canvas feedBackCanvas;
    public Canvas BreathFB;
    public Canvas PronunciationFB;
    public Canvas FluencyFB;
    public Canvas parentPasswordCanvas;
    public Canvas informationCanvas;
    public Canvas BT_Canvas;

    public string BT = "BT_Scene";
    public string PT = "PT_Scene";
    public string FT = "FT_Scene";

    private Canvas currentCanvas;
    private Stack<Canvas> canvasStack = new Stack<Canvas>(); // �ڷΰ��� ���� �߰�
    private bool returnToTrainingCanvas = false; // �Ʒ� ĵ������ ���ư��� �÷���

    // �̱��� ���� ���� �� �ʱ�ȭ
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            instance = this;
            InitializeFirstCanvas(); // ó�� ĵ���� ����
            SceneManager.sceneLoaded += OnSceneLoaded; // ���� �ε�� �� �̺�Ʈ ����
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeFirstCanvas()
    {
        currentCanvas = startCanvas; // ó�� Ȱ��ȭ�� ĵ���� ����
        currentCanvas.gameObject.SetActive(true);
    }

    // ������ ĵ���� ��ȯ �޼��忡 ���� �߰�
    private void SwitchCanvas(Canvas currentCanvas, Canvas newCanvas)
    {
        if (currentCanvas != null)
        {
            currentCanvas.gameObject.SetActive(false);
            canvasStack.Push(currentCanvas); // ���� ĵ������ ���ÿ� �߰��Ͽ� ����
        }

        if (newCanvas != null)
        {
            newCanvas.gameObject.SetActive(true);
            AlignCanvasTransform(newCanvas, currentCanvas);
            this.currentCanvas = newCanvas; // ���� ĵ���� ������Ʈ
        }
    }

    // �ڷΰ��⸦ ���� �޼��� �߰�
    public void GoBack()
    {
        if (canvasStack.Count > 0)
        {
            Canvas previousCanvas = canvasStack.Pop(); // ���ÿ��� ���� �ֱ� ĵ���� ����
            SwitchCanvas(currentCanvas, previousCanvas); // ���ÿ� �߰����� �ʰ� ��ȯ
        }
    }

    // ĵ���� ��ȯ �� ��ġ, ȸ��, ũ�� ����ȭ
    private void AlignCanvasTransform(Canvas newCanvas, Canvas oldCanvas)
    {
        if (oldCanvas != null)
        {
            newCanvas.transform.position = oldCanvas.transform.position;
            newCanvas.transform.rotation = oldCanvas.transform.rotation;
            newCanvas.transform.localScale = oldCanvas.transform.localScale;
        }
    }

    // �α��� -> Training1���� ��ȯ
    public void SwitchToTraining1()
    {
        SwitchCanvas(currentCanvas, training1Canvas);
    }

    // �α��� -> SignUp���� ��ȯ
    public void SwitchToSignUp()
    {
        SwitchCanvas(currentCanvas, signUpCanvas);
    }

    // Start -> Login Canvas ��ȯ
    public void SwitchToLogin()
    {
        SwitchCanvas(currentCanvas, loginCanvas);
    }

    // sufinish -> Test1 Canvas ��ȯ
    public void SwitchToTest1()
    {
        SwitchCanvas(currentCanvas, test1Canvas);
    }

    // Test1 -> Test2 Canvas ��ȯ
    public void SwitchToTest2()
    {
        SwitchCanvas(currentCanvas, test2Canvas);
    }

    // Test2 -> Test3 Canvas ��ȯ
    public void SwitchToTest3()
    {
        SwitchCanvas(currentCanvas, test3Canvas);
    }

    // Test3 -> Test4 Canvas ��ȯ
    public void SwitchToTest4()
    {
        SwitchCanvas(currentCanvas, test4Canvas);
    }

    // Test4 -> Test5 Canvas ��ȯ
    public void SwitchToTest5()
    {
        SwitchCanvas(currentCanvas, test5Canvas);
    }

    // Test5 -> Level Canvas ��ȯ
    public void SwitchToLevel()
    {
        SwitchCanvas(currentCanvas, levelCanvas);
    }

    // Place -> Training1 Canvas ��ȯ
    public void ShowTraining1Canvas()
    {
        SwitchCanvas(currentCanvas, training1Canvas);
    }

    // Place -> Training2 Canvas ��ȯ
    public void ShowTraining2Canvas()
    {
        SwitchCanvas(currentCanvas, training2Canvas);
    }

    // Feedback Canvas���� ��ǥ Canvas��� ��ȯ
    public void SwitchToBreathFB()
    {
        SwitchCanvas(feedBackCanvas, BreathFB);
    }

    public void SwitchToPronunciationFB()
    {
        SwitchCanvas(feedBackCanvas, PronunciationFB);
    }

    public void SwitchToFluencyFB()
    {
        SwitchCanvas(feedBackCanvas, FluencyFB);
    }

    // �кθ� ��й�ȣ ĵ���� -> �ǵ�� ĵ���� ��ȯ
    public void SwitchToFeedbackCanvas()
    {
        SwitchCanvas(currentCanvas, feedBackCanvas);
    }

    // SignUp -> sufinish ĵ���� ��ȯ
    public void SwitchToSufinish()
    {
        SwitchCanvas(currentCanvas, sufinishCanvas);
    }

    // Place Canvas�� ��ȯ
    public void SwitchToPlace()
    {
        SwitchCanvas(currentCanvas, placeCanvas);
    }

    // information Canvas�� ��ȯ
    public void SwitchToInformationCanvas()
    {
        SwitchCanvas(currentCanvas, informationCanvas);
    }

    // ���� ������ ��ȯ�ϰ� ���ƿ� �� Ư�� ĵ���� Ȱ��ȭ
    public void SwitchToGameScene1()
    {
       /* returnToTrainingCanvas = true; // ���ƿ� �� TrainingCanvas�� Ȱ��ȭ�ϵ��� ����
        SceneManager.LoadScene(BT);*/
        SwitchCanvas(currentCanvas, BT_Canvas);
    }
    public void SwitchToGameScene2()
    {
        returnToTrainingCanvas = true;
        SceneManager.LoadScene(PT);
    }
    public void SwitchToGameScene3()
    {
        returnToTrainingCanvas = true;
        SceneManager.LoadScene(FT);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Main" && returnToTrainingCanvas)
        {
            returnToTrainingCanvas = false;
            ShowTraining1Canvas();
        }
        else if (scene.name == "Main")
        {
            InitializeFirstCanvas();
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void ActivateTrainingCanvas()
    {
        if (training1Canvas != null)
        {
            SwitchCanvas(currentCanvas, training1Canvas);
        }
        else
        {
            Debug.LogError("TrainingCanvas�� �������� �ʾҽ��ϴ�.");
        }
    }
}
