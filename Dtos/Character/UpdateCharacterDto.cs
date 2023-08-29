using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Model;

namespace Dtos.Character
{
    public class UpdateCharacterDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = "Hornet";
        public int Strength { get; set; } = 10;
        public int Hitpoints { get; set; } = 100;
        public int Intelligence { get; set; } = 10;
        public int Defense { get; set;} = 10;
        public RpgClass Class {get; set; } = RpgClass.Knight;
    }
}