namespace Repository.Models
{
    public class Elevator
    {
        public int Floor { get; set; }
        public StatusAndDirection Status { get; set; }
        public Door DoorStatus { get; set; }

        public Elevator(int floor, StatusAndDirection status, Door doorStatus)
        {
            Floor = floor;
            Status = status;
            DoorStatus = doorStatus;
        }
    }
}
