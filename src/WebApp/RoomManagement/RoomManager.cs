using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomManagement
{
    public class RoomManager
    {
        public List<Room> GetRooms(int lowBorder, int upBorder)
        {
            using (var context = new RoomContext())
            {
                return context.Rooms.Where(r => r.Id >= lowBorder && r.Id <= upBorder).ToList();
            }
        }
    }
}
