using UnityEngine;
using UnityEngine.Events;

public class ClickableThing : MonoBehaviour {
    [SerializeField] private UnityEvent callback = new UnityEvent();
    
    public virtual void HandleClick() {
        callback.Invoke();
    }
}