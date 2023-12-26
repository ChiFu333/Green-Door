using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class HardwareCursorManager : MonoBehaviour {
    [SerializeField] private CursorStateSO defaultCursorState;
    private CursorStateSO currentCursorState;
    public void SetCursor(CursorStateSO newCursorState) {
        if (newCursorState == null) return;
        currentCursorState = newCursorState;
        UpdateHardwareCursor();
    }

    public void ResetCursor() {
        if (defaultCursorState == null) return;
        currentCursorState = defaultCursorState;
        UpdateHardwareCursor();
    }

    #region Internal
    private void UpdateHardwareCursor() {
        Cursor.SetCursor(currentCursorState.GetTexture(), currentCursorState.GetHotspot(), CursorMode.Auto);
    }

    [MenuItem("GameObject/Delta/HardwareCursorManager")]
    private static void CreateHardwareCursorManager() {
        GameObject hcmo = new GameObject("HardwareCursorManager");
        hcmo.AddComponent<HardwareCursorManager>();
    }
    #endregion

    #region Singleton
    public static HardwareCursorManager inst { get; private set; }
    private void Awake() {
        if (inst != null && inst != this) {
            Destroy(this);
        } else {
            inst = this;
            DontDestroyOnLoad(gameObject);
            ResetCursor();
        }
    }
    #endregion
}
