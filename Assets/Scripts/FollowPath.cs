using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour
{
    Transform goal; //Goal from A* algorithm
    float speed = 5.0f;          //Velocidad del tanque
    float accuracy = 1.0f;		 //Distancia por debajo de la cual considero que he alcanzado el nodo
    float rotSpeed = 2.0f;       //Velocidad de rotacion del tanque
    public GameObject wpManager; //All the graph points and links
    GameObject[] wps;            //Nodos del grafo
    GameObject currentNode;		 //El nodo mas cercano en cada momento
    int currentWP = 0;           //Indice a lo largo del camino que seguimos, no tiene que ver con el numero/posicion de cada WP
    Graph g;                     //Grafo

    public int helipadIndex = 0;    // Indices dentro del array "waypoints" del GraphManager (0 = WP001, 1 = WP002...)
    public int ruinsIndex = 1;    // AJUSTA estos valores segun donde hayas colocado el helipuerto y las ruinas

    // Start is called before the first frame update
    void Start()
    {
        //Inicializar wps, g y currentNode
        GraphManager gm = wpManager.GetComponent<GraphManager>();
        wps = gm.waypoints;
        g = gm.graph;
        currentNode = wps[0];
    }

    public void GoToHeli()
    {
        if (g.AStar(currentNode, wps[helipadIndex]))
            currentWP = 0;
        g.printPath();
    }

    public void GoToRuin()
    {
        if (g.AStar(currentNode, wps[ruinsIndex]))
            currentWP = 0;
        g.printPath();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //Testear si no hay un camino definido (empty path) o currentWP esta ya al final del camino
        if (g.getPathLength() == 0 || currentWP >= g.getPathLength())
        {
            g.debugDraw();
            return;
        }
        //Guardamos en currentNode el nodo que tenemos mas cercano en este momento
        currentNode = GetClosestWP();
        //Si estamos suficientemente cerca al nodo currentWP, pasamos a buscar el siguiente
        if (Vector3.Distance(g.getPathPoint(currentWP).transform.position,
                             transform.position) < accuracy)
        {
            currentWP++;
        }
        //Si aun no hemos llegado al final del camino, nos dirigimos al siguiente nodo
        if (currentWP < g.getPathLength()) {
            //Guardamos en goal el nuevo nodo a visitar
            goal = g.getPathPoint(currentWP).transform;
            //Miramos hacia nuestro destino (goal)
            Vector3 lookAtGoal = new Vector3(goal.position.x, transform.position.y, goal.position.z);
            Vector3 direction = lookAtGoal - transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * rotSpeed);
            //Movemos el tanque
            transform.Translate(0, 0, speed * Time.deltaTime);
            }

            g.debugDraw();

    }

    // Devuelve el waypoint mas cercano al tanque
    GameObject GetClosestWP()
    {
        GameObject closest = wps[0];
        float minDist = Mathf.Infinity;

        foreach (GameObject wp in wps)
        {
            float d = Vector3.Distance(transform.position, wp.transform.position);
            if (d < minDist)
            {
                minDist = d;
                closest = wp;
            }
        }
        return closest;
    }
}
