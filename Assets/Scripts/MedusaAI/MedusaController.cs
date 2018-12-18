using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedusaController : MonoBehaviour
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
        this.patterns.Add(this.GetComponent<MedusaWander>());
        this.durations.Add(10.0f);
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
        if(BossHealth.hp / BossHealth.maxHp <= 0.75f && this.phase == 0) {
            this.patterns[this.currentPattern].stopAttackPattern();
            this.currentPattern = 0;
            this.counter = 0.0f;
            this.phase = 1;
            this.patterns = new List<EnemyAttackPattern>();
            this.durations = new List<float>();
            this.patterns.Add(this.GetComponent<MedusaFirework>());
            this.durations.Add(20.0f);
            this.patterns.Add(this.GetComponent<MedusaWander>());
            MedusaWander wanderScript = this.GetComponent<MedusaWander>();
            wanderScript.shootCooldown *= 0.8f;
            this.durations.Add(20.0f);
            this.patterns[this.currentPattern].startAttackPattern();
        }
        if(BossHealth.hp / BossHealth.maxHp <= 0.25f && this.phase == 1) {
            this.patterns[this.currentPattern].stopAttackPattern();
            this.currentPattern = 0;
            this.counter = 0.0f;
            this.phase = 2;
            this.patterns = new List<EnemyAttackPattern>();
            this.durations = new List<float>();
            this.patterns.Add(this.GetComponent<MedusaFirework>());
            this.durations.Add(10.0f);
            MedusaFirework fireworkScript = this.GetComponent<MedusaFirework>();
            fireworkScript.shootCooldown *= 0.7f;
            this.patterns.Add(this.GetComponent<MedusaWanderWithGas>());
            this.durations.Add(20.0f);
            this.patterns[this.currentPattern].startAttackPattern();
        }
    }
}
