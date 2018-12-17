using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CentaurController : MonoBehaviour
{
    public Vector2 cloneSpawnPoint;
    private float counter = 0.0f;
    private EnemyAttackPattern currentPattern;

    private int phase = 0;

    private GameObject clone;

    public void Start() {
        this.phase = 0;
        this.counter = 0.0f;
        this.currentPattern = this.GetComponent<CentaurChargeWithoutAura>();
        this.currentPattern.startAttackPattern();
    }

    private void spawnClone() {
        Vector3 spawnPoint = new Vector3(this.cloneSpawnPoint.x, this.cloneSpawnPoint.y, 0.0f);
        this.clone = (GameObject) Instantiate(Resources.Load("centaur_clone"), spawnPoint, Quaternion.identity);
        CentaurCharge centaurCharge = (CentaurCharge) this.clone.GetComponent<CentaurCharge>();
        centaurCharge.standingPosition = new Vector2(this.clone.transform.position.x, this.clone.transform.position.y);
        centaurCharge.isMirror = true;
        centaurCharge.mirrorCenter = this.GetComponent<CentaurChargeWithoutAura>().standingPosition;
        centaurCharge.mirrorObject = this.gameObject;
    }

    public void OnDestroy() {
        this.currentPattern.stopAttackPattern();
        Destroy(this.clone);
    }

    public void Update() {
        this.counter += Time.deltaTime;
        if(BossHealth.Instance.hp / BossHealth.Instance.maxHp <= 0.66f && this.phase == 0) {
            this.currentPattern.stopAttackPattern();
            this.counter = 0.0f;
            this.phase = 1;
            this.currentPattern = this.GetComponent<CentaurCharge>();
            this.currentPattern.startAttackPattern();
        }
        if(BossHealth.Instance.hp / BossHealth.Instance.maxHp <= 0.25f && this.phase == 1) {
            this.currentPattern.stopAttackPattern();
            this.counter = 0.0f;
            this.phase = 2;
            this.currentPattern = this.GetComponent<CentaurCharge>();
            CentaurCharge centaurCharge = (CentaurCharge) this.currentPattern;
            centaurCharge.standingPosition = centaurCharge.standingPosition - this.cloneSpawnPoint;
            centaurCharge.onStand.subscribe(this.spawnClone);
            this.currentPattern.startAttackPattern();
        }
    }
}
