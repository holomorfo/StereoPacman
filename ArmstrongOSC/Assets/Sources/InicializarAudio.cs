using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InicializarAudio : MonoBehaviour {

    public AudioClip audio1;
    public AudioClip audio2;
    public AudioClip audio3;
    public AudioClip audio4;
    public AudioClip audio5;
    public AudioClip audio6;
    public AudioClip audio7;
    public AudioClip audio8;
    public AudioClip audio9;

    public int id;
    // Use this for initialization
	void Start () {
        List<AudioClip> audios= new List<AudioClip>();
        audios.Add(audio1);
        audios.Add(audio2);
        audios.Add(audio3);
        audios.Add(audio4);
        audios.Add(audio5);
        audios.Add(audio6);
        audios.Add(audio7);
        audios.Add(audio8);
        audios.Add(audio9);

        GetComponent<AudioSource>().clip = audios[id%9];
        //GetComponent<AudioSource>().playOnAwake = true;
        GetComponent<AudioSource>().Play() ;
        Color col = Color.black;
        switch(id%9){
            case 0:
                col = Color.blue;
                break;
            case 1:
                col = Color.cyan;
                break;
            case 2:
                col = Color.gray;
                break;
            case 3:
                col = Color.green;
                break;
            case 4:
                col = Color.magenta;
                break;
            case 5:
                col = Color.red;
                break;
            case 6:
                col = Color.yellow;
                break;
            case 7:
                col = new Color(0.3F,0.1F,1F);
                break;
            case 8:
                col = new Color(0.1F, 0.7F, 0F);
                break;
        }
        GetComponent<MeshRenderer>().materials[0].color = col;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
