﻿using BackendDotnetCore.Enitities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendDotnetCore.Configurations
{
    public class BaseEntityConfiguration:IEntityTypeConfiguration<Enitities.BaseEntity>
    {
        public void Configure(EntityTypeBuilder<BaseEntity> builder)
        {
           
        }
    }
}