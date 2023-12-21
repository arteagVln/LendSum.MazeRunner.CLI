using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LendSum.MazeRunner.CLI.DTOs
{
    public class MazeCreationResponse
    {
        public string MazeUid { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }
}
