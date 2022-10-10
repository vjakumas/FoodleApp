using Foodle.Data;
using Foodle.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<FoodleDbContext>();
builder.Services.AddTransient<ICategoriesRepository, CategoriesRepository>();
builder.Services.AddTransient<IRecipesRepository, RecipesRepository>();
builder.Services.AddTransient<IIngredientsRepository, IngredientsRepository>();


var app = builder.Build();

app.UseRouting();
app.MapControllers();


app.Run();
