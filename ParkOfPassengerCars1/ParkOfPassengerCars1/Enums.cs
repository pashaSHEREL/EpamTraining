using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParkOfPassengerCars1
{
    public enum CarBrand
    {
        BMW, AUDI, KIA,
        FORD, OPEL, NISSAN,
        RENAULT, TOYOTA, HYANDAI,
        CHEVROLET, MERSEDES_BENZ, LEXUS,
        MITSUBISHI, MAZDA, LADA,
        OTHER
    }

    public enum CarColor
    {
        RED, GREEN, BLUE,
        WHITE, BLACK, YELLOW,
        ORANGE, GRAY, PINK,
        OTHER
    }

    public enum ClassOfCar
    {
        LOW,
        AVERAGE,
        PREMIUM
    }

    public enum BodyTypeCar
    {
        SEDAN, WAGON, COUPE,
        HATCBACK, SUV, CROSSOVER,
        MINIVAN, CONVERTIBLE, LIMOUSINE,
        OTHER
    }

    public enum TypeOfFuel
    {
        GASOLINE,
        DIESEL,
        GAS,
        GASOLINE_GAS,
        DIESEL_GAS
    }
}
