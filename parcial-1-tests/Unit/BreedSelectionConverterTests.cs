using System.Globalization;
using parcial_1._Converters;
using parcial_1._Models;

namespace parcial_1_tests.Unit;

public class BreedSelectionConverterTests
{
    private readonly BreedSelectionConverter _converter = new();

    [Fact]
    public void Convert_ReturnsTrue_WhenBreedIsSelected()
    {
        var breed = new DogBreed { Id = "1", Name = "Akita" };

        var result = _converter.Convert(
            [breed, breed],
            typeof(bool),
            null!,
            CultureInfo.InvariantCulture);

        Assert.Equal(true, result);
    }

    [Fact]
    public void Convert_ReturnsFalse_WhenBreedIsDifferent()
    {
        var breed = new DogBreed { Id = "1", Name = "Akita" };
        var selected = new DogBreed { Id = "2", Name = "Poodle" };

        var result = _converter.Convert(
            [breed, selected],
            typeof(bool),
            null!,
            CultureInfo.InvariantCulture);

        Assert.Equal(false, result);
    }
}
