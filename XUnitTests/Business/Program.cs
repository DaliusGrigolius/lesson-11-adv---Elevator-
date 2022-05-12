using Business.Services;
using Repository;
using Repository.DataAccess;
using System;

namespace Business
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Enter floors number: ");
            int floorsNumber = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter elevators number: ");
            int elevatorsNumber = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine($"Floors - {floorsNumber}");

            IBuildingRepo buildingRepo = new BuildingRepo(floorsNumber, elevatorsNumber);
            Logger _logger = new();
            ElevatorCalling calling = new();

            var currentBuilding = buildingRepo.GetBuilding();
            _logger.AddLogToFile($"Floors - {currentBuilding.Floors}\r\n", "log");
            calling.CallElevator(buildingRepo, 1, 1, 4, _logger);
        }
    }
}