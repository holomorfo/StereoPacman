//
//	  UnityOSC - Example of usage for OSC receiver
//
//	  Copyright (c) 2012 Jorge Garcia Martin
//
// 	  Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated 
// 	  documentation files (the "Software"), to deal in the Software without restriction, including without limitation
// 	  the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, 
// 	  and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// 	  The above copyright notice and this permission notice shall be included in all copies or substantial portions 
// 	  of the Software.
//
// 	  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
// 	  TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL 
// 	  THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF 
// 	  CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS
// 	  IN THE SOFTWARE.
//

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityOSC;
using UnityEngine.UI;

public class oscControl : MonoBehaviour
{
    public Text textoObj;
    public int identificador;
    public GameObject oscH;
    OSCHandler oschand;
    public string IP;
    public int puerto;
    private Dictionary<string, ServerLog> servers;
    bool ipCorrecta = false;
    bool creadoServidor = false;
    public bool servidor = false;
    // Script initialization
    void Start()
    {
        // 192.168.1.86 wifi FLorida
        // 192.168.1.101 Router Wifi
            Debug.Log("Creado Servidor");
            OSCHandler.Instance.Init("192.168.1.101", 150,servidor); //init OSC
            servers = new Dictionary<string, ServerLog>();
            creadoServidor = true;
        
    }

    // NOTE: The received messages at each server are updated here
    // Hence, this update depends on your application architecture
    // How many frames per second or Update() calls per frame?
    void Update()
    {
        //oschand.SendMessageToClient("SuperCollider", "/mX", Input.mousePosition.normalized.x);
        //oschand.SendMessageToClient("SuperCollider", "/mY", Input.mousePosition.normalized.y);

        //oschand.SendMessageToClient("SuperCollider", "/aX", Input.acceleration.x);
        //oschand.SendMessageToClient("SuperCollider", "/aY", Input.acceleration.y);
        //oschand.SendMessageToClient("SuperCollider", "/aZ", Input.acceleration.z);
        if (!servidor)
        {
            if (ipCorrecta)
            {
                if (!creadoServidor)
                {
                    // Crear
                    Debug.Log("Creado cliente");
                    identificador = (int)UnityEngine.Random.Range(0, 10000);
                    OSCHandler.Instance.Init(IP, puerto,servidor); //init OSC
                    servers = new Dictionary<string, ServerLog>();
                    creadoServidor = true;
                }
                else
                {
                    //                Debug.Log("Cuantos clientes: " + OSCHandler.Instance.Clients.Count);
                    //                Debug.Log("CLiente: " + OSCHandler.Instance.Clients["SuperCollider"].client.ClientIPAddress);
                    Debug.Log("Envio mensaje");
                    OSCHandler.Instance.SendMessageToClient("SuperCollider", "/ID", identificador);
                    OSCHandler.Instance.SendMessageToClient("SuperCollider", "/mX", Input.mousePosition.normalized.x);
                    OSCHandler.Instance.SendMessageToClient("SuperCollider", "/mY", Input.mousePosition.normalized.y);
                    OSCHandler.Instance.SendMessageToClient("SuperCollider", "/aX", Input.acceleration.x);
                    OSCHandler.Instance.SendMessageToClient("SuperCollider", "/aY", Input.acceleration.y);
                    OSCHandler.Instance.SendMessageToClient("SuperCollider", "/aZ", Input.acceleration.z);

                    OSCHandler.Instance.UpdateLogs();
                    servers = OSCHandler.Instance.Servers;

                    foreach (KeyValuePair<string, ServerLog> item in servers)
                    {
                        // If we have received at least one packet,
                        // show the last received from the log in the Debug console
                        if (item.Value.log.Count > 0)
                        {
                            int lastPacketIndex = item.Value.packets.Count - 1;

                            UnityEngine.Debug.Log(String.Format("SERVER: {0} ADDRESS: {1} VALUE 0: {2}",
                                                                item.Key, // Server name
                                                                item.Value.packets[lastPacketIndex].Address, // OSC address
                                                                item.Value.packets[lastPacketIndex].Data[0].ToString())); //First data value
                        }
                    }

                }
            }
        }
        else
        {
            OSCHandler.Instance.UpdateLogs();
            servers = OSCHandler.Instance.Servers;

            foreach (KeyValuePair<string, ServerLog> item in servers)
            {
              //  Debug.Log("Num servidores: " + servers.Count);
                // If we have received at least one packet,
                // show the last received from the log in the Debug console
                
                //Debug.Log("Key: " + item.Key + " Log: " + item.Value.packets.Count);
                //for (int i = 0; i < item.Value.packets.Count;i++ ) {
                //    Debug.Log(item.Value.packets[i].Address);
                //    Debug.Log(item.Value.packets[i].Data[0].ToString());

                //}

                //if (item.Value.log.Count > 0)
                //{
                //    int lastPacketIndex = item.Value.packets.Count - 1;

                //    UnityEngine.Debug.Log(String.Format("SERVER: {0} ADDRESS: {1} VALUE 0: {2}",
                //                                        item.Key, // Server name
                //                                        item.Value.packets[lastPacketIndex].Address, // OSC address
                //                                        item.Value.packets[lastPacketIndex].Data[0].ToString())); //First data value
                //}
            }

        }
    }


    public void cambiarIP()
    {
        IP = textoObj.text;
        if (IP.Equals(""))
        {
            IP = GameObject.Find("Placeholder").GetComponent<Text>().text;
        }
        Debug.Log("IP aceptado: " + IP);

        ipCorrecta = true;
        //OSCHandler.Instance.Clients["SuperCollider"].client.ClientIPAddress = System.Net.IPAddress.Parse(IP);
        //OSCHandler.Instance.Init(IP, puerto); //init OSC
        //servers = new Dictionary<string, ServerLog>();
    }

    public void Restart()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
}