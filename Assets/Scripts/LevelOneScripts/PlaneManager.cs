using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneManager : MonoBehaviour
{
    public GameObject plane;
    public Vector3 TargetplanePos;
    private float planeSpeed = 10f;
    private bool planeMove =  false;
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            planeMove = true;          
        }
    }
    private void PlaneMove()
    {
        if (planeMove == true)
        {
            plane.transform.position = Vector2.MoveTowards(plane.transform.position, new Vector2(TargetplanePos.x, TargetplanePos.y), planeSpeed * Time.deltaTime);
        }
    }
    private void Update()
    {
        PlaneMove();
    }
}
