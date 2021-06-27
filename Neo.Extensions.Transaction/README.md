## Introdução
Torna um bloco de códigos transacional

* Entity Framework Core
* Microsoft.Extensions.DependencyInjection.Abstractions

### Instalação 

dotnet add package Neo.Extensions.Transaction


### Utilização

O código abaixo demostra como é feita utilização do ``` ITransactionScopeProvider ```

1 - Adicionar no service collection

```cs

        public static void UseTrnasaction(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureTransaction<DbContext>(configuration);
        }
```

2 - Instanciar no trecho de código que deseja utilizar

```cs
        private readonly ITransactionScopeProvider _provider;

        Construtor(ITransactionScopeProvider provider)
        {
            _provider = provider;
        }

        ...

        public async Task Exec()
        {
            using(var transaction = _provider.CreateTransaction()) 
            {
                try
                {
                    ...

                    transaction.Commit();
                } 
                catch (Exception)
                { 
                    transaction.Rollback();
                }
            }
        }
        
```