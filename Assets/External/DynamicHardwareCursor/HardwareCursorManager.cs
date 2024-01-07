using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class HardwareCursorManager : MonoBehaviour {
    [SerializeField] private CursorStateSO defaultCursorState;
    public bool isWorld = true;
    private CursorStateSO currentCursorState;
    HoverCursorChanger currentHoveredChanger;
    public void SetCursor(CursorStateSO newCursorState) {
        if (newCursorState == null) return;
        currentCursorState = newCursorState;
        UpdateHardwareCursor();
    }

    public CursorStateSO GetCurrentCursor() => currentCursorState;

    public void ResetCursor() {
        if (defaultCursorState == null) return;
        currentCursorState = defaultCursorState;
        UpdateHardwareCursor();
    }

    #region Internal
    private void UpdateHardwareCursor() {
        Cursor.SetCursor(currentCursorState.GetTexture(), currentCursorState.GetHotspot(), CursorMode.Auto);
    }

    private void Update() {
        HandleWorldChangers();
    }

    private void HandleWorldChangers() {
        if (!isWorld) return;
        //Find new changers
        List<HoverCursorChanger> changers = new List<HoverCursorChanger>();
        List<GameObject> objects = ScreenUtils.GetObjectsUnderMouse();
        for (int i = 0; i < objects.Count; i++) {
            HoverCursorChanger changer = objects[i].GetComponent<HoverCursorChanger>();
            if (changer == null) continue;
            changers.Add(changer);
        }
        //If no currentChanger - look for new
        if (currentHoveredChanger == null) {
            if (changers.Count >= 1) {
                currentHoveredChanger = changers[0];
                currentHoveredChanger.SetCursor();
            } else {
                //ResetCursor();
                Player.inst.controller.ExternalCursorReset();
            }
            return;
        }
        //If not covered but was - reset
        if (!changers.Contains(currentHoveredChanger)) {
            currentHoveredChanger.ResetCursor();
            currentHoveredChanger = null;
        }
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
            SceneManager.sceneLoaded += (scene, type) => {
                if (Player.inst == null) isWorld = false;
                ResetCursor();
            };
            ResetCursor();
        }
    }
    #endregion
}
