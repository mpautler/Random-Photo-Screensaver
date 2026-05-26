namespace RPS.Tests;

/// <summary>
/// Basic smoke tests to verify RPS test infrastructure is working
/// </summary>
public class SmokeTests
{
    [Fact]
    public void TestInfrastructure_IsWorking()
    {
        // Verify test infrastructure is functional
        Assert.True(true, "Test infrastructure is operational");
    }

    [Fact]
    public void RPS_Assembly_IsReferenced()
    {
        // Verify we can load the RPS assembly
        var assembly = typeof(RPS.Screensaver).Assembly;
        Assert.NotNull(assembly);
        Assert.Contains("RPS4", assembly.FullName);
    }
}
