﻿using Business.Services;
using Repository;
using Repository.DataAccess;
using Repository.Models;
using System;

namespace Business
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            int floorsNumber = GetFloorsNumber();
            int elevatorsNumber = GetElevatorsNumber();
            int numberOfTravelPoints = GetTravelPointsNumber(floorsNumber);
            int myPosition = GetMyPosition(floorsNumber);
            int elevatorId = GetElevatorId(elevatorsNumber);

            Console.WriteLine($"Floors - {floorsNumber}");

            IBuildingRepo buildingRepo = new BuildingRepo(floorsNumber, elevatorsNumber);
            ILogger _logger = new Logger();
            IElevatorCalling calling = new ElevatorCalling();

            Building currentBuilding = buildingRepo.GetBuilding();
            _logger.AddLogToFile($"Floors - {currentBuilding.Floors}\r\n", "log");
            calling.CallElevator(currentBuilding, myPosition, elevatorId, numberOfTravelPoints, _logger);
        }

        private static int GetFloorsNumber()
        {
            int floorsNumber;

            Console.Write("Enter a number of floors please: ");
            bool floorsNumberIsValid = int.TryParse(Convert.ToString(Console.ReadLine()), out floorsNumber);
            while (!floorsNumberIsValid || (floorsNumber < 2 || floorsNumber > 30))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("----------------------------------------");
                Console.ResetColor();
                Console.Write("Enter a number of floors from 2 to 30 please: ");
                floorsNumberIsValid = int.TryParse(Convert.ToString(Console.ReadLine()), out floorsNumber);
            }

            return floorsNumber;
        }

        private static int GetElevatorsNumber()
        {
            int elevatorsNumber;

            ChangeConsoleColorToGreen();
            Console.Write("Enter the number of elevators please: ");
            bool elevatorsNumberIsValid = int.TryParse(Convert.ToString(Console.ReadLine()), out elevatorsNumber);
            while (!elevatorsNumberIsValid || (elevatorsNumber < 2 || elevatorsNumber > 10))
            {
                ChangeConsoleColorToRed();
                Console.Write("Enter the number of elevators from 2 to 10 please: ");
                elevatorsNumberIsValid = int.TryParse(Convert.ToString(Console.ReadLine()), out elevatorsNumber);
            }

            return elevatorsNumber;
        }

        private static int GetTravelPointsNumber(int floorsNumber)
        {
            int numberOfTravelPoints;

            ChangeConsoleColorToGreen();
            Console.Write("Enter how many floors you want to visit please: ");
            bool numberOfTravelPointsIsValid = int.TryParse(Convert.ToString(Console.ReadLine()), out numberOfTravelPoints);
            while (!numberOfTravelPointsIsValid || (numberOfTravelPoints < 1 || numberOfTravelPoints > 10))
            {
                ChangeConsoleColorToRed();
                Console.Write($"Enter how many floors you want to visit from 1 to {floorsNumber} please: ");
                numberOfTravelPointsIsValid = int.TryParse(Convert.ToString(Console.ReadLine()), out numberOfTravelPoints);
            }

            return numberOfTravelPoints;
        }

        private static int GetMyPosition(int floorsNumber)
        {
            int myPosition;

            ChangeConsoleColorToGreen();
            Console.Write("Enter your starting position in building please: ");
            bool myPositionIsValid = int.TryParse(Convert.ToString(Console.ReadLine()), out myPosition);
            while (!myPositionIsValid || (myPosition < 1 || myPosition > floorsNumber))
            {
                ChangeConsoleColorToRed();
                Console.Write($"Enter your starting position in building from 1 to {floorsNumber} please: ");
                myPositionIsValid = int.TryParse(Convert.ToString(Console.ReadLine()), out myPosition);
            }

            return myPosition;
        }

        private static int GetElevatorId(int elevatorsNumber)
        {
            int elevatorId;

            ChangeConsoleColorToGreen();
            Console.Write("Enter elevator number(id) please: ");
            bool elevatorIdIsValid = int.TryParse(Convert.ToString(Console.ReadLine()), out elevatorId);
            while (!elevatorIdIsValid || (elevatorId - 1 < 0 || elevatorId >= elevatorsNumber))
            {
                ChangeConsoleColorToRed();
                Console.Write($"Enter elevator number(id) from 1 to {elevatorsNumber} please: ");
                elevatorIdIsValid = int.TryParse(Convert.ToString(Console.ReadLine()), out elevatorId);
            }
            Console.WriteLine();

            return elevatorId;
        }

        private static void ChangeConsoleColorToGreen()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("----------------------------------------");
            Console.ResetColor();
        }

        private static void ChangeConsoleColorToRed()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("----------------------------------------");
            Console.ResetColor();
        }
    }
}