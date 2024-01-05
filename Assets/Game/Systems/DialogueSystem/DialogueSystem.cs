using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueSystem : MonoBehaviour {
    public static DialogueSystem inst { get; private set; }

    private float symbolDelay;
    private DialogueSO currentDialogue;
    private int phraseIndex;
    private Phrase currentPhrase;
    private bool isTextAnimationPlaying, isPaused, wasUnpaused;
    public bool isDialogueOngoing { get; private set; }
    //View
    private Canvas dialogueCanvas;
    private GameObject textPanel;
    private TMP_Text text;
    private GameObject leftImageBox, rightImageBox;
    private Image leftImage, rightImage;

    public void Setup(float _symbolDelay, Canvas _dialogueCanvas, GameObject _textPanel, GameObject _leftImageBox, GameObject _rightImageBox) {
        symbolDelay = _symbolDelay;
        dialogueCanvas = _dialogueCanvas;
        textPanel = _textPanel;
        text = textPanel.GetComponentInChildren<TMP_Text>();
        leftImageBox = _leftImageBox; rightImageBox = _rightImageBox;
        leftImage = leftImageBox.transform.GetChild(0).GetComponent<Image>();
        rightImage = rightImageBox.transform.GetChild(0).GetComponent<Image>();

        if (wasUnpaused) {
            ResumeDialogue();
            wasUnpaused = false;
        }
    }

    public void Pause(bool _isPaused) {
        isPaused = _isPaused;
        if (!isPaused) wasUnpaused = true;
    }

    public void StartDialogue(DialogueSO dialogue) {
        currentDialogue = dialogue;
        isDialogueOngoing = true;
        SetUIVisibility(true);
        //Next phrase
        phraseIndex = 0;
        PlayNextPhrase();
    }

    private void ResumeDialogue() {
        SetUIVisibility(true);
        PlayNextPhrase();
    }

    private void PlayNextPhrase() {
        currentPhrase = currentDialogue.phrases[phraseIndex];
        phraseIndex++;
        currentPhrase.precallback.Invoke();
        //Update visuals
        if (currentPhrase.character.onRightSide) {
            rightImage.sprite = currentPhrase.character.face;
        } else {
            leftImage.sprite = currentPhrase.character.face;
        }
        leftImageBox.SetActive(!currentPhrase.character.onRightSide);
        rightImageBox.SetActive(currentPhrase.character.onRightSide);

        text.color = currentPhrase.character.textColor;
        StopAllCoroutines();
        StartCoroutine(WritePhraseText(currentPhrase.text));
    }

    private void SetUIVisibility(bool isVisible) {
        dialogueCanvas.gameObject.SetActive(isVisible);
    }

    private void Update() {
        if (isDialogueOngoing && Input.GetMouseButtonDown(0) && currentDialogue != null) {
            if (phraseIndex == currentDialogue.phrases.Count) {
                currentPhrase.callback.Invoke();
                if (isPaused) return;
                //End dialogue
                SetUIVisibility(false);
                isDialogueOngoing = false;
            } else {
                if (isTextAnimationPlaying) {
                    if (isPaused) return;
                    //Skip text animation
                    StopAllCoroutines();
                    text.text = currentPhrase.text;
                    isTextAnimationPlaying = false;
                } else {
                    currentPhrase.callback.Invoke();
                    if (isPaused) return;
                    PlayNextPhrase();
                }
            }
        }
    }

    public IEnumerator WritePhraseText(string message) {
        int count = 0;
        Timer timer = new Timer();
        isTextAnimationPlaying = true;
        while (count < message.Length) {
            count++;
            text.text = message.Substring(0, count);
            timer.SetTime(symbolDelay);
            while (!timer.Execute()) yield return null;
        }
        isTextAnimationPlaying = false;
    }

    private void Awake() {
        if (inst != null && inst != this) {
            Destroy(this);
        } else {
            inst = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
