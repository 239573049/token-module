using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using NetCore.Domain;
using Token.Module;
using Token.Module.Attributes;

namespace NetCore.HttpApi;

public class NetCoreHttpApiModule : TokenModule
{
    public override void ConfigureServices(IServiceCollection services)
    {
        ConfigSwagger(services);
        ConfigCors(services);
    }

    
    private void ConfigCors(IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy(Constant.Cors, corsBuilder =>
            {
                corsBuilder.SetIsOriginAllowed((string _) => true).AllowAnyMethod().AllowAnyHeader()
                    .AllowCredentials();
            });
        });
    }

    private void ConfigSwagger(IServiceCollection services)
    {
        services.AddSwaggerGen(o =>
        {
            string[] files = Directory.GetFiles(AppContext.BaseDirectory, "*.xml"); //获取api文档
            string[] array = files;
            foreach (string filePath in array)
            {
                o.IncludeXmlComments(filePath, includeControllerXmlComments: true);
            }

            o.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "token API",
                Version = "v1"
            });
            o.DocInclusionPredicate((docName, description) => true);
            o.CustomSchemaIds(type => type.FullName);
            o.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Id = "Bearer", Type = ReferenceType.SecurityScheme
                        }
                    },
                    Array.Empty<string>()
                }
            });
            o.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "请输入文字“Bearer”，后跟空格和JWT值，格式  : Bearer {token}",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey
            });
        });
    }

    public override void OnApplicationShutdown(IApplicationBuilder app)
    {
        UseSwagger(app);
        UseCors(app);
    }

    private void UseSwagger(IApplicationBuilder app)
    {
        var env = app.ApplicationServices.GetService<IWebHostEnvironment>();
        
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "API");
                options.RoutePrefix = string.Empty;
            });
        }
    }
    
    private void UseCors(IApplicationBuilder app)
    {
        app.UseCors(Constant.Cors);
    }
}