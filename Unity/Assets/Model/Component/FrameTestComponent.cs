using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETModel
{
    [ObjectSystem]
    public class FrameTestComponentUpdateSystem : UpdateSystem<FrameTestComponent>
    {
        public override void Update(FrameTestComponent self)
        {
            self.Update();
        }
    }

    public class FrameTestComponent:Component
    {
        private int m_Count = 0;
        private bool m_Interval = false;
        private int m_WaitTime = 1000;

        public void Update()
        {
            if(m_Interval)
            {
                return;
            }

            this.UpdateAsync().Coroutine();
        }

        private async ETVoid UpdateAsync()
        {
            Log.Info($"TestFrame  {m_Count}");

            m_Count++;
            m_Interval = true;

            await Game.Scene.GetComponent<TimerComponent>().WaitAsync(m_WaitTime);
            m_Interval = false;
        }

        public override void Dispose()
        {
            if(this.IsDisposed)
            {
                return;
            }
            base.Dispose();
        }
    }
}
