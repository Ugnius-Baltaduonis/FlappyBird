using Xunit;
using Xunit.Runners;
public class Tests
{
#pragma warning disable xUnit1013
    public static void RunTests()
    {
        string assemblyPath = typeof(Tests).Assembly.Location;
        using var runner = AssemblyRunner.WithoutAppDomain(assemblyPath);

        runner.OnTestPassed = info => Console.WriteLine($"Test {info.TestDisplayName} passed.");
        runner.OnTestFailed = info => Console.WriteLine($"Test {info.TestDisplayName} failed: {info.ExceptionMessage}");
        runner.OnExecutionComplete = info => Console.WriteLine("Test run complete.\nPress enter to continue");

        runner.Start();
        Console.WriteLine("Running tests...");
        Console.ReadLine();

        Console.Clear();
    }
#pragma warning restore xUnit1013

    [Fact]
    public void IsInBounds_ValidCoordinates_ReturnsTrue()
    {
        Display display = new Display(30, 12);
        Assert.True(display.IsInBounds(5, 5));
    }

    [Fact]
    public void IsInBounds_NegativeX_ReturnsFalse()
    {
        Display display = new Display(30, 12);
        Assert.False(display.IsInBounds(-1, 5));
    }

    [Fact]
    public void IsInBounds_NegativeY_ReturnsFalse()
    {
        Display display = new Display(30, 12);
        Assert.False(display.IsInBounds(5, -1));
    }

    [Fact]
    public void IsInBounds_XTooLarge_ReturnsFalse()
    {
        Display display = new Display(30, 12);
        Assert.False(display.IsInBounds(30, 5));
    }

    [Fact]
    public void IsInBounds_YTooLarge_ReturnsFalse()
    {
        Display display = new Display(30, 12);
        Assert.False(display.IsInBounds(5, 12));
    }

    [Fact]
    public void AddObject_PillarCreation_UpdatesMap()
    {
        Display display = new Display(30, 12);
        Pillar pillar = new Pillar(10, 12, 3);
        display.AddObject(pillar);
        Assert.True(display.IsObstacle(10, 0));
    }

    [Fact]
    public void Clear_ClearsCurrentMap()
    {
        Display display = new Display(30, 12);
        Pillar pillar = new Pillar(5, 12, 3);
        display.AddObject(pillar);
        display.Clear();
        Assert.False(display.IsObstacle(5, 5));
    }

    [Fact]
    public void Bird_ApplyGravity_IncreasesYByGravity()
    {
        Bird bird = new Bird(1, 5, 3);
        bird.SetPos(bird.GetX(), bird.GetY() + 1);
        Assert.Equal(6, bird.GetY());
    }

    [Fact]
    public void Bird_Flap_DecreasesYByFlapPower()
    {
        Bird bird = new Bird(1, 5, 3);
        bird.Flap();
        Assert.Equal(2, bird.GetY());
    }

    [Fact]
    public void Pillar_InitialPosition_IsCorrect()
    {
        Pillar pillar = new Pillar(10, 12, 3);
        Assert.Equal(10, pillar.GetPos());
    }

    [Fact]
    public void Pillar_Move_UpdatesPosition()
    {
        Pillar pillar = new Pillar(10, 12, 3);
        pillar.SetPos(9);
        Assert.Equal(9, pillar.GetPos());
    }
}