﻿using System;
using System.Collections.Generic;
using System.Globalization;
using Reversi.Logic.Rooms;

namespace Reversi.Logic.Lobbies
{
    public class Lobby
    {
        public List<Room> Rooms { get; private set; }
        public Lobby()
        {
            Rooms = new List<Room>();
        }

        public void AddRoom(string userId)
        {
            Rooms.Add(new Room(userId));            
        }

        public void JoinRoom(string userId, int roomIndex) 
        {
            Rooms[roomIndex].JoinRoom(userId);
        }
    }
}
