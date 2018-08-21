using System;

namespace GarbageCalednar.TrashSkill
{
    public class TrashMonthInfo
    {
        public TrashType TrashType { get; set; }
        public DateTime Date { get; set; }

        public TrashMonthInfo(TrashType trashType, DateTime date)
        {
            TrashType = trashType;
            Date = date;
        }
    }
}