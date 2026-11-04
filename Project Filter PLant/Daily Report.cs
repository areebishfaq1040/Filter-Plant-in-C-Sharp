using System;

namespace Project_Filter_Plant
{
    public class DailyReport
    {
        public string date;
        public int totalCanes;
        public int paidCanes;
        public int labour1;
        public int labour2;
        public int petrol;
        public int earning;
        public int remaining;

        public DailyReport(string date, int totalCanes, int paidCanes, int l1, int l2, int petrol)
        {
            this.date = date;
            this.totalCanes = totalCanes;
            this.paidCanes = paidCanes;
            this.labour1 = l1;
            this.labour2 = l2;
            this.petrol = petrol;

            this.earning = paidCanes * 200;
            this.remaining = this.earning - l1 - l2 - petrol;
        }
    }
}