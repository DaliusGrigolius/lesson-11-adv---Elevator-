using Business.Services;
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
            // TODO: 1. tryParse for inputs 2. callElevator params must be only variables from inputs
            Console.Write("Enter floors number: ");
            int floorsNumber = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter elevators number: ");
            int elevatorsNumber = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter how many floors you want to visit: ");
            int numberOfTravelPoints = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine($"Floors - {floorsNumber}");

            IBuildingRepo buildingRepo = new BuildingRepo(floorsNumber, elevatorsNumber);
            ILogger _logger = new Logger();
            IElevatorCalling calling = new ElevatorCalling();

            Building currentBuilding = buildingRepo.GetBuilding();
            _logger.AddLogToFile($"Floors - {currentBuilding.Floors}\r\n", "log");
            calling.CallElevator(currentBuilding, 1, 1, numberOfTravelPoints, _logger);
        }
    }
}