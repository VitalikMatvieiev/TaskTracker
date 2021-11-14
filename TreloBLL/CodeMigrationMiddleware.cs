using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Trelo1.Interfaces;
using TreloBLL.Interfaces;
using TreloDAL.Data;

namespace TreloBLL
{
    public class CodeMigrationMiddleware
    {
        private readonly RequestDelegate _next;
        private bool firstTime = true;
         public CodeMigrationMiddleware(RequestDelegate next)
        {
            this._next = next;
        }
        public async Task InvokeAsync(HttpContext context, TreloDbContext _dbContext, TreloBLL.Services.CodeMigration codeMigration, IServiceProvider serviceProvider)
        {
            if (!firstTime)
            {
                return;
            }
            firstTime = false;

            var executedCodeMigration = _dbContext.CodeMigrations;

            var type = typeof(ICodeMigration);

            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p));

            foreach(var t in types)
            {
                var t1 = t.Name;
                if (executedCodeMigration.Any(a => a.MigrationName == t.Name) || t.Name == "ICodeMigration")
                {
                    continue;
                } 
                else
                {
                    ICodeMigration migration = (ICodeMigration)Activator.CreateInstance(t,serviceProvider);//ActivatorUtilities.CreateInstance(serviceProvider, t, new Type[] { typeof(IUserService), typeof(TreloDbContext) }); /*
                    try
                    {
                        await migration.Run();
                        TreloDAL.Models.CodeMigration codeMigrationModel = new TreloDAL.Models.CodeMigration() 
                        { 
                            MigrationName = t.Name, 
                            HasRun = true 
                        };
                        await _dbContext.AddAsync(codeMigrationModel);
                        await _dbContext.SaveChangesAsync();
                    }
                    catch (Exception ex)
                    {

                        throw new Exception();
                    }
                    
                }
            }
        }
    }
}
