using Repository;
using Repository.DataAccess;
using Repository.Models;
using System;
using System.Threading;

namespace Business.Services
{
    public class ElevatorCalling
    {
        private int counter { get; set; } = 0;

        public void CallElevator(BuildingRepo building, int myPosition, int elevatorId, Logger logger, int journeyTimes)
        {
            CheckRecursiveRepeat(journeyTimes, logger, myPosition);
            Building currentBuilding = building.GetBuilding();
            CheckParameters(myPosition, currentBuilding, elevatorId);

            Elevator currentElevator = currentBuilding.Elevators[elevatorId];
            int elevatorPosition = currentBuilding.Elevators[elevatorId].Floor;

            ElevatorInfo(logger, elevatorPosition);
            ExecuteCallByPositions(elevatorPosition, myPosition, logger, currentElevator, journeyTimes, currentBuilding, elevatorId, building);
        }

        private void CheckRecursiveRepeat(int journeyTimes, Logger logger, int myPosition)
        {
            if (counter == journeyTimes)
            {
                Console.WriteLine($"Elevator chilling @floor{myPosition}");
                logger.AddLogToFile($"Elevator chilling @floor{myPosition}\r\n");
                return;
            }
            counter++;
        }

        private void ExecuteCallByPositions(int elevatorPosition, int myPosition, Logger logger, Elevator currentElevator, int journeyTimes, Building currentBuilding, int elevatorId, BuildingRepo building)
        {
            if (elevatorPosition == myPosition)
            {
                Console.WriteLine($"Elevator call @{DateTime.Now} to floor {myPosition}");
                logger.AddLogToFile($"Elevator call @{DateTime.Now} to floor {myPosition}\r\n");

                DoorOpenClose(currentElevator, logger);

                currentElevator.Status = StatusAndDirection.Chilling;
                Console.WriteLine($"Elevator chilling @floor{currentElevator.Floor}");
                logger.AddLogToFile($"Elevator chilling @floor{currentElevator.Floor}\r\n");
            }
            else
            {
                Random random = new Random();
                Console.WriteLine($"Elevator call @{DateTime.Now} to floor {myPosition}");
                logger.AddLogToFile($"Elevator call @{DateTime.Now} to floor {myPosition}\r\n");

                ExecuteMovementAccordingToPositions(myPosition, currentElevator, logger);

                currentElevator.Floor = myPosition;
                if (counter != journeyTimes - 1) myPosition = random.Next(1, currentBuilding.Floors + 1);

                CallElevator(building, myPosition, elevatorId, logger, journeyTimes);
            }
        }

        private void ExecuteMovementAccordingToPositions(int myPosition, Elevator currentElevator, Logger logger)
        {
            if (myPosition < currentElevator.Floor)
            {
                currentElevator.Status = StatusAndDirection.MovingDown;
                for (int i = currentElevator.Floor; i >= myPosition; i--)
                {
                    Thread.Sleep(1000);
                    Console.WriteLine($"    Elevator @floor{ i } at @{DateTime.Now}");
                    logger.AddLogToFile($"Elevator @floor{ i } at @{DateTime.Now}\r\n");
                }
                DoorOpenClose(currentElevator, logger);
            }
            else
            {
                currentElevator.Status = StatusAndDirection.MovingUp;
                for (int i = currentElevator.Floor; i <= myPosition; i++)
                {
                    Thread.Sleep(1000);
                    Console.WriteLine($"    Elevator @floor{ i } at @{DateTime.Now}");
                    logger.AddLogToFile($"Elevator @floor{ i } at @{DateTime.Now}\r\n");
                }
                DoorOpenClose(currentElevator, logger);
            }
        }

        private void CheckParameters(int myPosition, Building currentBuilding, int elevatorId)
        {
            if (myPosition > currentBuilding.Floors || myPosition < 1) throw new Exception("Calling position is not valid");
            if (!currentBuilding.Elevators.Exists(i => i.Id == elevatorId)) throw new Exception("No such elevator");
        }

        private void ElevatorInfo(Logger logger, int elevatorPosition)
        {
            Console.WriteLine();
            Console.WriteLine($"Elevator starts at - {elevatorPosition}");
            logger.AddLogToFile($"Elevator starts at - {elevatorPosition}\r\n");
        }

        private void DoorOpenClose(Elevator currentElevator, Logger logger)
        {
            currentElevator.DoorStatus = Door.Opening;
            Console.WriteLine($"        Door opening {DateTime.Now}");
            logger.AddLogToFile($"Door opening {DateTime.Now}\r\n");
            Thread.Sleep(2000);
            currentElevator.DoorStatus = Door.Open;
            Console.WriteLine($"        Door open {DateTime.Now}");
            logger.AddLogToFile($"Door open {DateTime.Now}\r\n");

            Console.WriteLine($"        Door closing {DateTime.Now}");
            logger.AddLogToFile($"Door closing {DateTime.Now}\r\n");
            currentElevator.DoorStatus = Door.Closing;
            Thread.Sleep(2000);
            Console.WriteLine($"        Door closed {DateTime.Now}");
            logger.AddLogToFile($"Door closed {DateTime.Now}\r\n");
            currentElevator.DoorStatus = Door.Closed;
        }
    }
}
