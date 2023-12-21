using LendSum.MazeRunner.CLI.Clients;
using LendSum.MazeRunner.CLI.DTOs;
using LendSum.MazeRunner.CLI.Enums;
using LendSum.MazeRunner.CLI.Models;
using LendSum.MazeRunner.CLI.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace LendSum.MazeRunner.CLI
{
    public class App
    {
        private readonly IMazeRunnerRepository _mazeRunnerRepository;

        public App(IMazeRunnerRepository mazeRunnerRepository)
        {
            _mazeRunnerRepository = mazeRunnerRepository;
        }

        public async Task Run()
        {
            var creationRequest = new MazeCreationRequest()
            {
                Width = 5,
                Height = 5
            };

            var mazeCreationresponse = _mazeRunnerRepository.CreateMaze(creationRequest);

            if(mazeCreationresponse.MazeUid == null)
            {
                throw new Exception("An error ocurred creating the maze");
            }

            var mazeUid = mazeCreationresponse.MazeUid;
            Console.WriteLine($"New maze created with id: {mazeUid}");

            var mazeStartGameResponse = _mazeRunnerRepository.StartGame(mazeUid);

            if (mazeStartGameResponse.GameUid == null)
            {
                throw new Exception("An error ocurred starting the game");
            }

            var gameUid = mazeStartGameResponse.GameUid;
            Console.WriteLine($"New game started with id: {gameUid}");

            Console.WriteLine($"Starting maze");

            List<MazeCord> cords = new();
            List<string> path = new();
            bool result = SolveMaze(mazeUid, gameUid, cords, ref path);

            if(result)
            {
                Console.WriteLine("Maze has been completed, the path is");
                foreach( var direction in path)
                {
                    Console.WriteLine(direction);
                }
            }
            else 
            {
                Console.WriteLine("Maze has no solution");
            }

            Console.ReadLine();
        }

        private bool SolveMaze(string mazeUid, string gameUid, List<MazeCord> cords, ref List<string> path)
        {
            var status = _mazeRunnerRepository.Peek(mazeUid, gameUid);

            if(status.Game.Completed)
            {
                return true;
            }

            var currentCord = new MazeCord() { X = status.MazeBlockView.CoordX, Y = status.MazeBlockView.CoordY};
            cords.Add(currentCord);

            //Iterating though the 4 directions available
            for (int i = 1; i <= 4; i++)
            {
                var direction = ((GameOperations)i).ToString();

                if (IsValid(status, currentCord, direction, cords))
                {
                    path.Add(direction);
                    // Recursively call the FindPath function for the next cord
                    status = _mazeRunnerRepository.Move(mazeUid, gameUid, direction);
                    SolveMaze(mazeUid, gameUid, cords, ref path);
                    
                    // Remove the last direction when backtracking and go back
                    path.RemoveAt(path.Count - 1);
                }
            }

            cords.RemoveAt(cords.Count - 1);

            return false;
        }

        private GameOperationResponse GoBack(string mazeUid, string gameUid, string previousMove)
        {
            switch (previousMove)
            {
                case "GoNorth":
                    return _mazeRunnerRepository.MoveSouth(mazeUid, gameUid);
                case "GoEast":
                    return _mazeRunnerRepository.MoveWest(mazeUid, gameUid);
                case "GoSouth":
                    return _mazeRunnerRepository.MoveNorth(mazeUid, gameUid);
                default:
                    return _mazeRunnerRepository.MoveEast(mazeUid, gameUid);
            }
        }

        private static bool IsValid(GameOperationResponse status, MazeCord currentCord, string direction, List<MazeCord> cords)
        {
            if (!status.MazeBlockView.SouthBlocked && direction.Equals(GameOperations.GoSouth.ToString()) 
                && !cords.Any(c => c.X == currentCord.X && c.Y == currentCord.Y + 1))
                return true; 
            if (!status.MazeBlockView.EastBlocked && direction.Equals(GameOperations.GoEast.ToString()) 
                && !cords.Any(c => c.X == currentCord.X + 1 && c.Y == currentCord.Y))
                return true;
            if (!status.MazeBlockView.NorthBlocked && direction.Equals(GameOperations.GoNorth.ToString()) 
                && !cords.Any(c => c.X == currentCord.X && c.Y == currentCord.Y - 1))
                return true;
            if (!status.MazeBlockView.WestBlocked && direction.Equals(GameOperations.GoWest.ToString()) 
                && !cords.Any(c => c.X == currentCord.X - 1 && c.Y == currentCord.Y))
                return true;
            return false;
        }
    }
}
