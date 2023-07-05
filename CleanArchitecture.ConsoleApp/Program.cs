using CleanArchitecture.Data;
using CleanArchitecture.Domain;

StreamerDbContext dbContext = new();

Streamer streamer = new Streamer()
{
    Nombre = "Amazon Prime",
    Url = "https://www.amazonprime.com"
};

dbContext!.Streamers!.Add(streamer);

await dbContext!.SaveChangesAsync();

var movies = new List<Video>() {
    new Video
    {
        Nombre = "Mad Max",
        StreamerId = streamer.Id,
    },
    new Video
    {
        Nombre = "Batman",
        StreamerId = streamer.Id,
    },
    new Video
    {
        Nombre = "Crepusculo",
        StreamerId = streamer.Id,
    },
    new Video
    {
        Nombre = "Citizen Kane",
        StreamerId = streamer.Id,
    },
};

await dbContext.AddRangeAsync(movies);
await dbContext!.SaveChangesAsync();