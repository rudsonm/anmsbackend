using System;
using System.Collections.Generic;
using System.Linq;
using Servicos.Bundles.Core.Entity;
using System.ComponentModel;
using Servicos.Context;

namespace Servicos.Bundles.Animais.Entity
{
    public enum Filo
    {
        [Description("MAMIFERO")]
        MAMIFERO,
        [Description("AVE")]
        AVE,
        [Description("REPTIL")]
        REPTIL,
        [Description("ANFIBIO")]
        ANFIBIO,
        [Description("PEIXE")]
        PEIXE
    }
    public class Animal : AbstractEditableEntity
    {
        public string Nome { get; set; }
        public Filo Filo { get; set; }
        public string Especie { get; set; }
        public float Peso { get; set; }
        public string Descricao { get; set; }
        public DateTime DataNascimento { get; set; }
        public virtual List<int> Fotos {
            get
            {
                return new ServicosContext().Fotos
                                            .Where(foto => foto.EntidadeId == this.Id && foto.EntidadeNome.Equals("Animal"))
                                            .Select(foto => foto.Id)
                                            .ToList();
            }
        }
    }
}