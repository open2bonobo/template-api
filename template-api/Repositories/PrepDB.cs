using Backend.Models;

namespace Backend.Data
{
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder app)
        {
            using( var serviceScope = app.ApplicationServices.CreateScope())
            {
                if(serviceScope != null)
                    SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>());
            }
        }

        private static void SeedData(AppDbContext context)
        {
            
            if(!context.Tasks.Any())
            {
                Console.WriteLine("--> Seeding Data...");

                context.Tasks.AddRange(
                    new Backend.Models.Task() {
                        Id = 0,
                        Name = "Woshing",
                        Priority = 1,
                        Status = Status.Completed
                    },
                    new Backend.Models.Task() {
                        Id = 0,
                        Name = "Tooth",
                        Priority = 1,
                        Status = Status.InProgress
                    },
                    new Backend.Models.Task() {
                        Id = 0,
                        Name = "Breakfast",
                        Priority = 1,
                        Status = Status.Initial
                    },
                    new Backend.Models.Task() {
                        Id = 0,
                        Name = "HorsingRound",
                        Priority = 1,
                        Status = Status.Initial
                    });

                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("--> We already have data");
            }
        }
    }
}