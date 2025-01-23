using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace drinks.athallie
{
    public record class Drinks(
        [property: JsonPropertyName("strDrink")] string Name,
        [property: JsonPropertyName("strDrinkThumb")] string thumbnailUrl,
        [property: JsonPropertyName("idDrink")] string Id
    );
}
