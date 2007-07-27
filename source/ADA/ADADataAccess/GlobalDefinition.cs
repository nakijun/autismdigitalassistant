using System;
using System.Collections.Generic;
using System.Text;

namespace ADADataAccess
{
    public class Constants
    {
        public const int ALARM_REMINDER_ID = 1;
    };

    public enum ScheduleType
    {
        Normal,
        MiniSchedule,
        WorkSystemModel,
        WorkSystemInstance
    };
}
