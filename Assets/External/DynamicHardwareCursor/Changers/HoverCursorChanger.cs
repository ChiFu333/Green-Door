using UnityEngine;

public class HoverCursorChanger : MonoBehaviour {
    [SerializeField] private CursorStateSO cursor;
    [SerializeField] private bool isTemporal = true;
    public void SetCursor() => HardwareCursorManager.inst.SetCursor(cursor);
    public void ResetCursor() {
        if (isTemporal) {
            HardwareCursorManager.inst.ResetCursor();
        }
    }
}
