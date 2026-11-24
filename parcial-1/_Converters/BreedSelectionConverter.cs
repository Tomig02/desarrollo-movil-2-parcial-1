using parcial_1._Models;
using System.Globalization;

namespace parcial_1._Converters;

public class BreedSelectionConverter : IMultiValueConverter
{
    public object Convert(
        object[] values,
        Type targetType,
        object parameter,
        CultureInfo culture)
    {
        if (values.Length < 2 ||
            values[0] is not DogBreed breed ||
            values[1] is not DogBreed selected)
            return false;

        return breed.Id == selected.Id;
    }

    public object[] ConvertBack(
        object value,
        Type[] targetTypes,
        object parameter,
        CultureInfo culture)
        => throw new NotImplementedException();
}