using System;
using System.Collections.Generic;
using System.Text;
using TreloDAL.Models;

namespace TreloBLL.Interfaces
{
    public interface IChangeTrackingService
    {
        void TrackChangeGeneric<TrackingEntity, LogEntity>(TrackingEntity newEntity, int entityId) where TrackingEntity : class where LogEntity : class;
        void TrackChange(UserTask updateEntity, int taskId);


    }
}
