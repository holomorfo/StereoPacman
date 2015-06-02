using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectManager : MonoBehaviour
{

    public GameObject cuadritoPrefab;
    //GameObject go;
    public List<object> valores;
    public List<int> listaJugadores;
    public List<GameObject> listaObjetosCuadritos;
    // Use this for initialization
    void Start()
    {
        valores = new List<object>();
        listaJugadores = new List<int>();
        listaObjetosCuadritos = new List<GameObject>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        actualizar();
        //           actualizarOSC();
    }

    public void actualizar()
    {
        //   Debug.Log("SizeV2 " + valores.Count);
        if (valores.Count > 0)
        {
            int cuantos = valores.Count;
           // Debug.Log("Cuantos van " + cuantos);
            for (int i = 0; i < cuantos; i++)
            {
                List<object> cosa = valores[valores.Count - 1] as List<object>;
             //   Debug.Log("Recibi: " + cosa[0].ToString() + " " + cosa[1].ToString() + " " + cosa[2].ToString());
                // AQUI SE INSTANCIAN LOS CUADRITOS
                if (listaJugadores.IndexOf((int)cosa[0]) == -1)
                {
                    // Agregar a la lista de jugadores y crear objeto
                    listaJugadores.Add((int)cosa[0]);
                    GameObject g = Instantiate(cuadritoPrefab, new Vector3(0, 0, 38.0f), Quaternion.identity) as GameObject;
                    g.GetComponent<InicializarAudio>().id = (int)cosa[0];
                    listaObjetosCuadritos.Add(g);
                }
                else
                {
                    int idx = listaJugadores.IndexOf((int)cosa[0]);
                    float px = (float)cosa[1] - 0.5f;
                    float py = (float)cosa[2] - 0.5f;
                    listaObjetosCuadritos[idx].GetComponent<ColisionesPacmans>().grabarUltimaVelocidad(px,py);
                }
            }
            for (int i = 0; i < cuantos; i++)
            {
                valores.RemoveAt(0);
            }
        }
    }

    //public void actualizarOSC()
    //{
    //    // Debo vaciar el buffer en cada paso... pero no se como hacerlo
    //    // Correr sobre los que van hasta el momento
    //    // El corte se hará con el número de elementos
    //    int cuantos = valores.Count;
    //    //Debug.Log("Cuantos van " + cuantos);
    //    for (int i = 0; i < cuantos; i++)
    //    {
    //        // aqui va corriendo sobre todos estos
    //        List<object> cosa = valores[i] as List<object>;
    //      //  Debug.Log("Recibi: " + cosa[0].ToString() + " " + cosa[1].ToString() + " " + cosa[2].ToString());
    //        // AQUI SE INSTANCIAN LOS CUADRITOS
    //        //Debug.Log("Tipo " + cosa[0].GetType() + " " + cosa[0]);
    //        if (listaJugadores.IndexOf((int)cosa[0]) == -1)
    //        {
    //            // Agregar a la lista de jugadores y crear objeto
    //            listaJugadores.Add((int)cosa[0]);
    //            GameObject g = Instantiate(cuadritoPrefab, new Vector3(UnityEngine.Random.Range(-0.0f, 100.0f), UnityEngine.Random.Range(-0.0f, 100.0f), 0), Quaternion.identity) as GameObject;
    //            g.GetComponent<InicializarAudio>().id = (int)cosa[0];
    //            listaObjetosCuadritos.Add(g);

    //        }
    //        else
    //        {
    //            int idx = listaJugadores.IndexOf((int)cosa[0]);
    //            float px = (float)cosa[1] - 0.5f;
    //            float py = (float)cosa[2] - 0.5f;
    //            listaObjetosCuadritos[idx].transform.position += new Vector3(px * 2, py * 2, 0.0f);
    //        }
    //    }
    //    for (int i = 0; i < cuantos; i++)
    //    {
    //        valores.RemoveAt(0);
    //    }
    //    Debug.Log("Quité " + cuantos);
    //    // Al final quitar todos estos
    //}
}
