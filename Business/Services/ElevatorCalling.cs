using Repository;
using Repository.Models;
using System;
using System.Threading;

namespace Business.Services
{
    public class ElevatorCalling : IElevatorCalling
    {
        public void CallElevator(Building currentBuilding, int myPosition, int elevatorId, int numberOfTravelPoints, ILogger _logger)
        {
            if (numberOfTravelPoints < 1)
            {
                ShowElevatorChillingMessage(myPosition, _logger);
                return;
            }
            numberOfTravelPoints--;

            CheckParameters(myPosition, currentBuilding, elevatorId);

            Elevator currentElevator = currentBuilding.Elevators[elevatorId];
            int elevatorPosition = currentBuilding.Elevators[elevatorId].Floor;

            ElevatorInfo(elevatorPosition, _logger);
            ExecuteCallByPositions(myPosition, currentElevator, numberOfTravelPoints, currentBuilding, elevatorId, currentBuilding, _logger);
        }

        private static void ShowElevatorChillingMessage(int myPosition, ILogger _logger)
        {
            Console.WriteLine($"Elevator chilling @floor{myPosition}");
            _logger.AddLogToFile($"Elevator chilling @floor{myPosition}\r\n", "log");
        }

        private void ExecuteCallByPositions(int myPosition, Elevator currentElevator, int numberOfTravelPoints, Building currentBuilding, int elevatorId, Building building, ILogger _logger)
        {
            Console.WriteLine($"Elevator call @{DateTime.Now} to floor {myPosition}");
            _logger.AddLogToFile($"Elevator call @{DateTime.Now} to floor {myPosition}\r\n", "log");

            ExecuteMovementAccordingToPositions(myPosition, currentElevator, _logger);

            currentElevator.Floor = myPosition;
            Random random = new();
            if (numberOfTravelPoints != 0) myPosition = random.Next(1, currentBuilding.Floors + 1);

            CallElevator(building, myPosition, elevatorId, numberOfTravelPoints, _logger);
        }

        private static void ExecuteMovementAccordingToPositions(int myPosition, Elevator currentElevator, ILogger _logger)
        {
            if (myPosition < currentElevator.Floor)
            {
                currentElevator.Status = StatusAndDirection.MovingDown;
                for (int i = currentElevator.Floor; i >= myPosition; i--) ExecuteMovement(_logger, i);
                DoorOpenClose(currentElevator, _logger);
            }
            else if (myPosition > currentElevator.Floor)
            {
                currentElevator.Status = StatusAndDirection.MovingUp;
                for (int i = currentElevator.Floor; i <= myPosition; i++) ExecuteMovement(_logger, i);
                DoorOpenClose(currentElevator, _logger);
            }
            else
            {
                DoorOpenClose(currentElevator, _logger);
                currentElevator.Status = StatusAndDirection.Chilling;
            }
        }

        private static void ExecuteMovement(ILogger _logger, int i)
        {
            Thread.Sleep(1000);
            Console.WriteLine($"    Elevator @floor{ i } at @{DateTime.Now}");
            _logger.AddLogToFile($"Elevator @floor{ i } at @{DateTime.Now}\r\n", "log");
        }

        private static void CheckParameters(int myPosition, Building currentBuilding, int elevatorId)
        {
            if (myPosition > currentBuilding.Floors || myPosition < 1) throw new Exception("Calling position is not valid");
            if (!currentBuilding.Elevators.Exists(i => i.Id == elevatorId)) throw new Exception("No such elevator");
        }

        private static void ElevatorInfo(int elevatorPosition, ILogger _logger)
        {
            Console.WriteLine();
            Console.WriteLine($"Elevator starts at - {elevatorPosition}");
            _logger.AddLogToFile($"Elevator starts at - {elevatorPosition}\r\n", "log");
        }

        private static void DoorOpenClose(Elevator currentElevator, ILogger _logger)
        {
            currentElevator.DoorStatus = Door.Opening;
            Console.WriteLine($"        Door opening {DateTime.Now}");
            _logger.AddLogToFile($"Door opening {DateTime.Now}\r\n", "log");
            Thread.Sleep(2000);
            currentElevator.DoorStatus = Door.Open;
            Console.WriteLine($"        Door open {DateTime.Now}");
            _logger.AddLogToFile($"Door open {DateTime.Now}\r\n", "log");

            Console.WriteLine($"        Door closing {DateTime.Now}");
            _logger.AddLogToFile($"Door closing {DateTime.Now}\r\n", "log");
            currentElevator.DoorStatus = Door.Closing;
            Thread.Sleep(2000);
            Console.WriteLine($"        Door closed {DateTime.Now}");
            _logger.AddLogToFile($"Door closed {DateTime.Now}\r\n", "log");
            currentElevator.DoorStatus = Door.Closed;
        }
    }
}