using System.Collections.Generic;
using UnityEngine;

public class ParryScript : MonoBehaviour
{
    private readonly List<Obstacle> obstaclesInZone = new List<Obstacle>();
    public float parryCooldown = 2f;
    private float lastParryTime = 0f;
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.LeftShift)) // change to your parry input
        //{
        //    if(Time.time - lastParryTime >= parryCooldown)
        //    {
        //        Parry();
        //        lastParryTime = Time.time;
        //    }
            
        //}
    }

    public void Parry()
    {
        // Loop backwards in case an obstacle removes itself when parried
        for (int i = obstaclesInZone.Count - 1; i >= 0; i--)
        {
            Obstacle obstacle = obstaclesInZone[i];
            if (obstacle == null) { obstaclesInZone.RemoveAt(i); continue; }

            if (obstacle.isParryable)
            {
                obstacle.Parry();
                // Add effects or feedback here
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Obstacle")) return;

        Obstacle obstacle = other.GetComponent<Obstacle>();
        if (obstacle != null && !obstaclesInZone.Contains(obstacle))
            obstaclesInZone.Add(obstacle);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Obstacle")) return;

        Obstacle obstacle = other.GetComponent<Obstacle>();
        if (obstacle != null)
            obstaclesInZone.Remove(obstacle);
    }
}