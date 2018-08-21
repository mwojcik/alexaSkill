using System;
using System.Collections.Generic;

namespace GarbageCalednar.TrashSkill
{
    public static class TrashCalendar
    {
        public static IEnumerable<TrashMonthInfo> TrashMonthInfos = new List<TrashMonthInfo>
        {
            new TrashMonthInfo(TrashType.MetalAndPlastic,new DateTime(2018,08,30)),
            new TrashMonthInfo(TrashType.MetalAndPlastic,new DateTime(2018,09,13)),
            new TrashMonthInfo(TrashType.MetalAndPlastic,new DateTime(2018,09,27)),
            new TrashMonthInfo(TrashType.MetalAndPlastic,new DateTime(2018,10,11)),
            new TrashMonthInfo(TrashType.MetalAndPlastic,new DateTime(2018,10,15)),
            new TrashMonthInfo(TrashType.MetalAndPlastic,new DateTime(2018,11,8)),
            new TrashMonthInfo(TrashType.MetalAndPlastic,new DateTime(2018,11,22)),
            new TrashMonthInfo(TrashType.MetalAndPlastic,new DateTime(2018,12,6)),
            new TrashMonthInfo(TrashType.MetalAndPlastic,new DateTime(2018,12,20)),


            new TrashMonthInfo(TrashType.Glass,new DateTime(2018,08,16)),
            new TrashMonthInfo(TrashType.Glass,new DateTime(2018,09,27)),
            new TrashMonthInfo(TrashType.Glass,new DateTime(2018,10,25)),
            new TrashMonthInfo(TrashType.Glass,new DateTime(2018,11,22)),
            new TrashMonthInfo(TrashType.Glass,new DateTime(2018,12,20)),

            new TrashMonthInfo(TrashType.Paper,new DateTime(2018,08,23)),
            new TrashMonthInfo(TrashType.Paper,new DateTime(2018,09,6)),
            new TrashMonthInfo(TrashType.Paper,new DateTime(2018,09,20)),
            new TrashMonthInfo(TrashType.Paper,new DateTime(2018,10,4)),
            new TrashMonthInfo(TrashType.Paper,new DateTime(2018,10,18)),
            new TrashMonthInfo(TrashType.Paper,new DateTime(2018,11,3)),
            new TrashMonthInfo(TrashType.Paper,new DateTime(2018,11,15)),
            new TrashMonthInfo(TrashType.Paper,new DateTime(2018,11,29)),
            new TrashMonthInfo(TrashType.Paper,new DateTime(2018,12,13)),
            new TrashMonthInfo(TrashType.Paper,new DateTime(2018,12,27)),

        };
    }
}