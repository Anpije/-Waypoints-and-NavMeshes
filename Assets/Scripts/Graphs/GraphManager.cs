using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Creamos una estructura para facilitar la entrada de datos en nuestro grafo
[System.Serializable]
public struct Link //Edge entre nodos
{
    public enum direction { UNI, BI }; //Edge de una direccion o bidireccional
    public GameObject node1;
    public GameObject node2;
    public direction dir;
}

public class GraphManager : MonoBehaviour
{
    public GameObject[] waypoints;    //Array de waypoints
    public Link[] links;              //Array de links
    public Graph graph = new Graph(); //Codigo para el grafo esta ya creado en la carpeta "Graphs"

    // Start is called before the first frame update
    void Start()
    {
        //Si wayPoints tiene nodos definidos (prevenir estructura vacia)
        if (waypoints.Length > 0) {
            //Iteramos para cada WP y lo guardamos en la estructura graph
            foreach (GameObject wp in waypoints)
            {
                graph.AddNode(wp);
            }

            //Iteramos para cada link y lo guardamos en la estructura graph
            foreach (Link l in links)
            {
                //node1 a node2
                graph.AddEdge(l.node1, l.node2);
                // node2 a node1 bidireccional
                if (l.dir == Link.direction.BI) graph.AddEdge(l.node2, l.node1);          
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        graph.debugDraw();
    }
}
