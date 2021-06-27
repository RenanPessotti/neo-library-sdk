using System.ComponentModel;

namespace Neo.Extensions.Kafka.Topics
{
    public enum EnumTopicsNeoPonto
    {
        [Description("registro-ponto")]
        ResgistroPonto,

        [Description("integracao")]
        Integracao,

        [Description("integracao-usuario")]
        IntegracaoUsuario,

        [Description("integracao-horario")]
        IntegracaoHorario,

        [Description("integracao-escala")]
        IntegracaoEscala,

        [Description("integracao-movimentacao-escala")]
        IntegracaoMovimentacaoEscala,

        [Description("integracao-ausencia")]
        IntegracaoAusencia,

        [Description("integracao-rubrica")]
        IntegracaoRubrica,

        [Description("integracao-log")]
        IntegracaoLog,

        [Description("integracao-log-vapt")]
        IntegracaoLogVapt,

        [Description("alteracao-ponto")]
        AlteracaoPonto,

        [Description("processa-cartao-ponto")]
        ProcessaCartaoPonto,

        [Description("integracao-registro-ponto")]
        IntegracaoResgistroPonto
    }
}
