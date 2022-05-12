using Repository.Models;
using System;
using System.Collections.Generic;

namespace Repository.DataAccess
{
    public class BuildingRepo : IBuildingRepo
    {
        private Building Building { get; set; }
        private List<Elevator> Elevators { get; set; }

        public BuildingRepo(int floorsNumber, int elevatorsNumber)
        {
            if (floorsNumber < 2 || elevatorsNumber < 2) throw new Exception("The number of floors of the building and the number of elevators can't be less than 2");

            Elevators = new List<Elevator>();
            for (int i = 0; i < elevatorsNumber; i++) Elevators.Add(new Elevator(i, 1, StatusAndDirection.Chilling, Door.Closed));

            Building = new Building(floorsNumber, Elevators);
        }

        public Building GetBuilding()
        {
            return Building;
        }
    }
}
