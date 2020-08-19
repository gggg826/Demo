using System.Threading;

namespace ETModel
{
    [TimeBehavior(Typebehavior.Waiting)]
    public class WaitingTimeBehavior : ITImeBehavior
    {
        public TestRoom room;
        public long waitTime;

        public void Behavior(Entity parent, long time)
        {
            room = parent as TestRoom;
            waitTime = time;
            Waiting().Coroutine();
        }

        public async ETVoid Waiting()
        {
            TimerComponent timer = Game.Scene.GetComponent<TimerComponent>();
            room.waitCts = new CancellationToken();

            await timer.WaitAsync(waitTime, room.waitCts);
            Log.Info($"{room.GetType().ToString()}-执行完waiting");
            //room.waitCts.Dispose();
            //room.waitCts = null;
        }
    }
}