using Repository;
using Repository.Models;

namespace Business.Services
{
    public interface IElevatorCalling
    {
        void CallElevator(Building currentBuilding, int myPosition, int elevatorId, int numberOfTravelPoints, ILogger _logger);
    }
}