using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CentaurCloneController : MonoBehaviour
{
    private EnemyAttackPattern currentPattern;

    public void OnDestroy() {
        this.currentPattern.stopAttackPattern();
    }

    public void Start() {
        this.currentPattern = this.GetComponent<CentaurCharge>();
        this.currentPattern.startAttackPattern();
    }
}
