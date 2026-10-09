
namespace parcial_1._Models;

public sealed class DogBreedDetails
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public int? LifeMin { get; set; }
    public int? LifeMax { get; set; }

    public int? MaleWeightMin { get; set; }
    public int? MaleWeightMax { get; set; }

    public int? FemaleWeightMin { get; set; }
    public int? FemaleWeightMax { get; set; }

    public int? MaleHeightMin { get; set; }
    public int? MaleHeightMax { get; set; }

    public int? FemaleHeightMin { get; set; }
    public int? FemaleHeightMax { get; set; }

    public bool? Hypoallergenic { get; set; }
    public string? OriginCountry { get; set; }
    public string? OriginRegion { get; set; }
}