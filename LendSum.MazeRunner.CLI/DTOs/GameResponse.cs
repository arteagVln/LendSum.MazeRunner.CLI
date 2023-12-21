using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LendSum.MazeRunner.CLI.DTOs
{
    public class GameResponse
    {
        public string MazeUid { get; set; }
        public string GameUid { get; set;}
        public bool Completed { get; set; }
        public int CurrentPositionX { get; set; }
        public int CurrentPositionY { get; set; }
    }
}
