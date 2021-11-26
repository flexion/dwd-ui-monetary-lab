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
            new();
    }
}
