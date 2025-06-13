using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HatManHP : BossHP
{
    public TTSController ttsController;
    public GameObject deathVFX;
    public float SFDTime;

    private void OnDestroy()
    {
        ttsController.PlayTTS();
        GameObject VFX = Instantiate(deathVFX, transform.position, Quaternion.identity);
        VFX.GetComponent<DestroyObject>().StartDestroy(VFX, SFDTime);
    }
}
