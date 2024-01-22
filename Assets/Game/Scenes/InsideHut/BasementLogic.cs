using UnityEngine;

public class BasementLogic : MonoBehaviour
{
    [SerializeField] AudioClip SpookySound;
    void Start()
    {
        FindObjectOfType<TimeToLose>().StartTimer(90);
    }
    public void StopTimer() => FindObjectOfType<TimeToLose>().StopTimer();

}
