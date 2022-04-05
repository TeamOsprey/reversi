using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Reversi.Logic;

namespace Reversi.Web.Services.Interfaces
{
    public interface IGameService
    {
        public void PlaceCounter(int row, int col);
        public string[] GetOutput();
    }
}
