using AutoFixture;
using AutoFixture.Xunit2;

namespace UnitTests;

internal class DateOnlyFixture
{
    public static Fixture Create()
    {
        var fixture = new Fixture();
        fixture.Customize<DateOnly>(composer => composer.FromFactory<DateTime>(DateOnly.FromDateTime));
        return fixture;
    }
}

internal class DateOnlyAutoDataAttribute : AutoDataAttribute
{
    public DateOnlyAutoDataAttribute() : base(DateOnlyFixture.Create) { }
}
