using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeehiveManagementSystem
{
    internal class Queen : Bee
    {
        public const float EGGS_PER_SHIFT = 0.45F;
        public const float HONEY_PER_UNASSINGNED_WORKER = 0.5F;

        private Bee[] workers = new Bee[0];
        private float eggs = 0;
        private float unassingnedWorkers = 3;

        public string StatusReport { get; private set; }
        public override float CostPerShift { get { return 2.15f; } }

        public Queen() : base("Queen")
        {
            AssignBee("Nectar Collector");
            AssignBee("Honey Manufacturer");
            AssignBee("Egg Care");
        }
        private void AddWorker(Bee worker)
        {
            if (unassingnedWorkers >= 1)
            {
                unassingnedWorkers--;
                Array.Resize(ref workers, workers.Length + 1);
                workers[workers.Length - 1] = worker;
            }
        }
        private void UpdateStatusReport()
        {
            StatusReport = $"Vault report:\n{HoneyVault.StatusReport}\n" +
                $"\nEgg count: {eggs:0.0}\nUnassingned workers: {unassingnedWorkers:0.0}\n" +
                $"{WorkerStatus("Nectar Collector")}\n{WorkerStatus("Honey Manufacturer")}" +
                $"\n{WorkerStatus("Egg Care")}\nTOTAL WORKERS: {workers.Length}";
        }
        public void CareForEggs(float eggsToConvert)
        {
            if (eggs >= eggsToConvert)
            {
                eggs -= eggsToConvert;
                unassingnedWorkers += eggsToConvert;
            }
        }

        private string WorkerStatus(string job)
        {
            int count = 0;
            foreach (Bee worker in workers)
                if (worker.Job == job) count++;
            string s = "s";
            if (count == 1) s = "";
            return $"{count} {job} bee{s}";
        }
        public void AssignBee(string job)
        {
            switch(job)
            {
                case "Nectar Collector":
                    AddWorker(new NectarCollector());
                    break;
                case "Honey Manufacturer":
                    AddWorker(new HoneyManufacturer());
                    break;
                case "Egg Care":
                    AddWorker(new EggCare(this));
                    break;
            }
            UpdateStatusReport();
        }
        protected override void DoJob()
        {
            eggs += EGGS_PER_SHIFT;
            foreach (Bee worker in workers)
            {
                worker.WorkTheNexthift();
            }
            HoneyVault.ConsumeHoney(unassingnedWorkers * HONEY_PER_UNASSINGNED_WORKER);
            UpdateStatusReport();
        }

    }
}
