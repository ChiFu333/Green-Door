using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerAndDo : MonoBehaviour
{
    public bool first = true;
    [SerializeField] private GameObject AnotherTrigger;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if(first)
            {
                DialogueSystem.inst.StartDialogue(FindObjectOfType<MagicOrchestrator>().castedData.GoToFood);
                AnotherTrigger.SetActive(true);
            }
            else
            {
                DialogueSystem.inst.StartDialogue(FindObjectOfType<MagicOrchestrator>().castedData.CatIsAngry);
            }
            Destroy(gameObject);
        }
    }
}
