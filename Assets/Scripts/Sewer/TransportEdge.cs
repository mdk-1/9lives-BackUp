using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

namespace Spawner
{
    public class TransportEdge : MonoBehaviour
    {
        [SerializeField, Tooltip("Assign to Spawn Points")]
        private GameObject[] spawnPoints = null;
        [SerializeField]
        private GameObject[] bounds = null;
        [SerializeField]
        private float distanceL = 0;
        [SerializeField]
        private float distanceR = 0;
        [SerializeField]
        private float setDistance = 0;


        [SerializeField, Tooltip("Which SpawnPoint Object will Spawn to")]
        public int spawnIndex = 0;
        private void Start()
        {
            spawnIndex = Random.Range(0, spawnPoints.Length);
            distanceL= Vector2.Distance(transform.position, bounds[0].transform.position);
            distanceR = Vector2.Distance(transform.position, bounds[1].transform.position);
        }
        private void Update()
        {
            distanceL = Vector2.Distance(transform.position, bounds[0].transform.position);
            distanceR = Vector2.Distance(transform.position, bounds[1].transform.position);
            if (distanceL<=setDistance||distanceR<=setDistance)
            {
                transform.position = spawnPoints[spawnIndex].transform.position;
                spawnIndex = Random.Range(0, spawnPoints.Length);
            }
        }

        /// <summary>
        /// When Object Collides with Collider object spawns to random spawn point
        /// </summary>
        /// <param name="collision"></param>
        //private void OnTriggerEnter2D(Collider2D boundC)
        //{
        //    if (boundC.CompareTag("Bound"))
        //    {
        //        transform.position = spawnPoints[spawnIndex].transform.position;
        //        spawnIndex = Random.Range(0, spawnPoints.Length); 
        //    }
            

        //}
    }
}
