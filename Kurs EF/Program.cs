using Kurs_EF.Dto;
using Kurs_EF.Entities;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

builder.Services.AddDbContext<MyBoardsContext>(
    option => option
    //.UseLazyLoadingProxies()
    .UseSqlServer(builder.Configuration.GetConnectionString("MyBoardsConnectionString"))
    );

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using var scope = app.Services.CreateScope();
var DbContext = scope.ServiceProvider.GetService<MyBoardsContext>();

var pendingMigrations = DbContext.Database.GetPendingMigrations();
if (pendingMigrations.Any()) DbContext.Database.Migrate();

//dodawanie danych do tabeli (encji) - seed-owanie danych - sposób 3
var user = DbContext.Users.ToList();
if (!user.Any())
{
    var user1 = new User()
    {
        Email = "fiszer@wp.pl",
        FullName = "User One",
        Adress = new Adress
        {
            Country = "Poland",
            City = "Wroc³aw",
            PostalCode = "12345",
            Street = "prochowicka 45"
        }
    };
    var user2 = new User()
    {
        Email = "konopka@wp.pl",
        FullName = "User Two",
        Adress = new Adress
        {
            Country = "Poland",
            PostalCode = "67890",
            Street = "wroc³awska 23"
        }
    };
    DbContext.Users.AddRange(user1, user2);
    DbContext.SaveChanges();
}
var tag = DbContext.Tags.ToList();
if (tag.Count < 5)
{
    var tag1 = new Tag()
    {
        //Id = 1,
        Value = "Web",
    };
    var tag2 = new Tag()
    {
        //Id = 2,
        Value = "UI",
    };

    DbContext.Tags.AddRange(tag1, tag2);
    if (tag.Count == 5) Console.WriteLine("Wpisy istniej¹");
    else Console.WriteLine("Wpisy nie istniej¹");
    DbContext.SaveChanges();
}

app.MapGet("Pagination", async (MyBoardsContext db) =>
{
    //teoretyczne dane ze strony od u¿ytkownika
    var filter = "anna";
    string sortBy = null;  //"FulllName", "Email", null
    bool sortByDescending = false;
    int pageNumber = 1;
    int pageSize = 10;

    var querry = db.Users
    .Where(u => filter == null ||
    (u.Email.ToLower().Contains(filter.ToLower()) ||
    u.FullName.ToLower().Contains(filter.ToLower())));

    var totalCount = querry.Count();

    if(sortBy != null)
    {
        var columnSelector = new Dictionary<string, Expression<Func<User, object>>>
        {
            { nameof(User.Email), user=>user.Email },
            { nameof(User.FullName), user=>user.FullName },
        };
        var sortByExpression = columnSelector[sortBy];

        querry = sortByDescending 
        ? querry.OrderByDescending(sortByExpression)
        : querry.OrderBy(sortByExpression);
    }
    var result = querry.Skip(pageSize * (pageNumber - 1))
                        .Take(pageSize)
                        .ToList();

    var pagedResult = new PagedResult<User>(result, totalCount, pageSize, pageNumber);
    return pagedResult;
});

app.MapDelete("delete", async (MyBoardsContext db) =>
{
    var workItem = new Epic { Id = 2 };
    var entry = db.Attach(workItem);
    entry.State = EntityState.Deleted;
    db.SaveChanges();
    return user;

    //var user = await db.Users
    //.Include(u => u.Comments)
    //.FirstAsync(a => a.Id == Guid.Parse("E4E81277-99E7-4ACF-CBC5-08DA10AB0E61"));
    //db.Remove(user);
    //db.SaveChanges();

    //var userComments = db.Comments
    //.Where(u=>u.AuthorId== Guid.Parse("5CB27C3F-32D9-4474-CBC2-08DA10AB0E61"))
    //.ToList();
    //db.RemoveRange(userComments);
    //await db.SaveChangesAsync();
    //var user = await db.Users.FirstAsync(u => u.Id == Guid.Parse("5CB27C3F-32D9-4474-CBC2-08DA10AB0E61"));
    //db.Remove(user);
    //await db.SaveChangesAsync();

    //var workItemTags = await db.WorkItemTag.Where(c=>c.WorkItemId==12).ToListAsync();
    //db.WorkItemTag.RemoveRange(workItemTags);
    //var workItem = await db.WorkItems.FirstAsync(w=>w.Id==16);
    //db.RemoveRange(workItem);
    //await db.SaveChangesAsync();
});

app.MapGet("View", async (MyBoardsContext db) =>
{
    var powi¹zanieKEYLESS = await db.Adresses.Where(a=>a.Coordinates.Longitude>10).ToListAsync();
    return powi¹zanieKEYLESS;

    //var TopAuthors = await db.ViewTopAuthors.ToListAsync();
    //return TopAuthors;
});

app.MapGet("Find", async (MyBoardsContext db) =>
{

    var WIStates85 = db.WorkItemStates              //...Raw dla zapytañ bez zmiennych. Musz¹ byæ ujête wszystkie kolumny z DbSeta - tu Id, Value
    .FromSqlRaw(@"SELECT wis.Id,  wis.Value, COUNT(*)
                  FROM WorkItemStates wis
                  Join WorkItems wi on wi.StateId = wis.Id
                  GROUP BY wis.Id, wis.Value
                  HAVING COUNT(*)>85")
    .ToList();
    return WIStates85;

    //var minWIcounts = 85;
    //var workItemStates85 = db.WorkItemStates           //...Interpolated dla zapytañ ze zmiennymi. Musz¹ byæ ujête wszystkie kolumny z DbSeta
    //.FromSqlInterpolated($@"SELECT wis.Id,  wis.Value, COUNT(*) 
    //              FROM WorkItemStates wis
    //              Join WorkItems wi on wi.StateId = wis.Id
    //              GROUP BY wis.Id, wis.Value
    //              HAVING COUNT(*)>{minWIcounts}")
    //.ToList();
    //return workItemStates85;

    //var states = db.WorkItemStates
    //.AsNoTracking()
    //.ToList();
    //var entries1 = db.ChangeTracker.Entries();
    //return states;

    //var user = await db.Users
    //.FirstAsync(u => u.Id == Guid.Parse("D00D8059-8977-4E5F-CBD2-08DA10AB0E61"));
    //var entries1 = db.ChangeTracker.Entries();
    //user.FullName = "Krystyna Starosta";
    //var entries2 = db.ChangeTracker.Entries();
    //db.SaveChanges();
    //return user;

    //var user = await db.Users
    //.Include(u => u.Comments).ThenInclude(c => c.WorkItem)
    //.Include(u => u.Adress)
    //.FirstAsync(u => u.Id == Guid.Parse("819E827C-4C91-4244-4FC9-08DB46527A69"));
    //return user;

    //var User = await db.Users.FirstAsync(u => u.Id == Guid.Parse("5CB27C3F-32D9-4474-CBC2-08DA10AB0E61"));
    //var UserComments = await db.Comments.Where(c=>c.AuthorId==User.Id).ToListAsync();
    //return UserComments;
});

app.MapPost("create", async (MyBoardsContext db) =>
{
    var adres1 = new Adress() { Id = Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950e"), Country = "Polska", City = "Warszawa", Street = "Borowska" };
    User user1 = new User() { Email = "user2@wp.pl", FullName = "Ania Bania", Adress = adres1 };
    await db.Users.AddAsync(user1);
    await db.SaveChangesAsync();
    return user1;

    //Tag AspTag = new Tag() { Value = "ASP" };
    //Tag MvcTag = new Tag() { Value = "MVC" };
    ////List<Tag> TagList = new List<Tag>() { AspTag, MvcTag };
    ////await db.Tags.AddRangeAsync(TagList);
    //await db.Tags.AddRangeAsync(AspTag, MvcTag);
    //await db.SaveChangesAsync();

    //Tag tag = new Tag()
    //{
    //    Value = "EF"
    //};
    //await db.AddAsync(tag);
    //await db.SaveChangesAsync();
    //return tag;
});

app.MapPost("update", async (MyBoardsContext db) =>
{
    Epic epic = await db.Epics.FirstAsync(e => e.Id == 1);
    var rejectedState = await db.WorkItemStates.FirstAsync(s => s.Value == "Rejected");
    epic.State = rejectedState;
    await db.SaveChangesAsync();
    return epic;

    //Epic epic = await db.Epics.FirstAsync(e => e.Id == 1);
    //var onHoldState = await db.WorkItemStates.FirstAsync(s => s.Value == "On Hold");
    //epic.StateId = onHoldState.Id;
    //await db.SaveChangesAsync();
    //return epic;

    //Epic epic = await db.Epics.FirstAsync(e => e.Id == 1);
    //epic.StateId = 1;
    //await db.SaveChangesAsync();
    //return epic;

    //Epic epic = await db.Epics.FirstAsync(e=>e.Id==1);
    //epic.Area = "Updated area";
    //epic.Priority = 1;
    //epic.StartDate = DateTime.Now;
    //await db.SaveChangesAsync();
    //return epic;
});

app.MapGet("data", async (MyBoardsContext db) =>
{
    var EpicsOnHold = await db
    .Epics
    .Where(e => e.StateId == 4)
    .OrderByDescending(p => p.Priority)
    .ToListAsync();
    return EpicsOnHold;

    //var UserWithMaxComment = await db
    //.Comments
    //.GroupBy(u => u.AuthorId)
    //.Select(a => new { Author = a.Key, count=a.Count()})
    //.OrderByDescending(x=>x.count)
    //.Take(1)
    //.ToListAsync();
    //return UserWithMaxComment; 

    //var whatWorkIteamsState = await db
    //    .WorkItems
    //    .GroupBy(x => x.StateId)
    //    .Select(g => new { stateId = g.Key, count = g.Count() })
    //    .ToListAsync();
    //return whatWorkIteamsState;

    //var topFive = await db
    //.Comments
    //.OrderByDescending(c=>c.CreatedDate)
    //.Take(5)
    //.ToListAsync();
    //return topFive;

    //var CommentsDate = await db
    //.Comments
    //.Where(c=>c.CreatedDate> new DateTime(2022,7,23))
    //.ToListAsync();
    //return CommentsDate;

    //var ToDoEpic = db.WorkItems.Where(e => e.StateId == 3).ToList();
    //return ToDoEpic;

    //var epic = db.Epics.First();
    //var user = db.Users.First(u=>u.FullName == "User Two");
    //return new { epic, user };

    //var tags = db.Tags.ToList();
    //return tags;
});

app.Run();

