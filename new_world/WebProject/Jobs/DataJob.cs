using Quartz;

namespace WebProject.Jobs
{
    public class DataJob : IJob
    {
        private readonly IServiceScopeFactory serviceScopeFactory;

        public DataJob(IServiceScopeFactory serviceScopeFactory)
        {
            this.serviceScopeFactory = serviceScopeFactory;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            using (var scope = serviceScopeFactory.CreateScope())
            {
                var landMiner = scope.ServiceProvider.GetService<LandMiner>();

                await landMiner.MineMoneyAsync();
            }
        }
    }
}
