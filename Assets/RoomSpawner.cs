using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public int openingDirection;
    //1 - north
    //2 - east
    //3 - south
    //4 - west

    private RoomTemplates roomTemplates;
    private int random;
    private bool isSpawned = false;

    [SerializeField] private float waitTime = 4f;

    private void Start()
    {
        Destroy(gameObject, waitTime);
        roomTemplates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        Invoke("Spawn", 0.1f);
    }

    private void Spawn()
    {
        if(isSpawned == false)
        {
            if (openingDirection == 1)
            {
                //spawn north door
                random = Random.Range(0, roomTemplates.northRooms.Length);
                Instantiate(roomTemplates.northRooms[random], transform.position, Quaternion.identity);
            }
            else if (openingDirection == 2)
            {
                //spawn east door
                random = Random.Range(0, roomTemplates.eastRooms.Length);
                Instantiate(roomTemplates.eastRooms[random], transform.position, Quaternion.identity);
            }
            else if (openingDirection == 3)
            {
                //spawn south door
                random = Random.Range(0, roomTemplates.southRooms.Length);
                Instantiate(roomTemplates.southRooms[random], transform.position, Quaternion.identity);
            }
            else if (openingDirection == 4)
            {
                //spawn west door
                random = Random.Range(0, roomTemplates.westRooms.Length);
                Instantiate(roomTemplates.westRooms[random], transform.position, Quaternion.identity);
            }
            isSpawned = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Spawn Point"))
        {
            if (other.GetComponent<RoomSpawner>().isSpawned == false && isSpawned == false)
            {
                //spawn walls blocking off any openings
                Instantiate(roomTemplates.closedRoom, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        isSpawned = true;
        }
    }
}
