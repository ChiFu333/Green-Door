using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using JetBrains.Annotations;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEditor.U2D.Path.GUIFramework;

public class DialogSystem : MonoBehaviour
{
    [SerializeField] private Sprite[] faces = new Sprite[6];
    [SerializeField] private GameObject[] imageBoxes = new GameObject[2];
    [SerializeField] private GameObject TextLabel;
    private TMP_Text text;
    [SerializeField] private Color[] colors = new Color[2];
    [SerializeField] private GameObject clickPanel;
    [SerializeField] private float timeToText = 0.02f;
    private int faceId = 0;
    private void Start()
    {
        
    }
    public void SetFaceId(int id)
    {
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
    public void HideUI()
    {
        imageBoxes[0].SetActive(false);
        imageBoxes[1].SetActive(false);
        TextLabel.SetActive(false);
    }
    public IEnumerator WriteText(string message)
    {
        text = TextLabel.GetComponentInChildren<TMP_Text>();
        int count = 0;
        while (true)
        {
            if (count < message.Length)
            {
                count++;
                text.text = message.Substring(0, count);
                //if (count % 2 == 0) ASP.Play();
                yield return new WaitForSeconds(timeToText);
            }
            else
            { 
                break; 
            }
        }
    }
}
