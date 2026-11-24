using System.Text.Json;
using System.Text.Json.Serialization;
using parcial_1._Models;

namespace parcial_1._Services;

public sealed class DogApiService
{
    private readonly HttpClient _httpClient;

    public DogApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IReadOnlyList<DogBreed>> GetBreedsAsync( CancellationToken cancellationToken = default)
    {
        const string url = "https://dogapi.dog/api/v2/breeds?page%5Bsize%5D=1000";

        using HttpResponseMessage response = await _httpClient.GetAsync(url, cancellationToken);

        response.EnsureSuccessStatusCode();

        await using Stream stream = await response.Content.ReadAsStreamAsync(cancellationToken);

        var result = await JsonSerializer.DeserializeAsync<BreedsResponse>(
            stream,
            cancellationToken: cancellationToken
        );

        return result?.Data?
            .Where(item => item.Attributes?.Name is not null)
            .Select(item => new DogBreed {
                    Id = item.Id,
                    Name = item.Attributes!.Name!
                }
            )
            .OrderBy(breed => breed.Name)
            .ToList()
            ?? [];
    }


    public async Task<DogBreedDetails> GetBreedByIdAsync(
        string id,
        CancellationToken cancellationToken = default)
    {
        using var response = await _httpClient.GetAsync(
            $"https://dogapi.dog/api/v2/breeds/{Uri.EscapeDataString(id)}",
            cancellationToken);

        response.EnsureSuccessStatusCode();

        await using var stream =
            await response.Content.ReadAsStreamAsync(cancellationToken);

        var result = await JsonSerializer.DeserializeAsync<BreedDetailResponse>(
            stream,
            cancellationToken: cancellationToken);

        var data = result?.Data
            ?? throw new InvalidOperationException(
                "La API no devolvió información para esta raza.");

        var attributes = data.Attributes
            ?? throw new InvalidOperationException(
                "La respuesta no contiene los atributos de la raza.");

        return new DogBreedDetails
        {
            Id = data.Id,
            Name = attributes.Name ?? "Nombre desconocido",
            Description = attributes.Description ?? "Sin descripción disponible.",

            LifeMin = attributes.Life?.Min,
            LifeMax = attributes.Life?.Max,

            MaleWeightMin = attributes.MaleWeight?.Min,
            MaleWeightMax = attributes.MaleWeight?.Max,

            FemaleWeightMin = attributes.FemaleWeight?.Min,
            FemaleWeightMax = attributes.FemaleWeight?.Max,

            MaleHeightMin = attributes.MaleHeight?.Min,
            MaleHeightMax = attributes.MaleHeight?.Max,

            FemaleHeightMin = attributes.FemaleHeight?.Min,
            FemaleHeightMax = attributes.FemaleHeight?.Max,

            Hypoallergenic = attributes.Hypoallergenic,
            OriginCountry = attributes.Origin?.Country,
            OriginRegion = attributes.Origin?.Region
        };
    }

    private sealed class BreedDetailResponse
    {
        [JsonPropertyName("data")]
        public BreedDetailData? Data { get; set; }
    }

    private sealed class BreedDetailData
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;

        [JsonPropertyName("attributes")]
        public BreedDetailAttributes? Attributes { get; set; }
    }

    private sealed class BreedDetailAttributes
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("life")]
        public BreedRange? Life { get; set; }

        [JsonPropertyName("male_weight")]
        public BreedRange? MaleWeight { get; set; }

        [JsonPropertyName("female_weight")]
        public BreedRange? FemaleWeight { get; set; }

        [JsonPropertyName("male_height")]
        public BreedRange? MaleHeight { get; set; }

        [JsonPropertyName("female_height")]
        public BreedRange? FemaleHeight { get; set; }

        [JsonPropertyName("hypoallergenic")]
        public bool? Hypoallergenic { get; set; }

        [JsonPropertyName("origin")]
        public BreedOrigin? Origin { get; set; }
    }

    private sealed class BreedRange
    {
        [JsonPropertyName("min")]
        public int? Min { get; set; }

        [JsonPropertyName("max")]
        public int? Max { get; set; }
    }

    private sealed class BreedOrigin
    {
        [JsonPropertyName("country")]
        public string? Country { get; set; }

        [JsonPropertyName("region")]
        public string? Region { get; set; }
    }

    private sealed class BreedsResponse
    {
        [JsonPropertyName("data")]
        public List<BreedData>? Data { get; set; }
    }

    private sealed class BreedData
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;

        [JsonPropertyName("attributes")]
        public BreedAttributes? Attributes { get; set; }
    }

    private sealed class BreedAttributes
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }
    }
}