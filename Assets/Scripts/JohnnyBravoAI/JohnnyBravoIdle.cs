using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JohnnyBravoIdle : MonoBehaviour, EnemyAttackPattern
{
    private bool isRunning = false;

    public void startAttackPattern() {
        this.isRunning = true;
    }

    public void stopAttackPattern() {
        this.isRunning = false;
    }

}
