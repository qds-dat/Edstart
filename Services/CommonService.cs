using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Edstart.Services
{
    public class CommonService
    {
        private Random rnd = new Random();

        public string GenRandomEmail()
        {
            List<string> lst = new List<string>();
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var result = new string(
                Enumerable.Repeat(chars, 8)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());

            return result + "@edstart.com.au";
        }

        public string GenRandomSchool()
        {
            List<string> lst = new List<string>();
            lst.Add("California Institute of Technology");
            lst.Add("Karolinska Institute");
            lst.Add("London School of Economics and Political Science");
            lst.Add("Georgia Institute of Technology");
            lst.Add("Weizmann Institute of Science");
            lst.Add("Icahn School of Medicine at Mount Sinai");
            lst.Add("Tokyo Institute of Technology");
            lst.Add("Trinity College Dublin");
            lst.Add("Dartmouth College");
            lst.Add("Harbin Institute of Technology");
            lst.Add("Rensselaer Polytechnic Institute");
            lst.Add("Royal Institute of Technology");
            return lst.OrderBy(xx => rnd.Next()).First();
        }

        public string GenRandomAddresss()
        {
            List<string> lst = new List<string>();
            lst.Add("7185 Dewy Mews");
            lst.Add("5961 Shady Wynd");
            lst.Add("8707 Golden Goose Mall");
            lst.Add("2858 Wishing Timber Point");
            lst.Add("396 Umber Common");
            lst.Add("7643 Old Nectar Promenade");
            lst.Add("7668 Broad Glen");
            lst.Add("345 Broad Glen");
            lst.Add("24 Umber Hill");
            lst.Add("356 Old Nectar");
            lst.Add("413 Gold Drug");
            lst.Add("23 THT");
            return lst.OrderBy(xx => rnd.Next()).First();
        }

        public int GenRandomNumber(int min =0,int max=0)
        {
            return rnd.Next(min,max);
        }
        public string GenRandomLastName()
        {
            List<string> lst = new List<string>();
            lst.Add("Smith");
            lst.Add("Johnson");
            lst.Add("Williams");
            lst.Add("Jones");
            lst.Add("Brown");
            lst.Add("Davis");
            lst.Add("Miller");
            lst.Add("Wilson");
            lst.Add("Moore");
            lst.Add("Taylor");
            lst.Add("Anderson");
            lst.Add("Thomas");
            lst.Add("Jackson");
            lst.Add("White");
            lst.Add("Harris");
            lst.Add("Martin");
            lst.Add("Thompson");
            lst.Add("Garcia");
            lst.Add("Martinez");
            lst.Add("Robinson");
            lst.Add("Clark");
            lst.Add("Rodriguez");
            lst.Add("Lewis");
            lst.Add("Lee");
            lst.Add("Walker");
            lst.Add("Hall");
            lst.Add("Allen");
            lst.Add("Young");
            lst.Add("Hernandez");
            lst.Add("King");
            lst.Add("Wright");
            lst.Add("Lopez");
            lst.Add("Hill");
            lst.Add("Scott");
            lst.Add("Green");
            lst.Add("Adams");
            lst.Add("Baker");
            lst.Add("Gonzalez");
            lst.Add("Nelson");
            lst.Add("Carter");
            lst.Add("Mitchell");
            lst.Add("Perez");
            lst.Add("Roberts");
            lst.Add("Turner");
            lst.Add("Phillips");
            lst.Add("Campbell");
            lst.Add("Parker");
            lst.Add("Evans");
            lst.Add("Edwards");
            lst.Add("Collins");
            lst.Add("Stewart");
            lst.Add("Sanchez");
            lst.Add("Morris");
            lst.Add("Rogers");
            lst.Add("Reed");
            lst.Add("Cook");
            lst.Add("Morgan");
            lst.Add("Bell");
            lst.Add("Murphy");
            lst.Add("Bailey");
            lst.Add("Rivera");
            lst.Add("Cooper");
            lst.Add("Richardson");
            lst.Add("Cox");
            lst.Add("Howard");
            lst.Add("Ward");
            lst.Add("Torres");
            lst.Add("Peterson");
            lst.Add("Gray");
            lst.Add("Ramirez");
            lst.Add("James");
            lst.Add("Watson");
            lst.Add("Brooks");
            lst.Add("Kelly");
            lst.Add("Sanders");
            lst.Add("Price");
            lst.Add("Bennett");
            lst.Add("Wood");
            lst.Add("Barnes");
            lst.Add("Ross");
            lst.Add("Henderson");
            lst.Add("Coleman");
            lst.Add("Jenkins");
            lst.Add("Perry");
            lst.Add("Powell");
            lst.Add("Long");
            lst.Add("Patterson");
            lst.Add("Hughes");
            lst.Add("Flores");
            lst.Add("Washington");
            lst.Add("Butler");
            lst.Add("Simmons");
            lst.Add("Foster");
            lst.Add("Gonzales");
            lst.Add("Bryant");
            lst.Add("Alexander");
            lst.Add("Russell");
            lst.Add("Griffin");
            lst.Add("Diaz");
            lst.Add("Hayes");

            return lst.OrderBy(xx => rnd.Next()).First();
        }

        public string GenRandomFirstName()
        {
            List<string> lst = new List<string>();
            lst.Add("Aiden");
            lst.Add("Jackson");
            lst.Add("Mason");
            lst.Add("Liam");
            lst.Add("Jacob");
            lst.Add("Jayden");
            lst.Add("Ethan");
            lst.Add("Noah");
            lst.Add("Lucas");
            lst.Add("Logan");
            lst.Add("Caleb");
            lst.Add("Caden");
            lst.Add("Jack");
            lst.Add("Ryan");
            lst.Add("Connor");
            lst.Add("Michael");
            lst.Add("Elijah");
            lst.Add("Brayden");
            lst.Add("Benjamin");
            lst.Add("Nicholas");
            lst.Add("Alexander");
            lst.Add("William");
            lst.Add("Matthew");
            lst.Add("James");
            lst.Add("Landon");
            lst.Add("Nathan");
            lst.Add("Dylan");
            lst.Add("Evan");
            lst.Add("Luke");
            lst.Add("Andrew");
            lst.Add("Gabriel");
            lst.Add("Gavin");
            lst.Add("Joshua");
            lst.Add("Owen");
            lst.Add("Daniel");
            lst.Add("Carter");
            lst.Add("Tyler");
            lst.Add("Cameron");
            lst.Add("Christian");
            lst.Add("Wyatt");
            lst.Add("Henry");
            lst.Add("Eli");
            lst.Add("Joseph");
            lst.Add("Max");
            lst.Add("Isaac");
            lst.Add("Samuel");
            lst.Add("Anthony");
            lst.Add("Grayson");
            lst.Add("Zachary");
            lst.Add("David");
            lst.Add("Christopher");
            lst.Add("John");
            lst.Add("Isaiah");
            lst.Add("Levi");
            lst.Add("Jonathan");
            lst.Add("Oliver");
            lst.Add("Chase");
            lst.Add("Cooper");
            lst.Add("Tristan");
            lst.Add("Colton");
            lst.Add("Austin");
            lst.Add("Colin");
            lst.Add("Charlie");
            lst.Add("Dominic");
            lst.Add("Parker");
            lst.Add("Hunter");
            lst.Add("Thomas");
            lst.Add("Alex");
            lst.Add("Ian");
            lst.Add("Jordan");
            lst.Add("Cole");
            lst.Add("Julian");
            lst.Add("Aaron");
            lst.Add("Carson");
            lst.Add("Miles");
            lst.Add("Blake");
            lst.Add("Brody");
            lst.Add("Adam");
            lst.Add("Sebastian");
            lst.Add("Adrian");
            lst.Add("Nolan");
            lst.Add("Sean");
            lst.Add("Riley");
            lst.Add("Bentley");
            lst.Add("Xavier");
            lst.Add("Hayden");
            lst.Add("Jeremiah");
            lst.Add("Jason");
            lst.Add("Jake");
            lst.Add("Asher");
            lst.Add("Micah");
            lst.Add("Jace");
            lst.Add("Brandon");
            lst.Add("Josiah");
            lst.Add("Hudson");
            lst.Add("Nathaniel");
            lst.Add("Bryson");
            lst.Add("Ryder");
            lst.Add("Justin");
            lst.Add("Bryce");

            //—————female

            lst.Add("Sophia");
            lst.Add("Emma");
            lst.Add("Isabella");
            lst.Add("Olivia");
            lst.Add("Ava");
            lst.Add("Lily");
            lst.Add("Chloe");
            lst.Add("Madison");
            lst.Add("Emily");
            lst.Add("Abigail");
            lst.Add("Addison");
            lst.Add("Mia");
            lst.Add("Madelyn");
            lst.Add("Ella");
            lst.Add("Hailey");
            lst.Add("Kaylee");
            lst.Add("Avery");
            lst.Add("Kaitlyn");
            lst.Add("Riley");
            lst.Add("Aubrey");
            lst.Add("Brooklyn");
            lst.Add("Peyton");
            lst.Add("Layla");
            lst.Add("Hannah");
            lst.Add("Charlotte");
            lst.Add("Bella");
            lst.Add("Natalie");
            lst.Add("Sarah");
            lst.Add("Grace");
            lst.Add("Amelia");
            lst.Add("Kylie");
            lst.Add("Arianna");
            lst.Add("Anna");
            lst.Add("Elizabeth");
            lst.Add("Sophie");
            lst.Add("Claire");
            lst.Add("Lila");
            lst.Add("Aaliyah");
            lst.Add("Gabriella");
            lst.Add("Elise");
            lst.Add("Lillian");
            lst.Add("Samantha");
            lst.Add("Makayla");
            lst.Add("Audrey");
            lst.Add("Alyssa");
            lst.Add("Ellie");
            lst.Add("Alexis");
            lst.Add("Isabelle");
            lst.Add("Savannah");
            lst.Add("Evelyn");
            lst.Add("Leah");
            lst.Add("Keira");
            lst.Add("Allison");
            lst.Add("Maya");
            lst.Add("Lucy");
            lst.Add("Sydney");
            lst.Add("Taylor");
            lst.Add("Molly");
            lst.Add("Lauren");
            lst.Add("Harper");
            lst.Add("Scarlett");
            lst.Add("Brianna");
            lst.Add("Victoria");
            lst.Add("Liliana");
            lst.Add("Aria");
            lst.Add("Kayla");
            lst.Add("Annabelle");
            lst.Add("Gianna");
            lst.Add("Kennedy");
            lst.Add("Stella");
            lst.Add("Reagan");
            lst.Add("Julia");
            lst.Add("Bailey");
            lst.Add("Alexandra");
            lst.Add("Jordyn");
            lst.Add("Nora");
            lst.Add("Carolin");
            lst.Add("Mackenzie");
            lst.Add("Jasmine");
            lst.Add("Jocelyn");
            lst.Add("Kendall");
            lst.Add("Morgan");
            lst.Add("Nevaeh");
            lst.Add("Maria");
            lst.Add("Eva");
            lst.Add("Juliana");
            lst.Add("Abby");
            lst.Add("Alexa");
            lst.Add("Summer");
            lst.Add("Brooke");
            lst.Add("Penelope");
            lst.Add("Violet");
            lst.Add("Kate");
            lst.Add("Hadley");
            lst.Add("Ashlyn");
            lst.Add("Sadie");
            lst.Add("Paige");
            lst.Add("Katherine");
            lst.Add("Sienna");
            lst.Add("Piper");

            return lst.OrderBy(xx => rnd.Next()).First();
        }

        public string RandomString() {
            return Path.GetRandomFileName();
        }

        public DateTime RandomDatetime() {
            DateTime start = new DateTime(1995, 1, 1);
            Random gen = new Random();

            int range = (DateTime.Today - start).Days;
            return start.AddDays(gen.Next(range));
        }

        public T RandomEnum<T>()
        {
            return Enum
                .GetValues(typeof(T))
                .Cast<T>()
                .OrderBy(x => rnd.Next())
                .FirstOrDefault();
        }

        public T DummyData<T>() where T : new()
        {
            T RetVal = Activator.CreateInstance<T>();
            Type t = RetVal.GetType();
            PropertyInfo[] Properties = t.GetProperties();
            foreach (var property in Properties)
            {                
                string propName = property.Name;
                string propType = property.PropertyType.Name;
                PropertyInfo propertyInfo = RetVal.GetType().GetProperty(propName);
                if (propName.ToLower().Contains("number") && propType.Equals("String"))
                {
                    propertyInfo.SetValue(RetVal, GenRandomNumber(111111111, 999999999).ToString());
                }
                else if (!propName.ToLower().Contains("id"))
                {
                    switch (propType)
                    {
                        case "Int32":
                            propertyInfo.SetValue(RetVal,GenRandomNumber(111111111, 999999999));
                            break;
                        case "Decimal":
                            propertyInfo.SetValue(RetVal, (decimal)GenRandomNumber(1000, 20000));
                            break;
                        case "String":
                            propertyInfo.SetValue(RetVal, RandomString());                            
                            break;
                        case "DateTime":
                            propertyInfo.SetValue(RetVal, RandomDatetime());
                            break;
                        default:
                            //string baseType = property.PropertyType.BaseType.Name;
                            //switch (baseType)
                            //{
                            //    case "Enum" :
                            //        Type ee = property.PropertyType.BaseType;
                            //       // propertyInfo.SetValue(RetVal, RandomEnum<ee>());
                                    
                            //        break;
                            //}
                            break;
                    }
                }
            }


            return RetVal;
        }
    }
}