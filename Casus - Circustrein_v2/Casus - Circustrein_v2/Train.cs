﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casus___Circustrein_v2
{
    public class Train
    {
        public List<Wagon> Wagons = new List<Wagon>();

        public Wagon AddAnimalToWagon(Animal animal)
        {
            Wagon newWagon = new Wagon();
            newWagon.AddAnimal(animal);
            Wagons.Add(newWagon);
            return newWagon;
        }

        public List<Animal> SortAnimals(List<Animal> unorderedAnimals)
        {
            return unorderedAnimals.OrderByDescending(animal => animal.Type)
                .ThenByDescending(animal => (int) animal.Size).ToList();
        }

        public void FillWagon(List<Animal> allAnimals)
        {
            allAnimals = SortAnimals(allAnimals);

            foreach (Animal currentAnimal in allAnimals)
            {
                if (currentAnimal.Type == Animal.Types.Carnivore)
                {
                    AddAnimalToWagon(currentAnimal);
                }
                else
                {
                    bool animalAdded = false;

                    foreach (Wagon currentWagon in Wagons)
                    {
                        animalAdded = currentWagon.AddAnimal(currentAnimal);
                        if (animalAdded) break;
                    }

                    if (!animalAdded)
                    {
                        AddAnimalToWagon(currentAnimal);
                    }
                }
            }
        }
    }
}
