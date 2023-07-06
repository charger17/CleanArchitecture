using CleanArchitecture.Data;
using CleanArchitecture.Domain;
using Microsoft.EntityFrameworkCore;

StreamerDbContext dbContext = new();

//await AddNewRecords();
//QueryStreaming();
//await QueryFilter();
//await QueryMethod();
await QueryLinq();

Console.WriteLine("Presione cualquier tecla para terminar el programa");
Console.ReadKey();


async Task QueryLinq()
{
    Console.WriteLine($"Ingrese el servicio de streaming: ");
    var streamerNombre = Console.ReadLine();

    var streamers = await (from i in dbContext.Streamers
                           where EF.Functions.Like(i.Nombre, $"{streamerNombre}")
                           select i).ToListAsync();

    foreach (var streamer in streamers)
    {
        Console.WriteLine($"{streamer.Id} - {streamer.Nombre}");
    }
}

async Task QueryMethod()
{
    var streamer = dbContext!.Streamers!;

    var firstAsync = await streamer.Where(y => y.Nombre.Contains("a")).FirstAsync();
    var firstorDefaultAsync = await streamer!.Where(y => y.Nombre.Contains("a")).FirstOrDefaultAsync();
    var firstOrDefualt_v2 = await streamer.FirstOrDefaultAsync(y => y.Nombre.Contains("a"));

    var singleAsync = await streamer.Where(y => y.Id == 1).SingleAsync();
    var singleOrDefaultAsync = await streamer.Where(y => y.Id == 1).SingleOrDefaultAsync();

    var resultado = await streamer.FindAsync(1);
}


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