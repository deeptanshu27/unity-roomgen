using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;

public class InteriorBuilder : MonoBehaviour
{
    private int roomCount;
    private bool hasHall;

    private void Start()
    {
        roomCount =  (int) Random.Range(4, 7);
        hasHall   = Random.value < 0.5;

        initRooms();
    }

    private void initRooms()
    {
        List<Room> rooms      = new List<Room>();
        List<Room> rightRooms = new List<Room>();
        List<Room> leftRooms  = new List<Room>();

        // Generate hall
        Room hall = new Room(hasHall ? Random.Range(4, 10) : 4, 4);
        hall.location = new Vector3(0, -1, 0);

        // Generate rooms
        bool firstRoomPlaced = false;
        bool right = Random.value < 0.5;
        for (int _ = 0; _ < roomCount; _++)
        {
            int minArea = 16;
            int length  = Random.Range(3, 7);
            int width   = 0;
            do
            {
                width = Random.Range(3, 7);
            } while (length * width < minArea);

            Room r = new Room(length, width);
            rooms.Add(r);
            r.location = Vector3.one;

            float side = Random.value;

            bool top   = side < 0.75 && side > 0.25;
            //bool top = true;
            if (hasHall) right = side < 0.5;

            if (!firstRoomPlaced)
            {
                float x = (float) (hall.length + r.length) / 2;
                float z = (4 - (float)r.width) / 2 * (right ? 1 : -1); // bool can't convert to int???
                r.location = new Vector3(x, 0, z);
                if (width > 4)
                {
                    if (right)
                        rightRooms.Add(r);
                    else
                        leftRooms.Add(r);
                }
            }
            // bruh
            //else if (!firstRoomPlaced)
            //{
            //    float x = Random.Range((float)(hall.length - r.length) / 2, (float)(hall.length + r.length) / 2 - 1);
            //    float z = (4 - (float)r.width) / 2 * (right ? 1 : -1);
            //    r.location = new Vector3(x, 0, z);
            //}
            else
            {
                float x = 0;
                Room last;

                // TODO: What the fuck?
                if (!right)
                {
                    if (rightRooms.Count == 0)
                    {
                        x = Random.Range((float) (hall.length - r.length) / 2, (float) (hall.length + r.length) / 2 - 1);
                        Debug.Log(1);
                        rightRooms.Add(r);
                    }
                    else
                    {
                        if (!top)
                        {
                            last = rightRooms.Last();
                            x = last.location.x - (float) (last.length + r.length) / 2;
                            Debug.Log(2.1);
                            rightRooms.Add(r);
                        }
                        else
                        {
                            last = rightRooms.First();
                            x = last.location.x + (float)(last.length + r.length) / 2;
                            Debug.Log(2.2);
                            rightRooms.Insert(0, r);
                        }
                    }
                }
                else
                {
                    if (leftRooms.Count == 0)
                    {
                        x = Random.Range((float) (hall.length - r.length) / 2, (float) (hall.length + r.length) / 2 - 1);
                        Debug.Log(3);
                        leftRooms.Add(r);
                    }
                    else
                    {
                        if (!top)
                        {
                            last = leftRooms.Last();
                            x = last.location.x - (float)(last.length + r.length) / 2;
                            Debug.Log(4.1);
                            leftRooms.Add(r);
                        }
                        else
                        {
                            last = leftRooms.First();
                            x = last.location.x + (float)(last.length + r.length) / 2;
                            Debug.Log(4.2);
                            leftRooms.Insert(0, r);
                        }
                    }
                }
                float z = (4 + (float)r.width) / 2 * (right ? 1 : -1);
                r.location = new Vector3(x, 0, z);
            }

            firstRoomPlaced = true;
        }

        // Draw hall
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.localScale = new Vector3(hall.length, 1, hall.width);
        cube.transform.position = hall.location;

        Renderer renderer = cube.GetComponent<Renderer>();
        renderer.material.color = new Color(Random.value, Random.value, Random.value);
        
        foreach (Room r in rooms)
        {
            if (r.location == Vector3.one) continue;
            cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.localScale = new Vector3(r.length, 1, r.width);
            cube.transform.position = r.location;

            renderer = cube.GetComponent<Renderer>();
            renderer.material.color = new Color(Random.value, Random.value, Random.value);
        }
    }
}
