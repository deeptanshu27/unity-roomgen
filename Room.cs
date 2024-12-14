using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Room
{
    public enum RoomType
    {
        BEDROOM,
        BATHROOM,
        HALL,
        KITCHEN,
        LIVINGROOM,
        STAIRCASE
    };

    public int length;
    public int width;
    public RoomType type;

    public Vector3 location;

    public Room() { }

    public Room(int length, int width)
    {
        this.length = length;
        this.width = width;
    }

    public Room(int length, int width, RoomType type) : this (length, width)
    {
        this.type = type;
    }
}
