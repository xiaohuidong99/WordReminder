﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WordReminder.Data;
using Microsoft.EntityFrameworkCore;
using WordReminder.Biz.UnitOfWorks;

namespace WordReminder.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {         
            services.AddMvc();

            string connection = @"Server=(localdb)\mssqllocaldb;Database=WordReminderDb;Trusted_Connection=True;ConnectRetryCount=0";
            services.AddDbContext<WordReminderContext>(opption => opption.UseSqlServer(connection));
            services.AddSingleton<IUnitOfWork, UnitOfWork>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
