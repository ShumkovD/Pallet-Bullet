using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class demoText : MonoBehaviour
{
    private Text text;
    float time;
    public float speed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        text = this.gameObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        text.color = GetAlphaColor(text.color);
    }

    Color GetAlphaColor(Color color)
    {
        time += Time.deltaTime * 5.0f * speed;
        color.a = Mathf.Sin(time);

        return color;
    }
}
