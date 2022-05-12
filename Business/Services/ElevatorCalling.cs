using Repository;
using Repository.DataAccess;
using Repository.Models;
using System;
using System.Threading;

namespace Business.Services
{
    public class ElevatorCalling
    {
        private int Counter { get; set; } = 0;

        public void CallElevator(IBuildingRepo buildingRepo, int myPosition, int elevatorId, int numberOfTravelPoints)
        {
            if (Counter == numberOfTravelPoints)
            {
                Console.WriteLine($"Elevator chilling @floor{myPosition}");
                Logger.AddLogToFile($"Elevator chilling @floor{myPosition}\r\n");
                return;
            }
            Counter++;

            Building currentBuilding = buildingRepo.GetBuilding();
            CheckParameters(myPosition, currentBuilding, elevatorId);

            Elevator currentElevator = currentBuilding.Elevators[elevatorId];
            int elevatorPosition = currentBuilding.Elevators[elevatorId].Floor;

            ElevatorInfo(elevatorPosition);

            ExecuteCallByPositions(myPosition, currentElevator, numberOfTravelPoints, currentBuilding, elevatorId, buildingRepo, numberOfTravelPoints);
        }

        private void ExecuteCallByPositions(int myPosition, Elevator currentElevator, int journeyTimes, Building currentBuilding, int elevatorId, IBuildingRepo building, int numberOfTravelPoints)
        {
            Console.WriteLine($"Elevator call @{DateTime.Now} to floor {myPosition}");
            Logger.AddLogToFile($"Elevator call @{DateTime.Now} to floor {myPosition}\r\n");

            ExecuteMovementAccordingToPositions(myPosition, currentElevator);

            currentElevator.Floor = myPosition;
            Random random = new();
            if (Counter != journeyTimes) myPosition = random.Next(1, currentBuilding.Floors + 1);

            CallElevator(building, myPosition, elevatorId, journeyTimes);
        }

        private static void ExecuteMovementAccordingToPositions(int myPosition, Elevator currentElevator)
        {
            if (myPosition < currentElevator.Floor)
            {
                currentElevator.Status = StatusAndDirection.MovingDown;
                for (int i = currentElevator.Floor; i >= myPosition; i--)
                {
                    Thread.Sleep(1000);
                    Console.WriteLine($"    Elevator @floor{ i } at @{DateTime.Now}");
                    Logger.AddLogToFile($"Elevator @floor{ i } at @{DateTime.Now}\r\n");
                }
                DoorOpenClose(currentElevator);
            }
            else if (myPosition > currentElevator.Floor)
            {
                currentElevator.Status = StatusAndDirection.MovingUp;
                for (int i = currentElevator.Floor; i <= myPosition; i++)
                {
                    Thread.Sleep(1000);
                    Console.WriteLine($"    Elevator @floor{ i } at @{DateTime.Now}");
                    Logger.AddLogToFile($"Elevator @floor{ i } at @{DateTime.Now}\r\n");
                }
                DoorOpenClose(currentElevator);
            }
            else
            {
                DoorOpenClose(currentElevator);
                currentElevator.Status = StatusAndDirection.Chilling;
            }
        }

        private static void CheckParameters(int myPosition, Building currentBuilding, int elevatorId)
        {
            if (myPosition > currentBuilding.Floors || myPosition < 1) throw new Exception("Calling position is not valid");
            if (!currentBuilding.Elevators.Exists(i => i.Id == elevatorId)) throw new Exception("No such elevator");
        }

        private static void ElevatorInfo(int elevatorPosition)
        {
            Console.WriteLine();
            Console.WriteLine($"Elevator starts at - {elevatorPosition}");
            Logger.AddLogToFile($"Elevator starts at - {elevatorPosition}\r\n");
        }

        private static void DoorOpenClose(Elevator currentElevator)
        {
            currentElevator.DoorStatus = Door.Opening;
            Console.WriteLine($"        Door opening {DateTime.Now}");
            Logger.AddLogToFile($"Door opening {DateTime.Now}\r\n");
            Thread.Sleep(2000);
            currentElevator.DoorStatus = Door.Open;
            Console.WriteLine($"        Door open {DateTime.Now}");
            Logger.AddLogToFile($"Door open {DateTime.Now}\r\n");

            Console.WriteLine($"        Door closing {DateTime.Now}");
            Logger.AddLogToFile($"Door closing {DateTime.Now}\r\n");
            currentElevator.DoorStatus = Door.Closing;
            Thread.Sleep(2000);
            Console.WriteLine($"        Door closed {DateTime.Now}");
            Logger.AddLogToFile($"Door closed {DateTime.Now}\r\n");
            currentElevator.DoorStatus = Door.Closed;
        }
    }
}