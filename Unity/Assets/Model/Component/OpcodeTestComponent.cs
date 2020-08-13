﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETModel
{
    [ObjectSystem]
    public class OpcodeComponetAwakeSystem : AwakeSystem<OpcodeTestComponent>
    {
        public override void Awake(OpcodeTestComponent self)
        {
            self.Awake();
        }
    }

    [ObjectSystem]
    public class OpcodeTestComponentLoadSystem : LoadSystem<OpcodeTestComponent>
    {
        public override void Load(OpcodeTestComponent self)
        {
            self.Load();
        }
    }


    public class OpcodeTestComponent:Component
    {
        public void Awake()
        {
            Log.Info("程序集初始，执行- OpcodeTest- Awake方法");
        }

        public void Load()
        {
            Log.Info("加载完成，执行- OpcodeTest- Load方法");
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
