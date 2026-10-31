using System;

namespace Filter_Plant
{
    public class MonthlyReport
    {
        public string monthName;
        public int totalEarning;
        public int electricCost;
        public int labourCost;
        public int petrolCost;
        public int otherCost;
        public int profit;

        public MonthlyReport(string month, int earn, int electric, int labour, int petrol, int others)
        {
            this.monthName = month;
            this.totalEarning = earn;
            this.electricCost = electric;
            this.labourCost = labour;
            this.petrolCost = petrol;
            this.otherCost = others;

            this.profit = earn - electric - labour - petrol - others;
        }
    }
}