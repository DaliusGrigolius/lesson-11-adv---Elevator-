using System.Collections.Generic;

namespace Repository.Models
{
    public class Elevator
    {
        public int Id { get; }
        public int Floor { get; set; }
        public StatusAndDirection Status { get; set; }
        public Door DoorStatus { get; set; }

        public Elevator(int id, int floor, StatusAndDirection status, Door doorStatus)
        {
            Id = id;
            Floor = floor;
            Status = status;
            DoorStatus = doorStatus;
        }
    }
}
