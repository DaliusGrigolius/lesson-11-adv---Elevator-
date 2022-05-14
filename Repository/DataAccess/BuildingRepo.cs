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
            Elevators = new List<Elevator>();
            for (int i = 0; i < elevatorsNumber; i++)
            {
                Elevators.Add(new Elevator(i, 1, StatusAndDirection.Chilling, Door.Closed));
            }
            Building = new Building(floorsNumber, Elevators);
        }

        public Building GetBuilding()
        {
            return Building;
        }
    }
}
