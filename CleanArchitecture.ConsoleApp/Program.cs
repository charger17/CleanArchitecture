using CleanArchitecture.Data;
using CleanArchitecture.Domain;
using Microsoft.EntityFrameworkCore;

StreamerDbContext dbContext = new();

//await AddNewRecords();
//QueryStreaming();
await QueryFilter();

Console.WriteLine("Presione cualquier tecla para terminar el programa");
Console.ReadKey();


async Task QueryFilter()
{
    Console.WriteLine($"Ingrese una compania de streaming: ");
    var streamingNombre = Console.ReadLine();

    var streamers = await dbContext.Streamers.Where(x => x.Nombre.Equals(streamingNombre)).ToListAsync();

    foreach (var streamer in streamers)
    {
        Console.WriteLine($"{streamer.Id} - {streamer.Nombre}");
    }

    //var streamerPartialResults = await dbContext.Streamers.Where(x => x.Nombre.Contains(streamingNombre)).ToListAsync();
    var streamerPartialResults = await dbContext.Streamers.Where(x => EF.Functions.Like(x.Nombre, $"%{streamingNombre}%")).ToListAsync();

    foreach (var streamer in streamerPartialResults)
    {
        Console.WriteLine($"{streamer.Id} - {streamer.Nombre}");
    }
}



void QueryStreaming()
{
    var streamers = dbContext.Streamers!.ToList();

    foreach (var streamer in streamers)
    {
        Console.WriteLine($"{streamer.Id} - {streamer.Nombre}");
    }
}

async Task AddNewRecords()
{
    Streamer streamer = new Streamer()
    {
        Nombre = "Disney",
        Url = "https://www.disney.com"
    };

    dbContext!.Streamers!.Add(streamer);

    await dbContext!.SaveChangesAsync();

    var movies = new List<Video>() {
    new Video
    {
        Nombre = "La Cenicienta",
        StreamerId = streamer.Id,
    },
    new Video
    {
        Nombre = "1001 Dalmatas",
        StreamerId = streamer.Id,
    },
    new Video
    {
        Nombre = "El jorobado de Notredame",
        StreamerId = streamer.Id,
    },
    new Video
    {
        Nombre = "Star wars",
        StreamerId = streamer.Id,
    },
};

    await dbContext.AddRangeAsync(movies);
    await dbContext!.SaveChangesAsync();

}