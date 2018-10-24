﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using SalesWeb.Models;

namespace SalesWeb.Data
{
    public class SalesWebContext : DbContext
    {
        public SalesWebContext (DbContextOptions<SalesWebContext> options)
            : base(options) { }

        public DbSet<Departamento> Departamento { get; set; }
        public DbSet<Vendas> Venda { get; set; }
        public DbSet<Vendedor> Vendedor { get; set; }
    }
}
