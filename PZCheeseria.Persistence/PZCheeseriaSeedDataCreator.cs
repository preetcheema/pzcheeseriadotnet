using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using PZCheeseria.Common;
using PZCheeseria.Domain;
using PZCheeseria.Domain.Entities;

namespace PZCheeseria.Persistence
{
    public static class PZCheeseriaSeedDataCreator
    {
        public static void CreateData(PZCheeseriaContext context, Func<DateTime> getDateTimeFunc)
        {
            context.Database.EnsureCreated();
            if (context.Cheeses.Any())
            {
                return;
            }

            SeedData(context, getDateTimeFunc);
        }

        public  static void SeedData(PZCheeseriaContext context, Func<DateTime> getDateTimeFunc)
        {
            var blue = new CheeseColour {Colour = "Blue"};
            var brownishYellow = new CheeseColour {Colour = "Brownish Yellow"};
            var cream = new CheeseColour {Colour = "Cream"};

          
            context.CheeseColours.AddRange(blue, brownishYellow, cream);
            context.SaveChanges();

            var ambert = new Cheese
            {
                Name = "Ambert",
                PricePerKilo = 20.50m,
                Colour = blue,
                ImageName = "ambert.jpg",
                Description =
                    @"Produced in the Auvergne region, Fourme d'Ambert (or simply Ambert) is one of France's oldest cheeses, dating back to the Roman occupation nearly 1,000 years ago.It is said that the Druids and the Gauls had developed the art of making this unique cheese. In 2002 it was separated from the Fourme de Montbrison,an identical cheese, to receive an individual AOC status.",
                CreatedOn = getDateTimeFunc()
            };
            var blueCastello = new Cheese
            {
                Name = "Blue Castello",
                PricePerKilo = 50,
                Colour = blue,
                ImageName = "blueCastello.jpg",
                Description =
                    "In the 1960s, one of the oldest cheese producing companies of Denmark - Tholstrup Cheese Company initially prepared Blue Castello cheese.This soft cheese made from cow’s milk has a smooth and creamy texture.",
                CreatedOn = getDateTimeFunc()
            };
            var bethmaleDesPyrenees = new Cheese
            {
                Name = "Bethmale des Pyrenees",
                PricePerKilo = 25.25m,
                Colour = brownishYellow,
                ImageName = "BethmaleDesPyrenees.jpg",
                Description =
                    "The Bethmale, made from cow's milk, is a Tomme with an impressive 'farmyard' quality. Soft in consistency and mild of taste,it is uncooked, pressed, with distinctive slight holes marking its appearance.The first Bethmale dates back to the times of the Moor occupation.",
                CreatedOn = getDateTimeFunc()
            };
            var bavariaBlu = new Cheese
            {
                Name = "Bavaria blu",
                PricePerKilo = 80.50m,
                Colour = cream,
                ImageName = "BavariaBlu.jpg",
                Description =
                    "Bavaria blu is a blue mould cheese introduced by Bergader in 1972. It is produced with the highest quality pasteurised cow's milk from the Bavarian Alps. The unique taste of the creamy soft cheese is all thanks to the juicy, fragrant grass the cows are fed on. It is entirely handmade without additives or preservatives.",
                CreatedOn = getDateTimeFunc()
            };
            var brie = new Cheese
            {
                Name = "Brie",
                PricePerKilo = 20.50m,
                Colour = cream,
                ImageName = "brie.jpg",
                Description =
                    "Brie is the best known French cheese and has a nickname 'The Queen of Cheeses'. Brie is a soft cheese named after the French region Brie, where it was originally created. Several hundred years ago, Brie was one of the tributes which had to be paid to the French kings.",
                CreatedOn = getDateTimeFunc()
            };

            context.Cheeses.AddRange(ambert, blueCastello, bethmaleDesPyrenees, bavariaBlu, brie);
            context.SaveChanges();
        }
    }
}