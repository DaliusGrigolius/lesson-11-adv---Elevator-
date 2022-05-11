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
            Console.WriteLine("Hello World!");

            Logger logger = new Logger();
            BuildingRepo buildingRepo = new BuildingRepo(12, 4);
            ElevatorCalling calling = new ElevatorCalling();
            calling.CallElevator(buildingRepo, 1, 1, logger);
        }
    }
}
