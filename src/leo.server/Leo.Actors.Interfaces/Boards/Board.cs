using System;
using System.Collections.Generic;

namespace Leo.Actors.Interfaces.Boards
{
    public class Board
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public DateTime CreatedOn { get; set; }

        public decimal SpendableBalance { get; set; }
    }
}