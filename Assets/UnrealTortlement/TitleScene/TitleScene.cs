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
        AK.Reload();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ChangeScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void Play()
    {
        AK.gameObject.SetActive(true);
        startCoroutine("PlayCoroutine");
    }

    IEnumerator PlayCoroutine()
    {
        for(var i = 0; i < 5;i++)
        {
            AK.tryFire();
            var startTime = Time.deltaTime;
            while(startTime.deltaTime - startTime < 0.5)
            {
                yield return null;
            }
            AK.tryFire();
        }
        ChangeScene("map1");
    }
}
