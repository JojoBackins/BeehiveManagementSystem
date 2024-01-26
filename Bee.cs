using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeehiveManagementSystem
{
    abstract class Bee
    {
        public abstract float CostPerShift { get; }
        public string Job { get; private set; }
        public Bee(string job)
        {
            Job = job;
        }
        public void WorkTheNexthift()
        {
            if (HoneyVault.ConsumeHoney(CostPerShift))
            {
                DoJob();
            }
        }
        protected abstract void DoJob();
    }
    class NectarCollector : Bee
    {
        public const float NECTAR_COLLECTED_PER_SHIFT = 33.25f;
        public override float CostPerShift { get { return 1.95f; } }
        public NectarCollector() : base("Nectar Collector") { }
        protected override void DoJob()
        {
            HoneyVault.CollectNectar(NECTAR_COLLECTED_PER_SHIFT);
        }
    }
    class HoneyManufacturer : Bee
    {
        public const float NECTAR_PROCESSED_PER_SHIFT = 33.15f;
        public override float CostPerShift { get { return 1.7f; } }
        public HoneyManufacturer() : base("Honey Manufacturer") { }

        protected override void DoJob()
        {
            HoneyVault.ConvertNectarToHoney(NECTAR_PROCESSED_PER_SHIFT);
        }

    }
    class EggCare : Bee
    {
        public const float CARE_PROGRESS_PER_SHIFT = 0.15F;
        public override float CostPerShift { get { return 1.35f; } }
        private Queen queen;
        public EggCare(Queen queen) : base("Egg Care")
        {
            this.queen = queen;
        }
        protected override void DoJob()
        {
            queen.CareForEggs(CARE_PROGRESS_PER_SHIFT);
        }
    }
}
