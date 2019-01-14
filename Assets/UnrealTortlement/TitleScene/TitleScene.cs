using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using UnrealTortlement.Weapons;

public class TitleScene : MonoBehaviour
{

    [SerializeField]Weapon AK;

    void Awake()
    {
        AK.gameObject.SetActive(false);
        AK._ammoCount = 30;
    }

    public void ChangeScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void Play()
    {
        AK.gameObject.SetActive(true);
        StartCoroutine("PlayCoroutine");
    }

    IEnumerator PlayCoroutine()
    {
        for(var i = 0; i < 5;i++)
        {
            AK.tryFire("");
            yield return new WaitForSeconds(0.5f);
            AK.tryFire("");
        }
        ChangeScene("map1");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
