using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeToLose : MonoBehaviour
{
    public static TimeToLose inst { get; private set; }
    private TMP_Text text;
    public int myTime;
    public bool timeIsComing = false;
    private GameObject timeHolder;
    [SerializeField] private AudioQuery AQ;
    public void Awake()
    {
        if (inst != null && inst != this)
        {
            Destroy(this);
        }
        else
        {
            inst = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    public void StartTimer(int time)
    {
        
        myTime = time;
        timeIsComing = true;
        StopAllCoroutines();
        StartCoroutine(Timer());
    }
    private IEnumerator Timer()
    {
        if(text == null)
        {
            timeHolder = GameObject.Find("TimerCanvas");
            GameObject th = timeHolder.transform.GetChild(0).gameObject;
            th.SetActive(true);
            text = th.GetComponentInChildren<TMP_Text>();
        }
        text.text = myTime.ToString();
        yield return new WaitForSeconds(1);
        myTime--;
        if(myTime <= 0)
        {
            AudioManager.inst.Play(AQ);
            timeIsComing = false;
            SceneLoader.inst.LoadScene("BadEnd", false);
        }
        else
        {
            StartCoroutine(Timer());
        }
        
    }
}
