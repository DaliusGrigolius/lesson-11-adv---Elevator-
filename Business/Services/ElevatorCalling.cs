using Repository.DataAccess;
using Repository.Models;
using System;

namespace Business.Services
{
    public class ElevatorCalling
    {
        public void CallElevator(BuildingRepo building, int myPosition, int elevatorId)
        {
            Building currentBuilding = building.GetBuilding();
            if (myPosition > currentBuilding.Floors || myPosition < 1) throw new Exception("Calling position is not valid");
            
            bool elevatorExist = building.GetBuilding().Elevators.Exists(i => i.Id == elevatorId);
            if (elevatorExist)
            {
                int elevatorPosition = currentBuilding.Elevators[elevatorId].Floor;
                int elevatorStatus = (int)currentBuilding.Elevators[elevatorId].Status;
                int elevatorDoorStatus = (int)currentBuilding.Elevators[elevatorId].DoorStatus;
            }
        }
    }
}
