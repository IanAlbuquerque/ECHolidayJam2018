using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JohnnyBravoController : MonoBehaviour
{
    private List<EnemyAttackPattern> patterns = new List<EnemyAttackPattern>();
    private List<float> durations = new List<float>();

    private float counter = 0.0f;
    private int currentPattern = 0;

    private int phase = 0;

    public BossHealth BossHealth;

    public void OnDestroy() {
        this.patterns[this.currentPattern].stopAttackPattern();
    }

    public void Start() {
        this.BossHealth = (BossHealth) FindObjectsOfType(typeof(BossHealth))[0];
        this.phase = 0;
        this.currentPattern = 0;
        this.counter = 0.0f;
        this.patterns.Add(this.GetComponent<JohnnyBravoFlower>());
        this.durations.Add(10.0f);
        this.patterns.Add(this.GetComponent<JohnnyBravoIdle>());
        this.durations.Add(8.0f);
        this.patterns.Add(this.GetComponent<JohnnyBravoGattling>());
        this.durations.Add(2.0f);
        this.patterns.Add(this.GetComponent<JohnnyBravoIdle>());
        this.durations.Add(1.0f);
        this.patterns.Add(this.GetComponent<JohnnyBravoGattling>());
        this.durations.Add(2.0f);
        this.patterns.Add(this.GetComponent<JohnnyBravoIdle>());
        this.durations.Add(1.0f);
        this.patterns.Add(this.GetComponent<JohnnyBravoGattling>());
        this.durations.Add(2.0f);
        this.patterns.Add(this.GetComponent<JohnnyBravoIdle>());
        this.durations.Add(1.0f);
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
        if(BossHealth.hp / BossHealth.maxHp <= 0.5f && this.phase == 0) {
            this.patterns[this.currentPattern].stopAttackPattern();
            this.currentPattern = 0;
            this.counter = 0.0f;
            this.phase = 1;
            this.patterns = new List<EnemyAttackPattern>();
            this.durations = new List<float>();
            this.patterns.Add(this.GetComponent<JohnnyBravoFlowerStanding>());
            this.durations.Add(3.5f);
            this.patterns.Add(this.GetComponent<JohnnyBravoFlower>());
            this.durations.Add(10.0f);
            this.patterns[this.currentPattern].startAttackPattern();
        }
    }
}
