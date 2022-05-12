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
            int floorsNumber = 12;
            int elevatorsNumber = 4;
            Console.WriteLine($"Floors - {floorsNumber}");

            IBuildingRepo buildingRepo = new BuildingRepo(floorsNumber, elevatorsNumber);
            ElevatorCalling calling = new();

            var currentBuilding = buildingRepo.GetBuilding();
            Logger.AddLogToFile($"Floors - {currentBuilding.Floors}\r\n");
            calling.CallElevator(buildingRepo, 12, 2, 5);
        }
    }
}