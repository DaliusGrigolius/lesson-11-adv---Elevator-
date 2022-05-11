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
            int buildingHasFloors = 12;
            Console.WriteLine($"Floors - {buildingHasFloors}");

            BuildingRepo buildingRepo = new BuildingRepo(buildingHasFloors, 4);
            Logger logger = new Logger();
            ElevatorCalling calling = new ElevatorCalling();

            var currentBuilding = buildingRepo.GetBuilding();
            logger.AddLogToFile($"Floors - {currentBuilding.Floors}\r\n");
            calling.CallElevator(buildingRepo, 5, 1, logger, 5);
        }
    }
}
