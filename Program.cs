using Microsoft.EntityFrameworkCore;

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
                       ?? "Data Source=contacts.db";
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));

builder.Services.AddControllersWithViews();

