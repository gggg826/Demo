using System.Collections.Generic;
using System.Threading;

namespace ETModel
{
    public sealed class TestRoom : Entity
    {
        public CancellationToken waitCts;
        public CancellationToken randCts;
        public Dictionary<int, string> gamers = new Dictionary<int, string>();
    }
}