using LendSum.MazeRunner.CLI.DTOs;

namespace LendSum.MazeRunner.CLI.Repository
{
    public interface IMazeRunnerRepository
    {
        MazeCreationResponse CreateMaze(MazeCreationRequest requestBody);
        GameOperationResponse Move(string mazeUid, string gameUid, string operation);
        GameOperationResponse MoveEast(string mazeUid, string gameUid);
        GameOperationResponse MoveNorth(string mazeUid, string gameUid);
        GameOperationResponse MoveSouth(string mazeUid, string gameUid);
        GameOperationResponse MoveWest(string mazeUid, string gameUid);
        GameOperationResponse Peek(string mazeUid, string gameUid);
        GameResponse StartGame(string mazeUid);
    }
}