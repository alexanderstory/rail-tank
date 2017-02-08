using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextController : MonoBehaviour {

    public float fadeSpeed;
    public float targetAlpha;
    private float maxAlpha;
    private Color alphaColor;
    private Text text;

    void Awake()
    {
        text = GetComponent<Text>();
        text.text = "";
        alphaColor = text.color;
        maxAlpha = text.color.a;

        SetAlpha(0.0f);
        
    }

    void OnGUI()
    {
        text.color = alphaColor;
    }

    void Update()
    {
        alphaColor.a = Mathf.MoveTowards(alphaColor.a, targetAlpha, Time.deltaTime * fadeSpeed);
    }



    public void UpdateText(string updateText)
    {
        text.text = updateText;
        targetAlpha = maxAlpha;
    }

    public void SetAlpha(float alpha)
    {
        alphaColor.a = alpha;
        targetAlpha = alpha;
    }

    public void FadeIn()
    {
        SetAlpha(0.0f);
        targetAlpha = maxAlpha;
    }

    public void FadeOut()
    {
        SetAlpha(maxAlpha);
        targetAlpha = 0.0f;
    }

    public bool IsEmpty()
    {
        return text.text == "";
    }



    //IEnumerator SetTarget(string text, bool fade)
    //{

    //    if (GetComponent<Text>().text == "" && fade == true)
    //    {
    //        GetComponent<Text>().text = text;
    //        targetAlpha = maxAlpha;
    //        yield return new WaitForSeconds(fadeSpeed);
    //    }
    //    else if (text == "" && fade == true)
    //    {
    //        targetAlpha = 0.0f;
    //        yield return new WaitForSeconds(fadeSpeed);
    //        GetComponent<Text>().text = text;
    //    }
    //    else if (fade == true)
    //    {
    //        targetAlpha = 0.0f;
    //        yield return new WaitForSeconds(fadeSpeed);
    //        GetComponent<Text>().text = text;
    //        targetAlpha = maxAlpha;
    //        yield return new WaitForSeconds(fadeSpeed);
    //    }
    //    else
    //    {
    //        targetAlpha = maxAlpha;
    //        GetComponent<Text>().text = text;
    //        yield return new WaitForSeconds(fadeSpeed);
    //    }


    //}
}
