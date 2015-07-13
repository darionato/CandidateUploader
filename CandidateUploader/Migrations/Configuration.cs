using System.Collections.Generic;
using System.Linq;
using CandidateUploader.Models;

namespace CandidateUploader.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<CuDataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(CuDataContext context)
        {

            if (context.Attributes.Any()) return;


            var course = new AttributeModel {Type = "Concorso", Position = 1};

            context.Attributes.Add(course);

            context.SaveChanges();

            context.Attributes.AddRange(
                new List<AttributeModel>
                {
                    new AttributeModel
                    {
                        Code = "concorso1",
                        Parent = course,
                        Position = 2,
                        Type = course.Type,
                        Text = "Concorso 1"
                    },
                    new AttributeModel
                    {
                        Code = "concorso2",
                        Parent = course,
                        Position = 3,
                        Type = course.Type,
                        Text = "Concorso 2"
                    },
                    new AttributeModel
                    {
                        Code = "concorso3",
                        Parent = course,
                        Position = 4,
                        Type = course.Type,
                        Text = "Concorso 3"
                    },
                    new AttributeModel
                    {
                        Code = "concorso4",
                        Parent = course,
                        Position = 5,
                        Type = course.Type,
                        Text = "Concorso 4"
                    },
                    new AttributeModel
                    {
                        Code = "concorso5",
                        Parent = course,
                        Position = 6,
                        Type = course.Type,
                        Text = "Concorso 5"
                    },
                    new AttributeModel
                    {
                        Code = "concorso6",
                        Parent = course,
                        Position = 7,
                        Type = course.Type,
                        Text = "Concorso 6"
                    },
                });


            context.SaveChanges();


        }
    }
}
