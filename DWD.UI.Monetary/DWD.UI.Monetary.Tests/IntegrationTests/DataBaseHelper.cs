namespace DWD.UI.Monetary.Tests.IntegrationTests
{
    using System.Collections.Generic;
    using Service.Frameworks;
    using Service.Models.Stubs;

    public static class DataBaseHelper
    {
        public static void InitializeDbForTests(ClaimantWageContext db)
        {
            if (db != null)
            {
                db.ClaimantWages.AddRange(GetSeedingClaimantWages());
                db.SaveChanges();
            }
        }

        public static void ReinitializeDbForTests(ClaimantWageContext db)
        {
            if (db != null)
            {
                db.ClaimantWages.RemoveRange(db.ClaimantWages);
                InitializeDbForTests(db);
            }
        }

        private static List<ClaimantWage> GetSeedingClaimantWages() =>
         new()
         {
             new ClaimantWage
             {
                 ClaimantId = "12345",
                 WageYear = 2019,
                 WageQuarter = 2,
                 TotalWages = 150
             },
             new ClaimantWage
             {
                 ClaimantId = "12346",
                 WageYear = 2019,
                 WageQuarter = 4,
                 TotalWages = 250
             }
         };
    }
}
