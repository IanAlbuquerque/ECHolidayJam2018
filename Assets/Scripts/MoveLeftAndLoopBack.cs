using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeftAndLoopBack : MonoBehaviour
{

    public float speed = 1.0f;
    private SpriteRenderer spriteRenderer;
    public float limitLeft = -4.0f;

    public GameObjectKeeper keeper;

    // Start is called before the first frame update
    void Start()
    {
        this.spriteRenderer = this.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = this.transform.position;
        pos.x -= this.speed * Time.deltaTime;
        if(pos.x + (this.spriteRenderer.bounds.size.x / 2.0f) < this.limitLeft) {
            SpriteRenderer keeperSpriteRenderer = this.keeper.storedGameObject.transform.GetComponent<SpriteRenderer>();
            float storedX = this.keeper.storedGameObject.transform.position.x;
            float halfSizeStored = (keeperSpriteRenderer.bounds.size.x / 2.0f);
            pos.x = storedX + halfSizeStored * 2.0f - this.speed * Time.deltaTime;// + (this.spriteRenderer.bounds.size.x * this.transform.localScale.x * this.keeper.gameObject.transform.localScale.x / 4.0f);
            keeper.storedGameObject = this.gameObject;
        }
        this.transform.position = pos;
    }
}
