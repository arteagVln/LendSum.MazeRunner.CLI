using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LendSum.MazeRunner.CLI.DTOs
{
    public  class GameOperationResponse
    {
        public GameResponse Game { get; set; }
        public MazeBlockResponse MazeBlockView { get; set; }
    }
}
