using System.ComponentModel;

namespace Neo.Extensions.Core.Enums
{
    public enum ClassificacaoTipoEventoEnum
    {
        [Description("inicio-jornada")]
        InicioJornada,

        [Description("fim-jornada")]
        FimJornada,

        [Description("descanso")]
        Descanso,

        [Description("lanche")]
        Lanche,

        [Description("refeicao")]
        Refeicao,

        [Description("espera-motorista")]
        EsperaMotorista,

        [Description("interjornada")]
        Interjornada,

        [Description("nao-definido")]
        NaoDefinido,

        [Description("inicio-direcao")]
        InicioDirecao,

        [Description("fim-direcao")]
        FimDirecao
    }
}
