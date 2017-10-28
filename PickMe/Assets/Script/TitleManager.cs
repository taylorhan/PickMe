using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour {

    public Text StartText;
    float elapsedTime = 0;
    float fadingAlpha = 0f;
    float dtFade = 5;

    // Update is called once per frame
    void Update ()
    {
        SetTextAlpha();
    }

    void SetTextAlpha()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime > 0.5f)
        {
            elapsedTime = 0;
            if (dtFade > 0)
            {
                dtFade = -Time.deltaTime*2f;
            }
            else
            {
                dtFade = Time.deltaTime*2f;
            }
        }
        fadingAlpha += dtFade;
        if (fadingAlpha > 1)
        {
            fadingAlpha = 1;
        }
        else if (fadingAlpha < 0)
        {
            fadingAlpha = 0;
        }
        StartText.color = new Color(StartText.color.r, StartText.color.g, StartText.color.b, fadingAlpha);
    }

    public void StartGame()
    {
        // Load InGame Scene
        SceneManager.LoadScene("InGame");
    }
}
