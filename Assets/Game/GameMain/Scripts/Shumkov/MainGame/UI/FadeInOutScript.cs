using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeInOutScript : MonoBehaviour
{
    public void FadeInEnd()
    {
        this.gameObject.SetActive(false);
    }

    public void FadeOutEnd()
    {
        SceneManager.LoadScene(0);
    }
    public void FadeOutEndMainToGame()
    {
        SceneManager.LoadScene(1);
    }
    public void SetLowOpacity()
    {
        GetComponent<Image>().color = Color.black;
    }
}
