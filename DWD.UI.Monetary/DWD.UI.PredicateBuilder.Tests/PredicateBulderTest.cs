namespace DWD.UI.PredicateBuilder.Tests;

using System;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Xunit;

public class PredicateBulderTest
{
    [Fact]
    public void ShouldBuildQueryWithMultipleOrPredicates()
    {
        // Arrange
        var dbContext =
            new AnEntityContext(new DbContextOptionsBuilder<AnEntityContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options);

        _ = dbContext.Add(new AnEntity() { Id = 1, Fk = 1, P1 = 1, P2 = 1 });
        _ = dbContext.Add(new AnEntity() { Id = 2, Fk = 1, P1 = 1, P2 = 2 });
        _ = dbContext.Add(new AnEntity() { Id = 3, Fk = 1, P1 = 1, P2 = 3 });
        _ = dbContext.Add(new AnEntity() { Id = 4, Fk = 2, P1 = 1, P2 = 3 });

        _ = dbContext.SaveChanges();
        var allEntities = dbContext.AnEntities.ToList();
        Assert.True(4 == allEntities.Count, "Arrange error");

        var queryObjects = new Collection<AnEntityQueryObject>
            {
                new AnEntityQueryObject() {P1 = 1, P2 = 2},
                new AnEntityQueryObject() {P1 = 1, P2 = 3},
            };

        // Act
        var fkExpression = PredicateBuilder.Create<AnEntity>(p => p.Fk == 1);
        var orExpression = PredicateBuilder.False<AnEntity>();
        foreach (var queryObject in queryObjects)
        {
            orExpression = orExpression.Or(p => p.P1 == queryObject.P1 && p.P2 == queryObject.P2);
        }
        var result = dbContext.AnEntities.Where(fkExpression.And(orExpression)).ToList();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
        Assert.Equal(2, result[0].Id);
        Assert.Equal(3, result[1].Id);
    }
}
