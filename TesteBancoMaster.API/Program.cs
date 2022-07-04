using TesteBancoMaster.API.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApiConfiguration(builder.Configuration);

// Swagger Config
builder.Services.AddSwaggerConfiguration();

// AutoMapperConfig
builder.Services.AddAutoMapperConfiguration();

//Injeção de dependencia
builder.Services.AddRegisterServices();

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
