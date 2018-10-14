using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class MenuUI : MonoBehaviour
{

    public AudioClip[] thunderSound;

    public AudioSource thunderSource;

    public Animator lightningFlash;

    bool thunderPlayed;
    bool keepPlaying = true;
    int soundWait = 2;
    int Num;

    // Use this for initialization
    void Start()
    {
        //FireSource.clip = fireSound;
        //FireSource.Play();

        StartCoroutine(ThunderSounds());
    }

    // Update is called once per frame
    void Update()
    {



    }



    public void loadSceneOnPlay()
    {
        SceneManager.LoadScene("Opening", LoadSceneMode.Single);

    }

    public void exitOnExit()
    {
        Application.Quit();
        Debug.Log("QUIT");

    }


    IEnumerator ThunderSounds()
    {
       // thunderPlayed = true;
        while (keepPlaying)
        {
           // thunderPlayed = true;
            Num = Random.Range(0, 3);
            thunderSource.clip = thunderSound[Num];
            thunderSource.Play();
            lightningFlash.SetTrigger("LightningFlash");
            //thunderPlayed = false;
            Debug.Log(Num);
            yield return new WaitForSeconds(20f);
        }

    }
}
