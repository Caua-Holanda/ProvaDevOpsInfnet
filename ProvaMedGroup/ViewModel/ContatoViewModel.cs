using System.ComponentModel.DataAnnotations;
using System;
using ProvaMed.Api.Extensions;

namespace ProvaMedGroup.ViewModels
{
    public class ContatoViewModel
    {

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O Máximo de caracteres permitidos do campo {0} é {2} a {1}.", MinimumLength = 2)]
        public string Nome { get; set; }

        public char Sexo { get; set; }


        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public DateTime DataNascimento { get; set; }
        public int Idade
        {
            get
            {
                int idade = DateTime.Now.Year - DataNascimento.Year;
                if (DateTime.Now.DayOfWeek < DataNascimento.DayOfWeek)
                {
                    idade -= 1;
                }
                return idade;
            }
        }
       public bool Ativo { get; set; }
    }
}
