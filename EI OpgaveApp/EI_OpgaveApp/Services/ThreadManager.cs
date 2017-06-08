using EI_OpgaveApp.Synchronizers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EI_OpgaveApp.Services
{
    public class ThreadManager
    {
        SynchronizerFacade facade = SynchronizerFacade.GetInstance;

        public async void StartSynchronizationThread()
        {

            int i = 0;
            bool done = false;
            while (!done)
            {
                await Task.Run(async () =>
                {
                    await facade.MaintenanceTaskSynchronizer.SyncDatabaseWithNAV();
                    await facade.TimeRegistrationSynchronizer.SyncDatabaseWithNAV();
                    await facade.MaintenanceActivitySynchronizer.SyncDatabaseWithNAV();
                    facade.JobRecLineSynchronizer.SyncDatabaseWithNAV();
                    facade.PictureSynchronizer.PutPicturesToNAV();
                    facade.ResourcesSynchronizer.SyncDatabaseWithNAV();
                    facade.CustomerSynchronizer.SyncDatabaseWithNAV();
                    facade.JobSynchronizer.SyncDatabaseWithNAV();
                    facade.JobTaskSynchronizer.SyncDatabaseWithNAV();
                    Debug.WriteLine(i + "!!!!!!! SYNCED!!!!!");
                    i++;
                });
                await Task.Delay(30000);
            }
        }
    }
}
