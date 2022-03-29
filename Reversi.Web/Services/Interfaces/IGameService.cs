using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reversi.Web.Services.Interfaces
{
    public interface IGameService
    {
        public void PlaceCounter(int row, int col);
    }
}
