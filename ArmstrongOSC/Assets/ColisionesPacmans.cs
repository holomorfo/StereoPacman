using UnityEngine;
using System.Collections;

public class ColisionesPacmans : MonoBehaviour
{
    public bool serv = true;
    public bool soyPacman = false;
    public Vector3 ultimaVelocidad;
    public long tiempoPasado = 0;
    bool cuentaRegVF = false;
    float velPacman = 11;
    float velFantasma = 7;
    float volPacman = 0.75f;
    float volFantasma = 0.2f;

    // Use this for initialization
    void Start()
    {
        tiempoPasado = System.DateTime.Now.Ticks;
        Debug.Log("Tiempo NOW!! "+System.DateTime.Now.Ticks);
        if (!serv)
        {
            transform.position = vectorAleatorio();
        }
        GetComponent<AudioSource>().volume = volFantasma;
    }

    // Update is called once per frame
    void Update()
    {
        if (cuentaRegVF)
        {
//            Debug.Log("Diferencia de tiempo " + (System.DateTime.Now.Ticks - tiempoPasado));
            if ((System.DateTime.Now.Ticks - tiempoPasado) > 1000000)
            {
                cuentaRegVF = false;
                soyPacman = false;        
                transform.localScale -= new Vector3(15F, 15F, 0);
                GetComponent<AudioSource>().volume = volFantasma;
            }
        }
       // transform.position += ultimaVelocidad;
        mover();
        if (transform.position.x > 600 || transform.position.x < -600 || transform.position.y > 600 || transform.position.y < -600)
        {
            transform.position = vectorAleatorio();
        }
    }

    public void grabarUltimaVelocidad(float px, float py){
        ultimaVelocidad = new Vector3(px, py, 0);
    }
    
    public void mover()
    {
        if (soyPacman)
        {
            transform.position += new Vector3(ultimaVelocidad.x * velPacman, ultimaVelocidad.y * velPacman, 0.0f);
        }
        else
        {
            transform.position += new Vector3(ultimaVelocidad.x * velFantasma, ultimaVelocidad.y * velFantasma, 0.0f);
        }
        //if (Input.GetKey(KeyCode.UpArrow))
        //{
        //    Debug.Log("Up");
        //    ultimaVelocidad = new Vector3(0, 3, 0.0f);
        //    Debug.Log("Ultima velocidad up " + ultimaVelocidad);
        //    transform.position += new Vector3(ultimaVelocidad.x * 3, ultimaVelocidad.y * 3, 0.0f);
        //}
        //if (Input.GetKey(KeyCode.DownArrow))
        //{
        //    Debug.Log("Dn");
        //    ultimaVelocidad = new Vector3(0, -3, 0.0f);
        //    transform.position += new Vector3(ultimaVelocidad.x * 3, ultimaVelocidad.y * 3, 0.0f);
        //}
        //if (Input.GetKey(KeyCode.LeftArrow))
        //{
        //    Debug.Log("Lf");
        //    ultimaVelocidad = new Vector3(-3, 0, 0.0f);
        //    transform.position += new Vector3(ultimaVelocidad.x * 3, ultimaVelocidad.y * 3, 0.0f);
        //}
        //if (Input.GetKey(KeyCode.RightArrow))
        //{
        //    Debug.Log("Rt");
        //    ultimaVelocidad = new Vector3(3, 0, 0.0f);
        //    transform.position += new Vector3(ultimaVelocidad.x * 3, ultimaVelocidad.y * 3, 0.0f);
        //}

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Jugador")
        {
            Debug.Log("Choque con el jugador: " + collision.gameObject.name);
            if (collision.gameObject.GetComponent<ColisionesPacmans>().soyPacman && soyPacman==false)
            {
                collision.gameObject.GetComponent<ColisionesPacmans>().desconvertiAPacman();
                convertiAPacman();
            }
        }

        if (collision.gameObject.tag == "Trifuerza")
        {
            convertiAPacman();
            Destroy(collision.gameObject);
        }
    }

    public void convertiAPacman()
    {
        soyPacman = true;
        transform.localScale += new Vector3(15F, 15F, 0);
        GetComponent<AudioSource>().volume = volPacman;
    }

    public void desconvertiAPacman()
    {
        transform.position = vectorAleatorio();
        //soyPacman = false;
        //transform.localScale -= new Vector3(15F, 15F, 0);
        // Comenzar cuenta regresiva =P
        cuentaRegVF = true;
    }

    public Vector3 vectorAleatorio()
    {
        Vector3 vec = new Vector3();
        switch((int)Random.Range(0,8)){
            case 0:
                vec = new Vector3(410f,420f,38.0f);
                break;
            case 1:
                vec = new Vector3(410f, 235f, 38.0f);
                break;
            case 2:
                vec = new Vector3(410f, -110f, 38.0f);
                break;
            case 3:
                vec = new Vector3(410f, -350f, 38.0f);
                break;
            case 4:
                vec = new Vector3(-300f, 420f, 38.0f);
                break;
            case 5:
                vec = new Vector3(-300f, 235f, 38.0f);
                break;
            case 6:
                vec = new Vector3(-300f, -110f, 38.0f);
                break;
            case 7:
                vec = new Vector3(-300f, -350f, 38.0f);
                break;
        }
        return vec;
    }
}
