using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "OrchestratorData", menuName = "Orchestration/Data")]
public class OrchestratorDataSO : ScriptableObject {
    [field: SerializeField] public string orchestratorType { get; private set; }
}
