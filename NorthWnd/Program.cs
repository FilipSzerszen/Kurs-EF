using LinqToDB.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NorthWnd.Entities;
using System.Linq.Expressions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<NorthwndContext>(
    option => option
    .UseSqlServer(builder.Configuration.GetConnectionString("NORTHWNDConnectionString"))
    );

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPut("updateLinq2db", async (NorthwndContext db) =>
{
    var employees = db.Employees
                 .Where(e => e.HireDate < new DateTime(1994, 1, 1));
    await LinqToDB.LinqExtensions.UpdateAsync(employees.ToLinqToDB(), x => new Employee{ Notes = "Old employee" });
});

app.MapPut("update", (NorthwndContext db) =>
{
    var users = db.Employees
                .Where(e => e.HireDate > new DateTime(1994, 1, 1))
                .ToList();
    foreach (var user in users)
    {
        user.Notes = "New employee";
    }
    db.SaveChanges();
});

app.MapGet("getOrderDetails", async (NorthwndContext db) =>
{
    Order order = await GetOrder(1, db, o => o.OrderDetails);
    return new { OrderId = order.OrderId, Details = order.OrderDetails };
});

app.MapGet("getOrderWithShipper", async (NorthwndContext db) =>
{
    Order order = await GetOrder(2, db, o => o.ShipViaNavigation);
    return new { OrderId = order.OrderId, ShipVia = order.ShipVia, Shipper=order.ShipViaNavigation };
});

app.MapGet("getOrderWithCustomer", async (NorthwndContext db) =>
{
    Order order = await GetOrder(3, db, o => o.Customer);
    return new { OrderId = order.OrderId, Customer = order.Customer };
});

app.MapGet("View", async (NorthwndContext db) =>
{
    var sample = await db.Products.Take(100).ToListAsync();
    return sample;

});

app.Run();

async Task<Order> GetOrder(int orderId, NorthwndContext db, params Expression<Func<Order, object>>[] includes)
{
    var baseQuery = db.Orders
        .AsQueryable();

    if (includes.Any())
    {
        foreach (var include in includes)
        {
            baseQuery = baseQuery.Include(include);
        }
    }
    var order = await baseQuery.FirstAsync(o => o.OrderId == orderId);
    return order;
}