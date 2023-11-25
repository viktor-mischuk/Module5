using System;
using System.Diagnostics.Eventing.Reader;
using System.Runtime.ConstrainedExecution;
using System.Xml.Linq;

namespace ConsoleApp1
{
    enum WordType
    {
        Text,
        Digit
    }

    internal class Program
    {
        

        static void Main(string[] args)
        {
            DisplayUser(EnterUser());
        }

        static void DisplayUser((string Name, string LastName, int Age, string[] PetNames, string[] FavoriteColors) user)
        {
            Console.WriteLine();
            Console.WriteLine($"Ваше имя: {user.Name}");
            Console.WriteLine($"Ваша фамилия: {user.LastName}");
            Console.WriteLine($"Вам {user.Age} лет");
            if(user.PetNames != null) 
            {
                Console.WriteLine($"У Вас {user.PetNames.Length} питомцев");
                foreach (var pet in user.PetNames)
                {
                    Console.WriteLine($"\t Кличка питомца: {pet} ");
                }
            }
            if(user.FavoriteColors != null)
            {
                Console.WriteLine($"Вы любите {user.FavoriteColors.Length} цветов");
                foreach (var color in user.FavoriteColors)
                {
                    Console.WriteLine($"\t Любимый цвет: {color} ");
                }
            }
            
            Console.ReadKey();

        }
        static (string name, string lastName, int age, string[] petNames, string[] favoriteColors) EnterUser()
        {
            (string Name, string LastName, int Age, string[] PetNames, string[] FavoriteColors) User;
            User.PetNames = null;
            User.FavoriteColors = null;
            User.Name = "";
            User.LastName = "";
            User.Age = 0;

            (WordType wordType, int outNumb) inputWord;

            bool isName = false;
            while (!isName)
            {
                Console.WriteLine("Ваше имя?");
                string name = Console.ReadLine();
                inputWord = CheckInput(name);
                if (inputWord.wordType == WordType.Text && name != "")
                {
                    
                    isName = true;
                    User.Name = name;
                }
                else
                {
                    Console.WriteLine($"Цифра {inputWord.outNumb} не может быть именем");
                }
            }

            bool isLastName = false;
            while (!isLastName)
            {
                Console.WriteLine("Ваша фамилия?");
                string lastname = Console.ReadLine();
                inputWord = CheckInput(lastname);
                if (inputWord.wordType == WordType.Text && lastname !="")
                {
                    isLastName = true;
                    User.LastName = lastname;
                }
                else
                {
                    Console.WriteLine($"Цифра {inputWord.outNumb} не может быть фамилией");
                }
            }

            bool isAge = false;
            while (!isAge)
            {
                Console.WriteLine("Введите возраст цифрами. Значение должно быть больше нуля");
                string age = Console.ReadLine();
                inputWord = CheckInput(age);
                if (inputWord.wordType == WordType.Digit && inputWord.outNumb > 0)
                {
                    isAge = true;
                    User.Age = inputWord.outNumb;
                }
            }

            Console.WriteLine("Есть ли питомец да/нет");
            string hasPet = Console.ReadLine();
            switch (hasPet)
            {
                case "да":
                    {
                        bool isHasPet = false;
                        while (!isHasPet)
                        {
                            Console.WriteLine("Количество животных цифрами. Значение должно быть больше нуля");
                            string pets = Console.ReadLine();
                            inputWord = CheckInput(pets);
                            if (inputWord.wordType == WordType.Digit && inputWord.outNumb > 0)
                            {
                                isHasPet = true;
                                User.PetNames = CreateArrayPets(inputWord.outNumb);
                            }
                        }
                        break;
                    }
                case "нет":
                    {
                        break;
                    }
                default:
                    {
                        Console.WriteLine($"Будем считать , что нет)))ds" +
                            $"");
                        break;
                    }
            }

            bool isHasFavColor = false;

            while (!isHasFavColor)
            {
                Console.WriteLine("Количество любимых цветов. Значение должно быть больше нуля");
                string colors = Console.ReadLine();
                inputWord = CheckInput(colors);
                if (inputWord.wordType == WordType.Digit && inputWord.outNumb > 0)
                {
                    isHasFavColor = true;
                    User.FavoriteColors = CreateFavorColor(inputWord.outNumb);
                }
                else
                {
                    isHasFavColor = true;
                    Console.WriteLine("Цвета Вы не любите)))");
                }
            }


            return User;
        }

        static (WordType, int) CheckInput(string str)
        {
            int keyResult;
            if (int.TryParse(str, out keyResult))
            {
                return (WordType.Digit, keyResult);
            }
            return (WordType.Text, keyResult);
        }
        static string[] CreateFavorColor(int qColors)
        {
            string[] colors = new string[qColors];
            for (int i = 0; i < colors.Length; i++)
            {
                bool isColor = false;
                while (!isColor)
                {
                    Console.WriteLine($"Название {i + 1}-го цвета");
                    string color = Console.ReadLine();
                    (WordType wordType, int outNumb) inputWord = CheckInput(color);
                    if (inputWord.wordType == WordType.Text)
                    {
                        isColor = true;
                        colors[i] = color;
                    }
                    else
                    {
                        Console.WriteLine($"цифра {inputWord.outNumb} не может быть названием цвета");
                    }
                }
            }
            return colors;
        }
        static string[] CreateArrayPets(int qPets)
        {
            string[] petnames = new string[qPets];
            for (int i = 0; i < petnames.Length; i++)
            {
                bool isName = false;

                while (!isName)
                {
                    Console.WriteLine($"Кличка животного {i + 1}-го");
                    string name = Console.ReadLine();
                    (WordType wordType, int outNumb) inputWord = CheckInput(name);
                    if (inputWord.wordType == WordType.Text)
                    {
                        isName = true;
                        petnames[i] = name;
                    }
                    else
                    {
                        Console.WriteLine($"{inputWord.outNumb} не может быть кличкой");
                    }
                }
            }
            return petnames;
        }
    }
}

