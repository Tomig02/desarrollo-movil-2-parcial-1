namespace parcial_1_tests.Testing;

internal static class TestData
{
    public const string BreedsJson = """
        {
          "data": [
            { "id": "2", "attributes": { "name": "Zebra Dog" } },
            { "id": "1", "attributes": { "name": "Akita" } }
          ]
        }
        """;

    public const string BreedDetailJson = """
        {
          "data": {
            "id": "akita",
            "attributes": {
              "name": "Akita",
              "description": "A spirited dog.",
              "life": { "min": 10, "max": 15 },
              "male_weight": { "min": 30, "max": 45 },
              "female_weight": { "min": 25, "max": 40 },
              "male_height": { "min": 60, "max": 70 },
              "female_height": { "min": 55, "max": 65 },
              "hypoallergenic": false,
              "origin": { "country": "Japan", "region": "Asia" }
            }
          }
        }
        """;
}
