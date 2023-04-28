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
                        Name = "Woshing",
                        Description = "i am here 0",
                        Priority = Priority.LowMedium,
                        Status = Status.Completed
                    },
                    new Backend.Models.Task() {
                        Name = "Tooth",
                        Description = "i am here 1",
                        Priority = Priority.Low,
                        Status = Status.InProgress
                    },
                    new Backend.Models.Task() {
                        Name = "Breakfast",
                        Description = "i am here 2",
                        Priority = Priority.Medium,
                        Status = Status.Initial
                    },
                    new Backend.Models.Task() {
                        Name = "HorsingRound",
                        Description = "i am here 3",
                        Priority = Priority.MediumHigh,
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