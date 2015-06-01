using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityOSC;
using UnityEngine.UI;

public class ManejarOSC : MonoBehaviour
{

    public GameObject cuadritoPrefab;
    GameObject g;
    public Text textoObj;
    public int identificador;
    // OSCHandler oschand;
    public string IP;
    public int puerto;
    private Dictionary<string, ServerLog> servers;
    public bool serv = false;
    public Vector3 posicionPasada;
    public bool presionado = false;
    public bool soltadoUnaVez = true;
    // Script initialization

    void Start()
    {
        // 192.168.1.86 wifi FLorida
        // 192.168.1.101 Router Wifi
        Debug.Log("Creado Servidor");
        identificador = (int)UnityEngine.Random.Range(0, 10000);
        //        OSCHandler.Instance.Init("127.0.0.1", 57120); //init OSC
        OSCHandler.Instance.Init(IP, puerto, serv); //init OSC

        servers = new Dictionary<string, ServerLog>();
        if (!serv)
        {
            g = Instantiate(cuadritoPrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            g.GetComponent<InicializarAudio>().id = identificador;
            g.GetComponent<ColisionesPacmans>().serv = false;
            g.transform.position = new Vector3(0, 0, 0);
            List<object> values = new List<object>();
            values.AddRange(new object[] { identificador, 0.5f / Screen.width, 0.5f / Screen.height });
            OSCHandler.Instance.SendMessageToClient("SuperCollider", "address/Mensaje", values);
            Debug.Log("Envio inicializacion" + Input.mousePosition.x / Screen.width + " " + Input.mousePosition.y / Screen.height);
            g.transform.position = new Vector3(0, 0, 0);
        }
    }


    // NOTE: The received messages at each server are updated here
    // Hence, this update depends on your application architecture
    // How many frames per second or Update() calls per frame?
    void Update()
    {
        // Si no es servidor, enviar mensajes
        if (!serv)
        {
            g.transform.position = new Vector3(0, 0, 0);
            if (Input.GetMouseButtonDown(0))
            {
                presionado = true;
                soltadoUnaVez = true;
            }
            if (Input.GetMouseButtonUp(0))
            {
                presionado = false;
            }

            if (presionado)
            {
                Vector3 vectAct = posicionPasada - Input.mousePosition;
             //   Debug.Log("Distancia " + vectAct.magnitude);
                posicionPasada = Input.mousePosition;
                if (vectAct.magnitude > 0)
                {
                    List<object> values = new List<object>();
                    values.AddRange(new object[] { identificador, Input.mousePosition.x / Screen.width, Input.mousePosition.y / Screen.height });
                    OSCHandler.Instance.SendMessageToClient("SuperCollider", "address/Mensaje", values);
                    Debug.Log("Envio mensaje mouse: " + Input.mousePosition.x / Screen.width + " " + Input.mousePosition.y / Screen.height);
                }
            }
            else
            {
                if (soltadoUnaVez)
                {
                    soltadoUnaVez = false;
                    List<object> values = new List<object>();
                    values.AddRange(new object[] { identificador, Input.mousePosition.x / Screen.width, Input.mousePosition.y / Screen.height });
                    OSCHandler.Instance.SendMessageToClient("SuperCollider", "address/Mensaje", values);
                    Debug.Log("Envio mensaje mouse off: " + 0.5f * Screen.width + " " + 0.5f * Screen.height);
                }
            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                Debug.Log("Up");
                soltadoUnaVez = false;
                List<object> values = new List<object>();
                values.AddRange(new object[] { identificador, 0f, 1f});
                OSCHandler.Instance.SendMessageToClient("SuperCollider", "address/Mensaje", values);
            //    Debug.Log("Envio mensaje mouse off: " + 0.5f * Screen.width + " " + 0.5f * Screen.height);
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                Debug.Log("Dn");
                soltadoUnaVez = false;
                List<object> values = new List<object>();
                values.AddRange(new object[] { identificador, 0, -1});
                OSCHandler.Instance.SendMessageToClient("SuperCollider", "address/Mensaje", values);
              //  Debug.Log("Envio mensaje mouse off: " + 0.5f * Screen.width + " " + 0.5f * Screen.height);
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                Debug.Log("Lf");
                soltadoUnaVez = false;
                List<object> values = new List<object>();
                values.AddRange(new object[] { identificador, -1f, 0});
                OSCHandler.Instance.SendMessageToClient("SuperCollider", "address/Mensaje", values);
                //Debug.Log("Envio mensaje mouse off: " + 0.5f * Screen.width + " " + 0.5f * Screen.height);
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                Debug.Log("Rt");
                soltadoUnaVez = false;
                List<object> values = new List<object>();
                values.AddRange(new object[] { identificador, 1, 0});
                OSCHandler.Instance.SendMessageToClient("SuperCollider", "address/Mensaje", values);
                Debug.Log("Envio mensaje mouse off: " + 0.5f * Screen.width + " " + 0.5f * Screen.height);
            }
        }
        else
        {
            OSCHandler.Instance.UpdateLogs();
        }
    }


    public void Restart()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
}
