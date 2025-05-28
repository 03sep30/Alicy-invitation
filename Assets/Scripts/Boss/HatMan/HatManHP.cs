using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HatManHP : BossHP
{
    private void OnDestroy()
    {
        SceneManager.LoadScene("Main_2BossCat_Map");
    }
}
