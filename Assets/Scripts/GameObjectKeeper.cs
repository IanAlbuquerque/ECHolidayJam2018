using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectKeeper : MonoBehaviour
{
    public GameObject storedGameObject;

    public GameObject initialGameObject;

    void Start () {
        this.storedGameObject = initialGameObject;
    }
}
