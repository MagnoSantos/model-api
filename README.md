## Informações gerais sobre projeto

Aplicação contendo arquitetura modelo para projetos de API's construídas com .Net. 

**Tecnologias e Bibiotecas:**

+ _Framework_ : .NET Core 3.1
+ [AutoFixture - Tests] 
+ [Fluent Assertions - Tests] 
+ [Moq - Tests]
+ [NUnit - Tests Framework]
+ [Swagger - Documentation]
+ [NewtonSoft - JSON]
+ [FluentValidation - JSON]
+ [Flurl - External Services]

## Configurações Específicas:

Esse tópico contempla configurações específicas do projeto.

_Serão apresentadas o passo a passo das configurações: Swagger, Fluent Validation, Configurações da Aplicação, Injeção de Dependências e Testes Unitários. Vale ressaltar a necessidade de incluir os nugets correspondentes no seu projeto para que seja possível realizar as configurações aqui descritas._

+ **Swagger**: Estrutura de _software_ aberto que auxilia os desenvolvedores na projeção e criação de documentações para Web API's. 

Adicionar na classe Startup da sua camanda de apresentação (.API), o seguinte código: 

```csharp
//Define ambiente da aplicação 
public IWebHostEnvironment Environment {get;}

//Configuração do Swagger no ConfigureServices
if (!Environment.IsProduction())
{
    services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "Sample API",
            Description = "API para servir de modelo para as demais aplicações",
            Version = "v1"
            });
        });
    }
}

//Configuração do Swagger no Configure
if (!env.IsProduction())
{
    app.UseSwagger();
    
    app.UseSwaggerUI(swagger =>
    {
        swagger.SwaggerEndpoint("/swagger/v1/swagger.json", "Sample API V1");
        swagger.RoutePrefix = string.Empty;
        });
    }
}
```

Feito isso será apresentado o Swagger da aplicação, mapeando os seus DTO's, rotas expostas, _status code_ de resposta e a possibilidade de realizar o teste da sua aplicação. 

+ **Fluent Validation**: Biblioteca para criação de regras de validação dos corpos enviados nas requisições. 

Nos seus DTO's configure os _validators_ específicos, como no exemplo: 

```csharp
public class ColaboradorValidator: AbstractValidator < ColaboradorDTO > {
    public ColaboradorValidator() {
        RuleFor(colaborador => colaborador.Nome)
            .NotEmpty().WithMessage("O campo de nome deve ser preenchido")
            .NotNull().WithMessage("O campo de nome não pode ser nulo");

        RuleFor(colaborador => colaborador.Salario)
            .NotEmpty().WithMessage("O campo de salario deve ser preenchido")
            .NotNull().WithMessage("O campo de salario não pode ser nulo");

        RuleFor(colaborador => colaborador.Idade)
            .NotEmpty().WithMessage("O campo de idade deve ser preenchido")
            .NotNull().WithMessage("O campo de idade não pode ser nulo");
    }
}
```

Após configuradas as regras de validação, acesse a sua classe _Statup_ e acrescente esse trecho de código: 

```csharp
//Configura fluent validation para validação dos corpos da requisições com base no DTO.
services
    .AddMvc()
    .AddFluentValidation(
        fvc => fvc.RegisterValidatorsFromAssemblyContaining < Startup > ()
    );
```

Agora, basta acessar o controllador e adicionar o código de verificação da validade do model enviado no corpo da requisição (caso não seja válido recomenda-se o retorno do _status code_ 400 - Bad Request): 

```csharp 
if (!ModelState.IsValid) {
    return BadRequest();
}
```

+ **Injeção de Dependências**: Padrão de projetos utilizado para reduzir o nível de acoplamento entre diferentes módulos e/ou camadas da aplicação. 
_Temos a injeção de dependências implementada nativamente no .NET, assim iremos utilizá-la_

+ Configurações da Aplicação

Nessa aplicação, corresponde as variáveis de ambientes que iremos utilizar (como por exemplo: URL's base de API's que serão consumidas na aplicação, tempo de expiração de tokens, dentre outros). Ou seja, tudo que pode ser parametrizado na sua aplicação. 

Nesse projeto, acesse a classe InjetorDependencias, inicialmente foi realizado a configuração das variáveis de ambiente da aplicação. Veja: 

```csharp
public static IServiceCollection AdicionarConfiguracoesAplicacao(
    this IServiceCollection services,
    IConfiguration configuration
) {
    return services
        .Configure<ConfiguracoesAplicacao>(o => {
            o.UrlDummy = configuration.GetValue < string > ("AppConfiguration:UrlDummy");
    });
}

 public class ConfiguracoesAplicacao {
     public string UrlDummy { get; set;}
 }
```

+ Injeção de Dependências

```csharp
public static IServiceCollection AdicionarComponentesAplicacao(this IServiceCollection services) {
    ConfigurarValidator(services);
    ConfigurarRepositories(services);
    ConfigurarServices(services);

    return services;
}

private static void ConfigurarValidator(IServiceCollection services) {
     services
         .AddTransient<IValidator<ColaboradorDTO>, ColaboradorValidator>();
 }
```

Note que é necessário injetar o _validator_ para a efetividade do Fluent Validation. E, no caso das dependências da aplicação, são injetadas informando qual a _interface_ será consumida pela classe concreta. 


Na sua classe Startup, no método de ConfigureServices deverá conter as configurações de injeção de dependências e variáveis da aplicação.

```csharp
 //Configura componentes da aplicação
 services.AdicionarComponentesAplicacao();

 //Configurações da aplicação
 services.AdicionarConfiguracoesAplicacao(Configuration);
```

+ **Testes Unitarios**: A menor parte testável do seu código. 

Os testes unitários no modelo proposto por essa aplicação, contemplam várias bibliotecas, dentre elas: _Fixture_ que é responsável pela criação automática de objetos de entrada e saída dos testes (_objects mocks_), _Fluent Assertions_ utilizado para definir as regras de asserts dos testes e _Moq_ para criar os mocks das dependências para o teste da classe especificada. **_O FrameWork de teste escolhido para essa aplicação foi o NUnit_**. 

Classe para testes do controller Colaborador do projeto: 

```csharp
//Define as dependências, como o fixture e a interface repository que é utilizada no controller
private readonly Fixture _fixture = new Fixture();
private Mock<IColaboradorRepository> _colaboradorRepository;

[SetUp]
public void SetupMocks() {
    _colaboradorRepository = new Mock<IColaboradorRepository>();
}

/*
Define o teste, note que inicialmente é definido o colaborador DTO e o objeto de retorno do teste - colaborador, 
depois é feito um setup para que o retorno seja setado com base na execução do método de adição de colaborador. 
O resultado é processado e os casos de assert vem logo abaixo com o resultado esperado. 
*/
 [Test]
 [Description("POST api/colaboradores")]
 public async Task DeveRetornar200SeConseguirProcessarAAdicaoDoColaborador() {
     var colaborador = _fixture.Create<ColaboradorDTO>();
     var colaboradorRetorno = _fixture.Create<Colaborador>();
     _colaboradorRepository
         .Setup(mock => mock.AdicionarColaborador(colaborador))
         .ReturnsAsync(colaboradorRetorno);
     var controller = Instanciar();

     var resultado = await controller.CadastrarColaborador(colaborador);

     resultado
         .Should()
         .BeOfType<OkObjectResult>()
         .Which.StatusCode
         .Should()
         .Be(200);

     resultado
         .Should()
         .BeOfType<OkObjectResult>()
         .Which.Value
         .Should()
         .BeEquivalentTo(colaboradorRetorno);
 }

//Instanciação do controller
 private ColaboradorController Instanciar() {
     return new ColaboradorController(
         _colaboradorRepository.Object
     );
 }
```

+ **Health Check**: Responsável por retornar uma verificação de integridade do status operacional do seu microsserviço.

Inicialmente configure uma classe com a integração que deseja consultar o status de _health check_, nesse caso, como fazemos integração apenas com a API do Dummy foi a integração escolhida. 

```csharp
//Define a classe de health check
public class DummyAPIHealthCheck: IHealthCheck {
    private readonly IColaboradorService _colaboradorService;

    public DummyAPIHealthCheck(
        IColaboradorService colaboradorService
    )
    {
        _colaboradorService = colaboradorService;
    }

    public Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context,
        CancellationToken cancellationToken =
        default
    ) 
    {
        try {
            var colaboradores = _colaboradorService.BuscarTodosOsColaboradores();
            
            //Retorna o resultado de status da API com base na obtenção dos colaboradores
            if (colaboradores == null) {
                return Task.FromResult(
                    HealthCheckResult.Unhealthy(
                        description: "API Dummy não retornou os colaboradores"
                    )
                );
            }

            return Task.FromResult(HealthCheckResult.Healthy());
            
        } catch (Exception ex) {
            return Task.FromResult(
                HealthCheckResult.Unhealthy(
                    description: "Falha ao obter todos os colaboradores",
                    exception: ex
                )
            );
        }
    }
}
```

Para finalizar essa etapa, é necessário preencher no Startup da aplicação o check específico e a rota para obtenção das informações de _health check_.

```csharp
//Configure services
//Configura o health check para a aplicação
services
    .AddHealthChecks()
    .AddCheck<DummyAPIHealthCheck>("Dummy API");
```

```csharp
//Configure
//Defina a rota de health
app.UseHealthChecks("/health");
```
