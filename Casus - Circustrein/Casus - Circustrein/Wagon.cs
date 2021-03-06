﻿using System.Collections.Generic;
using System.Linq;

namespace Casus___Circustrein
{
<<<<<<< HEAD
    public class Wagon
=======
    class Wagon
>>>>>>> master
    {
        public List<Animal> Animals = new List<Animal>();
        public int Space { get; private set; } = 10;

        private Animal.Sizes BiggestFlesheater()
        {
            List<Animal> FlesheaterList = Animals.Where(Animal => Animal.Carnivore).ToList();

            if (FlesheaterList.Count == 0)
            {
                return Animal.Sizes.None;
            }
            else
            {
                return FlesheaterList.First().Size;
            }

        }

        public bool TryAddAnimal(Animal newAnimal)
        {
            if (newAnimal.Size > BiggestFlesheater() && (int) newAnimal.Size <= Space)
            {
                Animals.Add(newAnimal);
                Space = 10 - Animals.Sum(Animal => (int) Animal.Size);
                return true;
            }
            else
            {
                return false;
            }
        }

        public override string ToString()
        {
            return "Wagon";
        }
    }
}
