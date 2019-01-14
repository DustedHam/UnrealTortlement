using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnrealTortlement.Weapons;
using UnityEngine.SceneManagement;

public class VictoryScene : MonoBehaviour
{

    public Weapon ak;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("VictoryCoroutine");
    }

    public void ChangeScene(string scene)
    {
        StopCoroutine("VictoryCoroutine");
        SceneManager.LoadScene(scene);
    }

    private IEnumerator VictoryCoroutine()
    {
        while(true)
        {
            yield return null;
            ak._ammoCount = 3;
            ak.tryFire("");
        }
    }
}
