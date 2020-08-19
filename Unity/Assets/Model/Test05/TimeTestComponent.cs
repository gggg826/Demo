using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ETModel
{
    [ObjectSystem]
    public class TimeTestComponentAwakeSystem : AwakeSystem<TimeTestComponent>
    {
        public override void Awake(TimeTestComponent self)
        {
            self.Awake();
        }
    }

    public class TimeTestComponent : Component
    {
        private Entity parent;
        private readonly Dictionary<string, ITImeBehavior> Tbehaviors = new Dictionary<string, ITImeBehavior>();

        public void Awake()
        {
            this.parent = this.GetParent<Entity>();
            this.Load();
        }

        public void Run(string type, long time = 0)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            try
            {
                Tbehaviors[type].Behavior(parent, time);
            }
            catch (Exception e)
            {
                throw new Exception($"{type} Time Behavior 错误: {e}");
            }
            sw.Stop();
            TimeSpan ts2 = sw.Elapsed;
            Log.Info($"sw总共花费{ts2.TotalMilliseconds / 1000}");
        }

        public void Load()
        {
            this.Tbehaviors.Clear();
            List<Type> types = Game.EventSystem.GetTypes(typeof(TimeBehaviorAttribute));

            foreach (Type type in types)
            {
                object[] arrts = type.GetCustomAttributes(typeof(TimeBehaviorAttribute), false);
                if (arrts.Length == 0)
                {
                    continue;
                }

                TimeBehaviorAttribute attribute = arrts[0] as TimeBehaviorAttribute;
                if (Tbehaviors.ContainsKey(attribute.Type))
                {
                    Log.Debug($"已经存在同类Time Behavior: {attribute.Type}");
                    throw new Exception($"已经存在同类Time Behavior: {attribute.Type}");
                }

                object o = Activator.CreateInstance(type);
                ITImeBehavior behavior = o as ITImeBehavior;
                if (behavior != null)
                {
                    Tbehaviors.Add(attribute.Type, behavior);
                }
            }
        }
    }
}