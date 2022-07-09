using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Image fadeImage;
    [SerializeField] private Text levelText;

    public bool isFaded = false;
    void Update()
    {
        levelText.text = "Level : " + TestGameManager.Instance.gameLevel;
    }

    // Update is called once per frame
    public void StartFadein()
    {
        StartCoroutine("FadeIn");
    }
    
    public void StartFadeout()
    {
        StartCoroutine("FadeOut");
    }


    IEnumerator FadeIn()
    {
        float fade = 0f;
        while (fade < 1)
        {
            yield return new WaitForSecondsRealtime(Time.unscaledDeltaTime);
            fade += 0.1f;
            fadeImage.color = new Color(0, 0, 0, fade);
        }

        isFaded = true;
    }

    IEnumerator FadeOut()
    {
        float fade = 1f;
        while (fade > 0)
        {
            yield return new WaitForSecondsRealtime(Time.unscaledDeltaTime);
            fade -= 0.1f;
            fadeImage.color = new Color(0, 0, 0, fade);
        }

        isFaded = false;
    }
}
