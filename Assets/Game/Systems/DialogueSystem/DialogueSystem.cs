using System.Collections;
using UnityEngine;
using TMPro;

public class DialogueSystem : MonoBehaviour {
    [SerializeField] private Sprite[] faces = new Sprite[6];
    [SerializeField] private GameObject[] imageBoxes = new GameObject[2];
    [SerializeField] private GameObject TextLabel;
    [SerializeField] private Color[] colors = new Color[2];
    [SerializeField] private GameObject clickPanel;
    [SerializeField] private float symbolDelay = 0.02f;

    private TMP_Text text;
    private int faceId = 0;

    public void SetFaceId(int id) {
        faceId = id;
    }

    public void PushText(string message) //0-2 - лица гг, 3-5 - лица кота
    {
        TextLabel.SetActive(true);
        clickPanel.SetActive(true);
        text = TextLabel.GetComponentInChildren<TMP_Text>();
        text.color = faceId < 3 ? colors[0] : colors[1];
        imageBoxes[0].SetActive(faceId < 3);
        imageBoxes[1].SetActive(faceId > 2);
        StopAllCoroutines();
        StartCoroutine(WriteText(message));
    }

    public void HideUI() {
        imageBoxes[0].SetActive(false);
        imageBoxes[1].SetActive(false);
        TextLabel.SetActive(false);
    }

    public IEnumerator WriteText(string message) {
        text = TextLabel.GetComponentInChildren<TMP_Text>();
        int count = 0;
        Timer timer = new Timer();
        //TODO: Test it
        while (count < message.Length) {
            count++;
            text.text = message.Substring(0, count);
            timer.SetTime(symbolDelay);
            while (!timer.Execute()) yield return null;
        }
    }
}
