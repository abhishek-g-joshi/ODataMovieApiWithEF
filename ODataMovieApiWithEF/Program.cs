using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using ODataMovieApiWithEF.EFCore;
using ODataMovieApiWithEF.Models;
using ODataMovieApiWithEF.Repository;


//Configure Edm Model
static IEdmModel GetEdmModel()
{
    ODataConventionModelBuilder builder1 = new ODataConventionModelBuilder();
    builder1.EntitySet<Movie>("Movies");
    builder1.EntitySet<Person>("Person");
    var edmModel = builder1.GetEdmModel();
    return edmModel;
}

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
#pragma warning disable CS8604 // Possible null reference argument.
builder.Services.AddDbContext<MovieAppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
#pragma warning restore CS8604 // Possible null reference argument.
builder.Services.AddControllers().AddOData(opt => opt.AddRouteComponents("v1",GetEdmModel()).Filter().Select().Expand().OrderBy());
builder.Services.AddScoped<IMovieRepository, MovieRepository>();
builder.Services.AddScoped<IPersonRepositroy, PersonRepositroy>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
