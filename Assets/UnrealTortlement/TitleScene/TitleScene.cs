using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using UnrealTortlement.Weapons;

public class TitleScene : MonoBehaviour
{
    [SerializeField]
    Weapon weapon;

    void Awake()
    {
        weapon.gameObject.SetActive(false);
        weapon.Reload(15);
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
        weapon.gameObject.SetActive(true);
        StartCoroutine("PlayCoroutine");
    }

    IEnumerator PlayCoroutine()
    {
        for(var i = 0; i < 5;i++)
        {
            weapon.tryFire("");
            yield return new WaitForSeconds(0.5f);           
            weapon.tryFire("");
        }
        ChangeScene("map1");
    }
}
