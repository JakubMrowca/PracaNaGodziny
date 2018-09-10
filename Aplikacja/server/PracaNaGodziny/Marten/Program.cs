using System;
using Marten;

namespace martenService
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var documentStore = DocumentStore.For(options =>
            {

                options.Connection(
                    "PORT = 5432; HOST = 127.0.0.1; TIMEOUT = 15; POOLING = True; MINPOOLSIZE = 1; MAXPOOLSIZE = 100; COMMANDTIMEOUT = 20; DATABASE = 'workhour'; PASSWORD = 'mrowca144'; USER ID = 'postgres'");
                options.Events.DatabaseSchemaName = "wh";
                options.DatabaseSchemaName = "wh";
                options.AutoCreateSchemaObjects = AutoCreate.All;
            });

            var service = documentStore.OpenSession();
        }

    }
}
