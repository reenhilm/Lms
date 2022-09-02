using Lms.Core.Repositories;
using Lms.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Data.Data
{
    public static class SeedData
    {
        public static async Task InitAsync(IUoW uow)
        {
            if (uow is null) throw new ArgumentNullException(nameof(uow));

            uow.CourseRepository.Add(
                new Course() {
                    StartDate = DateTime.Now,
                    Title = "HarEnModule",
                    Modules = new List<Module>()
                    {
                        new Module() {
                            StartDate= DateTime.Now,
                            Title = "Module1"
                        }
                    }
                });
            uow.CourseRepository.Add(
                new Course() {
                    StartDate = DateTime.Now.AddHours(5),
                    Title = "HarTvåModules",
                    Modules = new List<Module>()
                    {
                        new Module() {
                            StartDate= DateTime.Now.AddHours(-5),
                            Title = "Module1"
                        },
                        new Module() {
                            StartDate= DateTime.Now.AddHours(11),
                            Title = "Module2"
                        }
                    }
                });
            uow.CourseRepository.Add(
                new Course()
                {
                    StartDate = DateTime.Now.AddHours(-5),
                    Title = "Testar777",
                    Modules = new List<Module>()
                    {
                        new Module() {
                            StartDate= DateTime.Now.AddHours(-10),
                            Title = "ModuleTitleFirst"
                        }
                    }
                });
            uow.CourseRepository.Add(
                new Course()
                {
                    StartDate = DateTime.Now.AddHours(5),
                    Title = "HarIngaModules"
                });
            await uow.CompleteAsync();
        }
    }
}
