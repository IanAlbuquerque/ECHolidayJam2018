using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Credits : MonoBehaviour
{
    public TextAsset CreditsFile;
    public float TimeBeforeResetRoll = 20f;
    public float Speed = 1f;
    float curTime;
    Text text;
    RectTransform rectTransform;

    // Start is called before the first frame update
    void OnEnable()
    {
        curTime = 0;
        text = GetComponent<Text>();
        text.text = CreditsFile.text;
        rectTransform = GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(0,40);
    }

    // Update is called once per frame
    void Update()
    {
        curTime += Time.deltaTime;
        transform.Translate(Vector3.up * Speed * Time.deltaTime);

        if(curTime >= TimeBeforeResetRoll){
            curTime = 0;
            rectTransform.anchoredPosition = Vector2.zero;
        }
    }

    public void Back(){
        transform.parent.parent.GetChild(0).gameObject.SetActive(true);
        transform.parent.gameObject.SetActive(false);
    }
}
