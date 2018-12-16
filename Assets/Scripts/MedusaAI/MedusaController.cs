using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedusaController : MonoBehaviour
{
    private List<EnemyAttackPattern> patterns = new List<EnemyAttackPattern>();
    private List<float> durations = new List<float>();

    private float counter = 0.0f;
    private int currentPattern = 0;

    public void Start() {
        this.patterns.Add(this.GetComponent<MedusaWanderWithGas>());
        this.durations.Add(20.0f);
        this.patterns.Add(this.GetComponent<MedusaWander>());
        this.durations.Add(20.0f);
        this.patterns.Add(this.GetComponent<MedusaFirework>());
        this.durations.Add(20.0f);
        this.patterns[this.currentPattern].startAttackPattern();
    }

    public void Update() {
        this.counter += Time.deltaTime;
        if(this.counter > this.durations[this.currentPattern]) {
            this.counter = 0.0f;
            this.patterns[this.currentPattern].stopAttackPattern();
            this.currentPattern = (this.currentPattern + 1) % this.patterns.Count;
            this.patterns[this.currentPattern].startAttackPattern();
        }
    }
}
