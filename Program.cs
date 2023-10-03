using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

using Microsoft.EntityFrameworkCore;
using unvest_transactions_ms.Models;
using unvest_transactions_ms.Controllers;

using System.Text.Json;

var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
var dbPort = Environment.GetEnvironmentVariable("DB_PORT");
var dbName = Environment.GetEnvironmentVariable("DB_NAME");
var dbUser = Environment.GetEnvironmentVariable("DB_USER");
var dbPass = Environment.GetEnvironmentVariable("DB_PASSWORD");
var mqHost = Environment.GetEnvironmentVariable("MQ_HOST");

int mqPort = 5672;
if(!int.TryParse(Environment.GetEnvironmentVariable("MQ_PORT"), out mqPort))
{
    mqPort = 5672;
}

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<TransactionsContext>(opt => opt.UseSqlServer($"Server={dbHost},{dbPort}; Database={dbName}; User Id={dbUser}; Password={dbPass}; Trusted_Connection=false; MultipleActiveResultSets=true;TrustServerCertificate=True;"));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<TransactionsContext>();
    if (context.Database.GetPendingMigrations().Any())
    {
        context.Database.Migrate();
    }
}





var factory = new ConnectionFactory { HostName = mqHost, Port = mqPort };
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

channel.QueueDeclare(queue: "rpc_queue", durable: false, exclusive: false, autoDelete: false, arguments: null);
channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);
var consumer = new EventingBasicConsumer(channel);
channel.BasicConsume(queue: "rpc_queue", autoAck: false, consumer: consumer);

Console.WriteLine(" [x] Awaiting requests");

consumer.Received += async (model, ea) =>
{
    string response = string.Empty;

    var body = ea.Body.ToArray();
    var props = ea.BasicProperties;
    var replyProps = channel.CreateBasicProperties();
    replyProps.CorrelationId = props.CorrelationId;

    try
    {
        var dbContext = app.Services.GetRequiredService<TransactionsContext>();
        var transaccionController = new TransaccionAsyncController(dbContext);
        var transacciones =  await transaccionController.GetTransaccion();
        var opt = new JsonSerializerOptions(){ WriteIndented=true };
        response = JsonSerializer.Serialize<IList<Transaccion>>(transacciones, opt);
    }
    catch (Exception e)
    {
        Console.WriteLine($" [.] {e.Message}");
        response = string.Empty;
    }
    finally
    {
        var responseBytes = Encoding.UTF8.GetBytes(response);
        channel.BasicPublish(exchange: string.Empty, routingKey: props.ReplyTo, basicProperties: replyProps, body: responseBytes);
        channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
    }
};




app.Run();
