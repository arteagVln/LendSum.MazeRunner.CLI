using LendSum.MazeRunner.CLI.DTOs;
using LendSum.MazeRunner.CLI.Enums;
using Refit;

namespace LendSum.MazeRunner.CLI.Clients
{
    public interface IMazeRunnerClient
    {
        [Post("/Maze")]
        Task<MazeCreationResponse> CreateMaze(string code, [Body] MazeCreationRequest requestBody);

        [Post("/Game/{mazeUid}")]
        Task<GameResponse> StartGame(string mazeUid, string code, [Body] GameOperationRequest Operation);

        [Get("/Game/{mazeUid}/{gameUid}")]
        Task<GameOperationResponse> GetStatus(string mazeUid, string gameUid, string code);

        [Post("/Game/{mazeUid}/{gameUid}")]
        Task<GameOperationResponse> GameOperation(string mazeUid, string gameUid, string code, [Body] GameOperationRequest Operation);
    }
}
