using LendSum.MazeRunner.CLI.Clients;
using LendSum.MazeRunner.CLI.DTOs;
using LendSum.MazeRunner.CLI.Enums;

namespace LendSum.MazeRunner.CLI.Repository
{
    public class MazeRunnerRepository : IMazeRunnerRepository
    {
        private readonly IMazeRunnerClient _mazeRunnerClient;
        private static readonly string apiCode = "CTLH2JGw02ntEMlwXANzIegaNFGi/vSE34NSvgar5WYFb1x349z8jw==";
        private GameOperationRequest _gameOperation;

        public MazeRunnerRepository(IMazeRunnerClient mazeRunnerClient)
        {
            _mazeRunnerClient = mazeRunnerClient;
            _gameOperation = new GameOperationRequest()
            {
                Operation = "Start"
            };
        }

        public MazeCreationResponse CreateMaze(MazeCreationRequest requestBody)
        {
            return _mazeRunnerClient.CreateMaze(apiCode, requestBody).Result;
        }

        public GameResponse StartGame(string mazeUid)
        {
            return _mazeRunnerClient.StartGame(mazeUid, apiCode, _gameOperation).Result;
        }

        public GameOperationResponse Peek(string mazeUid, string gameUid)
        {
            return _mazeRunnerClient.GetStatus(mazeUid, gameUid, apiCode).Result;
        }

        public GameOperationResponse Move(string mazeUid, string gameUid, string operation)
        {
            _gameOperation.Operation = operation;
            return _mazeRunnerClient.GameOperation(mazeUid, gameUid, apiCode, _gameOperation).Result;
        }

        public GameOperationResponse MoveNorth(string mazeUid, string gameUid)
        {
            _gameOperation.Operation = GameOperations.GoNorth.ToString();
            return _mazeRunnerClient.GameOperation(mazeUid, gameUid, apiCode, _gameOperation).Result;
        }

        public GameOperationResponse MoveSouth(string mazeUid, string gameUid)
        {
            _gameOperation.Operation = GameOperations.GoSouth.ToString();
            return _mazeRunnerClient.GameOperation(mazeUid, gameUid, apiCode, _gameOperation).Result;
        }

        public GameOperationResponse MoveEast(string mazeUid, string gameUid)
        {
            _gameOperation.Operation = GameOperations.GoEast.ToString();
            return _mazeRunnerClient.GameOperation(mazeUid, gameUid, apiCode, _gameOperation).Result;
        }

        public GameOperationResponse MoveWest(string mazeUid, string gameUid)
        {
            _gameOperation.Operation = GameOperations.GoWest.ToString();
            return _mazeRunnerClient.GameOperation(mazeUid, gameUid, apiCode, _gameOperation).Result;
        }
    }
}
