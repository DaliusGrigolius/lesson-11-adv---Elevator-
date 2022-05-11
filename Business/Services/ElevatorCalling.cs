using Repository;
using Repository.DataAccess;
using Repository.Models;
using System;
using System.Threading;

namespace Business.Services
{
    public class ElevatorCalling
    {
        public void CallElevator(BuildingRepo building, int myPosition, int elevatorId, Logger logger)
        {
            Building currentBuilding = building.GetBuilding();
            if (myPosition > currentBuilding.Floors || myPosition < 1) throw new Exception("Calling position is not valid");
            if (!currentBuilding.Elevators.Exists(i => i.Id == elevatorId)) throw new Exception("No such elevator");
            Elevator currentElevator = currentBuilding.Elevators[elevatorId];
            currentElevator.Calls.Add(myPosition);

            int elevatorPosition = currentBuilding.Elevators[elevatorId].Floor;
            int elevatorStatus = (int)currentBuilding.Elevators[elevatorId].Status;
            int elevatorDoorStatus = (int)currentBuilding.Elevators[elevatorId].DoorStatus;

            if (elevatorPosition == myPosition)
            {
                Console.WriteLine($"Input floors - {currentBuilding.Floors}\r\n");
                logger.AddLogToFile($"Input floors - {currentBuilding.Floors}\r\n");

                Console.WriteLine($"Elevator starts at - {elevatorPosition}\r\n");
                logger.AddLogToFile($"Elevator starts at - {elevatorPosition}\r\n");

                Console.WriteLine($"Elevator call @{DateTime.Now} to floor {myPosition}\r\n");
                logger.AddLogToFile($"Elevator call @{DateTime.Now} to floor {myPosition}\r\n");
                Thread.Sleep(2000);
                Console.WriteLine($"Door opening {DateTime.Now}\r\n");
                logger.AddLogToFile($"Door opening {DateTime.Now}\r\n");
                currentElevator.DoorStatus = Door.Opening;
                Thread.Sleep(2000);
                currentElevator.DoorStatus = Door.Open;

                Console.WriteLine($"Door closing {DateTime.Now}\r\n");
                logger.AddLogToFile($"Door closing {DateTime.Now}\r\n");
                currentElevator.DoorStatus = Door.Closing;
                Thread.Sleep(2000);
                currentElevator.DoorStatus = Door.Closed;
            }

            //if (elevatorPosition != myPosition)
            //{
            //    currentElevator.Status = (int)StatusAndDirection.Moving;
            //    int floorsDiff = currentBuilding.Floors - myPosition;
            //    for (int i = 0; i < floorsDiff; i++)
            //    {
            //        Thread.Sleep(1000);
            //        // register movement: "Elevator @floor2 at @19.01.02"
            //    }
            //}

        }
    }
}
