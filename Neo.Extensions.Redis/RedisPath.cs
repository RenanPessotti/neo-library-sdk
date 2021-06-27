namespace Neo.Extensions.Redis
{
    public class RedisPath
    {
        private RedisPath(string value) { Value = value; }

        public string Value { get; set; }

        public static RedisPath KeyMotoristaOnline => new RedisPath("motorista_online");
        public static RedisPath KeyMotoristaLogado => new RedisPath("motorista_logado");
        public static RedisPath KeyMotoristaRegiao => new RedisPath("motorista_regiao");
        public static RedisPath KeySolicitacaoAceite => new RedisPath("solicitacao_aceite");
        public static RedisPath KeySolicitacaoTrajeto => new RedisPath("solicitacao_trajeto");
        public static RedisPath KeyDistanciaRestante => new RedisPath("distancia_restante");
        public static RedisPath KeySolicitacaoAguardando => new RedisPath("solicitacao_aguardando");
        public static RedisPath KeySolicitacaoAceita => new RedisPath("solicitacao_aceita");
        public static RedisPath KeyRefreshToken => new RedisPath("auth_refresh_token");
        public static RedisPath KeySolicitacaoRedistribuicao => new RedisPath("solicitacao_redistribuicao");
        public static RedisPath KeyRequestCache => new RedisPath("request_cache");
        public static RedisPath KeyVerificacaoEmail => new RedisPath("verificacao_email");
        public static RedisPath KeyPrevisaoOfertaDisponibilidade => new RedisPath("previsao_disponibilidade");
        public static RedisPath KeyPrevisaoOfertaDusponibilidadeLocation => new RedisPath("previsao_disponibilidade_location");
        public static RedisPath KeyPrevisaoDisponibilidade => new RedisPath("previsao_disponibilidade");
        public static RedisPath KeyVeiculoRegiao => new RedisPath("veiculo_regiao");
        public static RedisPath KeyContratacaoReservada => new RedisPath("contratacao_reservada");
        public static RedisPath KeyHotspot => new RedisPath("hotspot");
        public static RedisPath KeyControleAceite => new RedisPath("controle_aceite");
        public static RedisPath KeyEsqueciSenha => new RedisPath("esqueci_senha");
        public static RedisPath KeyNovaSenhaApp => new RedisPath("nova_senha_app");
        public static RedisPath KeyFutureOffer => new RedisPath("future_offer");
        public static RedisPath KeyLockDriver => new RedisPath("lock_driver");
        public static RedisPath KeyTentativasEsquecisenha => new RedisPath("tentativas_esquecisenha");
        public static RedisPath KeyIdentificadorDriversFuture => new RedisPath("identificador_drivers_future");
        public static RedisPath KeyTentativasConfirmacaoSms => new RedisPath("tentativas_confirmacao_sms");
        public static RedisPath KeyProrrogacaoJornada => new RedisPath("prorrogacao_jornada");
        public static RedisPath KeyFimTurno => new RedisPath("fim_turno");
        public static RedisPath KeyContratacaoExpirar => new RedisPath("contratacao_expirar");
        public static RedisPath KeyCacheLocalizacao => new RedisPath("cache_localizacao");
        public static RedisPath KeySolicitacaoCompartilhada => new RedisPath("solicitacao_compartilhada");
        public static RedisPath KeyCobrancaPendente => new RedisPath("cobranca_pendente");
    }
}
